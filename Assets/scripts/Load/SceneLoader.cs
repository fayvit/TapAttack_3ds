using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class SceneLoader:MonoBehaviour
{

    [SerializeField]private LoadBar loadBar;

    //private SaveDates S;
    private AsyncOperation[] a2;
    private FasesDoLoad fase = FasesDoLoad.emEspera;
    private NomesCenas cenaComum = NomesCenas.nula;
    private NomesCenas[] cenasParaCarregar;
    private bool podeIr = false;    
    private float tempo = 0;

    private const float tempoMin = 0.25f;

    private enum FasesDoLoad
    {
        emEspera,
        carregando,
        escurecendo,
        clareando
    }

    public static void CarregarCenas(NomesCenas nomeCena,NomesCenas cenaComum = NomesCenas.nula)
    {
        CarregarCenas(new NomesCenas[1] { nomeCena },cenaComum);
    }

    public static void CarregarCenas(NomesCenas[] nomesCenas, NomesCenas cenaComum = NomesCenas.nula)
    {
        GameObject G = new GameObject();
        SceneLoader loadScene = G.AddComponent<SceneLoader>();
        loadScene.CenaDoCarregamento(nomesCenas,cenaComum);
    }

    public void CenaDoCarregamento(NomesCenas[] nomesCenas, NomesCenas cenaComum = NomesCenas.nula)
    {
        this.cenaComum = cenaComum;
        DontDestroyOnLoad(gameObject);
        cenasParaCarregar = nomesCenas;
        SceneManager.LoadScene(NomesCenas.CenaDeCarregamento.ToString());
        SceneManager.sceneLoaded += IniciarCarregamento;
        
    }

    void IniciarCarregamento(Scene cena,LoadSceneMode mode)
    {
        
        loadBar = FindObjectOfType<LoadBar>();

        if (cenaComum != NomesCenas.nula)
        {
            SceneManager.LoadSceneAsync(cenaComum.ToString(), LoadSceneMode.Additive);
            SceneManager.sceneLoaded -= IniciarCarregamento;
            SceneManager.sceneLoaded += CarregouComuns;
        }
        else
        {
           ComunsCarregado();
        }
    }

    void CarregouComuns(Scene cena, LoadSceneMode mode)
    {
        ComunsCarregado();
    }

    void ComunsCarregado()
    {
        fase = FasesDoLoad.carregando;


        a2 = new AsyncOperation[cenasParaCarregar.Length];
        for (int i = 0; i < a2.Length; i++)
        {
            a2[i] = SceneManager.LoadSceneAsync(cenasParaCarregar[i].ToString(), LoadSceneMode.Additive);
        }
        Time.timeScale = 0;

        SceneManager.sceneLoaded -= CarregouComuns;
       SceneManager.sceneLoaded += SetarCenaPrincipal;
    }

    /*
    IEnumerator Status()
    {
        yield return new WaitForEndOfFrame();
        RecolocadorDeStatus.VerificaStatusDosAtivos();
    }*/

    void SetarCenaPrincipal(Scene scene, LoadSceneMode mode)
    {
        podeIr = true;
        InvocarSetScene(scene);
        SceneManager.sceneLoaded -= SetarCenaPrincipal;

        //Seria interessante um Publish de ume vento
    }

    IEnumerator SetarScene(Scene scene)
    {
        yield return new WaitForSeconds(0.5f);
        InvocarSetScene(scene);
    }

    public void InvocarSetScene(Scene scene)
    {
        //Debug.Log(scene.name);
        SceneManager.SetActiveScene(scene);
        //Debug.Log(GameController.g+" : "+scene.name);
        if (SceneManager.GetActiveScene() != scene)
            StartCoroutine(SetarScene(scene));

        //Debug.Log("nomeAtiva: " + SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        switch (fase)
        {
            case FasesDoLoad.carregando:
                
                tempo += Time.fixedDeltaTime;

                float progresso = 0;

                for (int i = 0; i < a2.Length; i++)
                {
                    progresso += a2[i].progress;
                }

                progresso /= a2.Length;

                //Debug.Log(progresso + " : " + (tempo / tempoMin) + " : " + Mathf.Min(progresso, tempo / tempoMin, 1));

                loadBar.ValorParaBarra(Mathf.Min(progresso, tempo / tempoMin, 1));

                if (podeIr && tempo >= tempoMin)
                {
                    GameObject go = GameObject.Find("EventSystem");
                    if(go)
                        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName("comunsDeFase"));

                    //FadeView pm = GameController.g.gameObject.AddComponent<FadeView>();
                    FadeView pm = ControladorGlobal.c.gameObject.AddComponent<FadeView>();
                    pm.vel = 2;
                    fase = FasesDoLoad.escurecendo;
                    tempo = 0;
                }
                
            break;
            case FasesDoLoad.escurecendo:
                tempo += Time.fixedDeltaTime;
                if (tempo > 0.95f)
                {
                    GameObject.FindObjectOfType<FadeView>().entrando = false;
                    FindObjectOfType<Canvas>().enabled = false;
                    fase = FasesDoLoad.clareando;
                    SceneManager.SetActiveScene(
                       SceneManager.GetSceneByName(cenasParaCarregar[0].ToString()));
                    //InformacoesDeCarregamento.FacaModificacoes();
                    //GameController.g.Salvador.SalvarAgora();
                    Time.timeScale = 1;
                    SceneManager.UnloadSceneAsync(NomesCenas.CenaDeCarregamento.ToString());
                    tempo = 0;
                }
            break;
            case FasesDoLoad.clareando:
                tempo += Time.fixedDeltaTime;
                if (tempo > 0.5f)
                {
                    
                    Destroy(gameObject);
                }
            break;
        }
    }
}

public enum NomesCenas
{
    nula = -1,
    inicial,
    Tutorial,
    CenaDeCarregamento,
    compraEquip_plus,
    contadoraDePontos_plus,
    Creditos,
    equipamentos_plus,
    escolheCabecudinho_plus,
    niveis_plus,
    nossosPatrocinadores,
    novoTitulo,
    Perfil,
    PreJogo,
    recompensas_plus,
    Sobre
}
