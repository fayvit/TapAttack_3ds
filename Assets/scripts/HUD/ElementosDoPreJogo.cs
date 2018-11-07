using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ElementosDoPreJogo : MonoBehaviour
{
    [SerializeField]private Text nomeDoPerfil;
    [SerializeField]private Text numeroDeMoedas;
    [SerializeField]private Text numeroDeEstrelas;
    [SerializeField] private GameObject btnTutorial;

    //private bool foi = false;
    // Use this for initialization
    void Start()
    {
        //if (ControladorGlobal.c != null && !foi)
       // {
           // foi = true;

            Perfil P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
            //CameraDosCabecudinhos.c.EscolheCabecudinho(P.IndiceDoPersonagemSelecionado);
            nomeDoPerfil.text = P.NomeDoPerfil;
            numeroDeEstrelas.text = P.EstrelasDeCristal.ToString();
            numeroDeMoedas.text = P.Dinheiro.ToString();

        if(btnTutorial!=null)
            btnTutorial.SetActive(ControladorGlobal.c.DadosGlobais.fizTutorial);

        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Start();
    }

    public void BotaoHeroi()
    {
        SceneManager.LoadScene("escolheCabecudinho_plus");
    }

    public void BotaoJogar()
    {
        //AdsManager ads = FindObjectOfType<AdsManager>();
        //if(ads)
            //ads.DepoisDoTitulo();

        if (ControladorGlobal.c.DadosGlobais.fizTutorial)
            SceneManager.LoadScene("inicial");
        else
            SceneManager.LoadScene("Tutorial");
    }

    public void BotaoTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void BotaoVoltar()
    {
        SceneManager.LoadScene("Perfil");
    }

    /*
    private void OnGUI()
    {
        Rect R = default(Rect);
        GUISkin skin = (GUISkin)Resources.Load("meuSkin 1");
        int w = 320;
        int h = 240;
        
        R = new Rect(0.81f * w, 0.82f * h, 0.16f * w, 0.15f * h);
        

        if (GUI.Button(R, "Jogar", skin.button))
        {
            BotaoJogar();
        }

        skin = (GUISkin)Resources.Load("meuSkin");
        R = new Rect(0.615f * w, 0.86f * h, 0.18f * w, 0.1f * h);

        if (GUI.Button(R, "Voltar", skin.button))
        {
            BotaoVoltar();
        }

        R = new Rect(0.34f * w, 0.88f * h, 0.2f * w, 0.1f * h);

        if (GUI.Button(R, "Herois", skin.button))
        {
            BotaoHeroi();
        }
    }*/
}
