using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PainelDoPerfil : MonoBehaviour
{
    [SerializeField] private TextsDoPainelDoPerfil texts;
    [SerializeField]private GameObject[] osPersonagens;

    [System.Serializable]
    private struct TextsDoPainelDoPerfil
    {
        [Header("Dados Principais")]
        public Text nomeDoPerfil;
        public Text quantidadeDeDinheiro;
        public Text quantidadeDeEstrelas;

        [Space(1)][Header("Estatisticas")]
        public Text quantidadeDaPontuacao;
        public Text numeroDeCombos;
        public Text numMaxMoedas;
        public Text numMaxCubos;
        public Text numMaxEsferas;
        public Text numMaxEstaminas;
        public Text nivelMaxAlcancado;
        public Text numInimigosDerrotados;
    }

    public static PainelDoPerfil t;
    // Use this for initialization
    void Awake()
    {
        t = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        if(SceneManager.GetActiveScene().name=="titulo")
            TrocaCabecudinho();
    }

    void TrocaCabecudinho()
    {
        GameObject G = GameObject.FindWithTag("Player");
        Transform T = G.transform;

        G = (GameObject)Instantiate(
            osPersonagens[ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.IndiceDoPersonagemSelecionado],
            T.position,
            T.rotation
            );
        G.transform.GetChild(0).gameObject.layer = 8;// LayerMask.NameToLayer("viewCamera");

        Destroy(T.gameObject);

        G.GetComponent<EstadoDePersonagem_Gerente>().enabled = false;
        G.GetComponent<AudioListener>().enabled = false;
        G.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
    }

    public void AtualizaExibicao(Perfil perfil)
    {
        if (texts.numeroDeCombos == null)
        {
            ExibeDadosPrincipais(perfil);
        }
        else
        {
            texts.quantidadeDaPontuacao.text = perfil.MaiorPontuacao.ToString();
            texts.numeroDeCombos.text = perfil.ComboMaximoAlcancado.ToString();
            texts.numMaxMoedas.text = perfil.NumeroMaximoDeMoedasEmUnicoJogo.ToString();
            texts.numMaxCubos.text = perfil.NumeroMaximoDeCheckCombosEmUnicoJogo.ToString();
            texts.numMaxEsferas.text = perfil.NumeroMaximoDeEsferasEmUnicoJogo.ToString();
            texts.numMaxEstaminas.text = perfil.NumeroMaximoDeEstaminasEmUnicoJogo.ToString();
            texts.nivelMaxAlcancado.text = perfil.NivelMaximoAlcancado.ToString();
            texts.numInimigosDerrotados.text = perfil.NumeroMaxInimigosDerrotadosEmunicoJogo.ToString();
        }
    }

    public void ZeraResultados()
    {
        if (texts.quantidadeDaPontuacao != null)
        {
            texts.quantidadeDaPontuacao.text = "0";
            texts.numeroDeCombos.text = "0";
            texts.numMaxMoedas.text = "0";
            texts.numMaxCubos.text = "0";
            texts.numMaxEsferas.text = "0";
            texts.numMaxEstaminas.text = "0";
            texts.nivelMaxAlcancado.text = "0";
        }
    }

    public void ExibeDadosPrincipais(Perfil perfil)
    {
        texts.nomeDoPerfil.text = perfil.NomeDoPerfil;
        texts.quantidadeDeDinheiro.text = perfil.Dinheiro.ToString();
        texts.quantidadeDeEstrelas.text = perfil.EstrelasDeCristal.ToString();
    }

    public void BotaoIniciarJogo()
    {
        ControladorGlobal.c.Estado = EstadoDoSoftware.emJogo;
        SceneManager.LoadScene("inicial");
    }

    public void BotaoVoltar()
    {
        InicializacaoDePerfil.I.ReligarEssePainel();
    }

    public void BotaoDesafios()
    {

    }

    public void BotaoDosHerois()
    {
        ControladorGlobal.c.Estado = EstadoDoSoftware.cenaDosPersonagens;
        SceneManager.LoadScene("escolheCabecudinho");
    }
}
