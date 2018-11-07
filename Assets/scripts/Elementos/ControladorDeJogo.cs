using UnityEngine;
using System.Collections;

public class ControladorDeJogo : MonoBehaviour {

    public static ControladorDeJogo c;
    public GameObject mostradorDeAlvo;

    [SerializeField]private GameObject[] elementos;
    [SerializeField]private AudioClip[] sons;
    [SerializeField]private SpawnerDeInimigos spawn;
    [SerializeField]private SpawnDeItens spawnItens;
    [SerializeField]private AtaqueEspecial atkE;
    [SerializeField]private GerenciadorDeCombos gCombo;
    [SerializeField]private GerenciadorDePontos gPontos;
    [SerializeField]private Transform spawnPersonagem;
    [SerializeField]private GameObject[] osPersonagens; 
    [SerializeField]private Transform[] cantosDoMapa;

    private bool pause;
    private bool jaIniciouHeroi;
    private bool usouVidaPorEstrela = false;
    private bool usouVidaPorPropaganda = false;

    [SerializeField]private bool vidaExtra = false;
    [SerializeField]private bool moedasMagneticas = false;

    [Header("Variaveis para testes")]
    [SerializeField]private bool umPontoDeVida = false;
    [SerializeField]private bool iniciarComEspecialCheio = true;
    [SerializeField]private int numCabecudinho = -1;
    

    
    public bool VidaExtra
    {
        get { return vidaExtra; }
        set { vidaExtra = value; }
    }

    public bool MoedasMagneticas
    {
        get { return moedasMagneticas; }
        set { moedasMagneticas = value; }
    }
    public AudioClip[] Sons
    {
        get { return sons; }
    }
    public GerenciadorDePontos G_Pontos
    {
        get { return gPontos; }
    }

    public GerenciadorDeCombos G_Combos
    {
        get { return gCombo; }
    }

    public SpawnerDeInimigos Spawn
    {
        get { return spawn; }
    }

    public bool Pause
    {
        get { return pause; }
        set { pause = value; }
    }

    public Transform[] CantosDoMapa
    {
        get {
            if (cantosDoMapa.Length == 0)
                Debug.LogError("Cantos do mapa não setados corretamente");

            return cantosDoMapa;
        }
    }

    public AtaqueEspecial AtkE
    {
        get { return atkE; }        
    }

    public SpawnDeItens SpawnItens
    {
        get { return spawnItens; }
    }

    public bool UsouVidaPorEstrela
    {
        get { return usouVidaPorEstrela; }
        set { usouVidaPorEstrela = value; }
    }

    public bool UsouVidaPorPropaganda
    {
        get { return usouVidaPorPropaganda; }
        set { usouVidaPorPropaganda = value; }
    }

    public GameObject RetornaElemento(Elementos e)
    {
        GameObject retorno = null;
        for (int i = 0; i < elementos.Length; i++)
            if (elementos[i].name == e.ToString())
                retorno = elementos[i];
        return retorno;
    }

    void SpawnarCabecudinho()
    {
        Instantiate(
            osPersonagens[ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.IndiceDoPersonagemSelecionado],
            spawnPersonagem.position,
            Quaternion.identity
            );
    }

    // Use this for initialization
    void Start()
    {
        
        c = this;
        gCombo.SetaMods();
        
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "inicial")
        {
            AplicaTestesPreSpawn();
            //ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.IndiceDoPersonagemSelecionado = 4;
            SpawnarCabecudinho();
                       
            ControladorGlobal.c.EmJogo = new ContainerDosDadosEmJogo();
            Perfil perfilSelecionado = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
            perfilSelecionado.GMissoes.SetarMissoes();
            perfilSelecionado.AplicarEfeitosDeEquipamento();

            AplicaTestesPosSpawn();
        }

        /*
        AdsManager ads =  FindObjectOfType<AdsManager>();
        if (ads)
            ads.RequestInterstitial();
            */
    }

    void AplicaTestesPreSpawn()
    {
        if(numCabecudinho!=-1)
        {
            Perfil P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
            P.IndiceDoPersonagemSelecionado 
                = Mathf.Clamp(numCabecudinho,0,P.MeusPersonagens.Length);
        }
    }

    void AplicaTestesPosSpawn()
    {
        if (umPontoDeVida)
            GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().Dados.UmPontoDeVida = true;

        if (iniciarComEspecialCheio)
            GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().Dados.AdicionaCristais(
                GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().Dados.CristaisParaAtivar)
                ;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "inicial")
            {
                if (!jaIniciouHeroi)
                {
                    Spawn.Start();
                    SpawnItens.Start();
                    jaIniciouHeroi = true;
                }
                else
                {
                    Spawn.Update();
                    SpawnItens.Update();
                }
            }
            gCombo.Update();
            AtkE.Update();
        }
    }

    public void DisparaEspecial()
    {
        AtkE.Iniciar();
        GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().ApComandos.Mov.PararMovimento();
    }
}

public enum Elementos
{
    cuboColisor,
    parDano,
    parTomeiDano,
    Moeda,
    parMoeda,
    CristalDeEspecial,
    pegueiCristal,
    parDanoEspecial,
    checkCombo,
    maisEstamina,
    explosao,
    TNT,
    CanvasDerrotado,
    estrelaDeCristal,
    checkComboParticles,
    cuboMaisAtaque,
    aMagiaAcertou,
    magiaDoMaguinho,
    mensVidaExtra,
    reviviParticulas,
    reviviLevantei,
    cuboEmChamas,
    estouQueimando,
    somDoAtaque,
    somDoAtaque2,
    somDoAtaqueDeFogo,
    danoDeFogo
}
