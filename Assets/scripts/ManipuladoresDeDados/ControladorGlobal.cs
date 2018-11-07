using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorGlobal : MonoBehaviour
{
    [SerializeField]private EstadoDoSoftware estado;
    [SerializeField]private DadosGlobais dadosGlobais = new DadosGlobais();
    [SerializeField]private ContainerDosDadosEmJogo emJogo;
    [SerializeField]private MusicaDeFundo musicas;

    public static ControladorGlobal c;

    public MusicaDeFundo Musicas
    {
        get { return musicas; }
    }

    public EstadoDoSoftware Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    public DadosGlobais DadosGlobais
    {
        get { return dadosGlobais; }
        set { dadosGlobais = value; }
    }

    public ContainerDosDadosEmJogo EmJogo
    {
        get { return emJogo; }
        set { emJogo = value; }
    }

    // Use this for initialization
    void Start()
    {
        GameObject[] G = GameObject.FindGameObjectsWithTag("GameController");
        
        if (G.Length>1)
            Destroy(gameObject);
        else
            c = this;

        DontDestroyOnLoad(gameObject);
        dadosGlobais = SaveGame.Load();
        dadosGlobais.ZerarJogosSeguidos();

        /*
            Uma pessima gambiarra para evitar erros nos testes das outras cenas
        */
        if (dadosGlobais.Perfis.Count == 0 && SceneManager.GetActiveScene().name != "titulo"
            && SceneManager.GetActiveScene().name != "Perfil"
             && SceneManager.GetActiveScene().name != "novoTitulo")
        dadosGlobais.CriarUmPerfilDeTesteParaCena();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "titulo" 
            && 
            estado == EstadoDoSoftware.retornandoParaPerfilDoTitulo)
        {
            
            FindObjectOfType<InicializacaoDePerfil>().BotaoAvancar();
            estado = EstadoDoSoftware.telaTitulo;
        }

        musicas.Update();
        
    }
}

public enum EstadoDoSoftware
{
    telaTitulo,
    emJogo,
    retornandoParaPerfilDoTitulo,
    contadorDePontos,
    cenaDosPersonagens
}
