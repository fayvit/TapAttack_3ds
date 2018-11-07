using UnityEngine;
using System.Collections;
public class EstadoDePersonagem_Gerente:MonoBehaviour
{
    [SerializeField]private DadosDoPersonagem dados;
    [SerializeField]private EstadoDePersonagem estado = EstadoDePersonagem.Controlavel;
    [SerializeField]private AplicadorAfastadorDeInimigo aDI;

    private bool reviveu = false;
    private AudioSource audioX;
    private AplicadorDeComandos ap;
    private EstadoDeDano estadoDeDano = new EstadoDeDano();

    public DadosDoPersonagem Dados
    {
        get{ return dados; }
    }

    public AplicadorDeComandos ApComandos
    {
        get { return ap; }
    }

    void Start() {
        
        dados.Start();
        ap = new AplicadorDeComandos(dados, gameObject);
        audioX = GetComponent<AudioSource>();
    }
     
    void Update() {
        switch (estado)
        {
            case EstadoDePersonagem.Controlavel:
                dados.RegeneracaoDeEstamina();
                if(!ControladorDeJogo.c.Pause)
                    ap.Update();
            break;
            case EstadoDePersonagem.emDano:
                ap.Mov.PararMovimento();
                if (!estadoDeDano.Update())
                    estado = EstadoDePersonagem.Controlavel;
            break;
            case EstadoDePersonagem.morto:
                //morto
            break;
        }
        
    }

    public void Revivi()
    {
        dados.VidaCorrente = dados.VidaMax;
        estado = EstadoDePersonagem.Controlavel;
    }

    public void AplicarDano(int dano,float tempoDeDano = 0.5f)
    {
        if (estado != EstadoDePersonagem.morto)
        {
            dados.AplicarDano(dano);
            audioX.clip = ControladorDeJogo.c.Sons[0];
            audioX.Play();
            if (ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado
                .PersonagemAtualSelecionado.Bonus != BonusDePersonagem.naoPerdeCheckCombo)
            {
                ControladorDeJogo.c.G_Combos.ZerarCombo();
            }

            ap.Mov.AnimaTomeiDano();
            estadoDeDano.DisparaDano(tempoDeDano);
            estado = EstadoDePersonagem.emDano;
            aDI.AfastaInimigoDeDano(transform);

            if (dados.VidaCorrente <= 0)
            {
                PersonagemDerrotado();
            }
        }
    }

    void PersonagemDerrotado()
    {
        ControladorGlobal.c.EmJogo.Pontuacao = ControladorDeJogo.c.G_Pontos.PontosTotais;
        ControladorGlobal.c.EmJogo.Nivel = Dados.NivelParaMostrador;
        estado = EstadoDePersonagem.morto;
        ap.Mov.AnimaDerrotado();

        
        if (ControladorDeJogo.c.VidaExtra)
        {
            
            InserirVidaExtra(MensagemVidaExtra.tipoDeVidaExtra.anel);
            ControladorDeJogo.c.VidaExtra = false;
        }
        else
        if (ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado
                .PersonagemAtualSelecionado.Bonus == BonusDePersonagem.vidaExtra
                &&
                !reviveu
            )
        {
            InserirVidaExtra(MensagemVidaExtra.tipoDeVidaExtra.bonusPersonagem);
            reviveu = true;
        }
        else
            Instantiate(ControladorDeJogo.c.RetornaElemento(Elementos.CanvasDerrotado));
    }

    public void InserirVidaExtra(MensagemVidaExtra.tipoDeVidaExtra tipo)
    {
        GameObject G = ControladorDeJogo.c.RetornaElemento(Elementos.mensVidaExtra);
        G = ParentearNaHUD.Parentear(G, GameObject.Find("Canvas").GetComponent<RectTransform>());
        G.GetComponent<MensagemVidaExtra>().Tipo = tipo;
    }
}

public enum EstadoDePersonagem
{
    Controlavel,
    emDano,
    emEspecial,
    morto
}