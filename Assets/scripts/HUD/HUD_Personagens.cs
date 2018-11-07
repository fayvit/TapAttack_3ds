using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HUD_Personagens : MonoBehaviour
{
    [System.Serializable]
    public class ElementosDaHUD_Personagem
    {
        [Header("HUD cabeçalho")]
        public Text numMoedas;
        public Text numEstrelas;

        [Header("Textos das habilidades")]
        public Text nomePersonagem;
        public Text txtBonus;
        public Text txtHabilidades;
        public Text txtBtnMelhorar;

        [Header("HUD das Compras")]
        public Image[] cadeados;
        public GameObject botoesDeComprarPersonagem;
        public PainelUmaMensagem painelUmaMensagem;
    }

    [SerializeField]private ElementosDaHUD_Personagem elementosDaHUD;
    //[SerializeField]private MovimentadorDoPersonagemNaTroca movT;
    [SerializeField]private BotoesDaHUD_Personagem btns;
    [SerializeField]private CompraDeNovosPersonagens compraP;
    

    private Personagem P;
    private Perfil p;

    // Use this for initialization
    void Start()
    {
        compraP.Start();
        //movT.Start();
        p = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        P = p.PersonagemAtualSelecionado;
        VerificaPersonagensHabilitados();
        SelecionaPersonagem(p.IndiceDoPersonagemSelecionado);

        if (elementosDaHUD.txtBtnMelhorar == null)
        {

        }
    }

    void VerificaPersonagensHabilitados()
    {
        for (int i = 1; i < p.MeusPersonagens.Length; i++)
        {
            elementosDaHUD.cadeados[i-1].enabled = p.MeusPersonagens[i].Bloqueado;
        }
    }

    void AtualizaTextos()
    {
        elementosDaHUD.nomePersonagem.text = P.NomeDoPersonagem;
        elementosDaHUD.txtBonus.text = BancoDeTextos.TextosDoIdioma(P.Bonus.ToString());
        elementosDaHUD.txtHabilidades.text
            = string.Format(
                BancoDeTextos.TextosDoIdioma(P.Habilidade.ToString()),
                P.ValorCorrenteDaHabilidade.ToString());

        elementosDaHUD.numEstrelas.text = p.EstrelasDeCristal.ToString();
        elementosDaHUD.numMoedas.text = p.Dinheiro.ToString();

        if(elementosDaHUD.txtBtnMelhorar!=null)
            elementosDaHUD.txtBtnMelhorar.text = MelhorarPersonagem.TextoDeMelhora(P);
    }

    // Update is called once per frame
    void Update()
    {
        //movT.Update();
        AtualizaTextos();
        compraP.Update();
    }

    void MostraDetalhesDeCompraPersonagem(int quem)
    {
        P = p.MeusPersonagens[quem];
        btns.HabilitarCompra();
        //movT.DisparaTroca(quem);
    }

    void SelecionaPersonagem(int quem)
    {
        p.IndiceDoPersonagemSelecionado = quem;
        P = p.PersonagemAtualSelecionado;
        btns.DesabilitarCompra();
        //movT.DisparaTroca(quem);
    }

    
    public void EscolherPersonagem(int este)
    {
        
        if (p.MeusPersonagens[este].Bloqueado)
        {
            Image img = GameObject.Find("containerDePersonagens").transform.GetChild(este).GetComponent<Image>();
            compraP.MudouPersonagem(este,img);
            MostraDetalhesDeCompraPersonagem(este);
        }
        else
        {
            SelecionaPersonagem(este);
        }
    }

    public void BotaoMelhorar()
    {
        MelhorarPersonagem.Melhorar(elementosDaHUD.painelUmaMensagem,btns, BotaoPorEnquantoNao);
    }

    public void BotaoRetornarAoPerfil()
    {
        ControladorGlobal.c.Estado = EstadoDoSoftware.telaTitulo;
        SceneManager.LoadScene("PreJogo");
    }

    public void BotaoPorEnquantoNao()
    {
        SelecionaPersonagem(p.IndiceDoPersonagemSelecionado);
    }

    public void BotaoComprarPersonagem()
    {
        if (compraP.TentarComprar())
        {
            VerificaPersonagensHabilitados();
            SelecionaPersonagem(p.IndiceDoPersonagemSelecionado);
            btns.DesabilitarCompra();
        }
        else
        {
            btns.DesabilitarBtnsDeCompra();
            elementosDaHUD.painelUmaMensagem.ConstroiPainelUmaMensagem(BotaoPorEnquantoNao, "Você ainda não tem as estrelas necessárias");
            /*
            elementosDaHUD. painelUmaMensagem.gameObject.SetActive(true);
            elementosDaHUD.painelUmaMensagem.retornar += new PainelUmaMensagem.RetornarParaAntecessor(BotaoPorEnquantoNao);
            elementosDaHUD.painelUmaMensagem.AtualizarTextoDaMensagem(
                "Você ainda não tem as estrelas necessárias"
                );*/
        }
    }
}
