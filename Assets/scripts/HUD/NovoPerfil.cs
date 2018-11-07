using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class NovoPerfil : FuncoesDePerfil
{
    [SerializeField]private Text numeroDeMoedas;
    [SerializeField]private Text numeroDeEstrelas;
    [SerializeField]private Text nomeDoPerfil;
    [SerializeField]private NovoEditorDePerfil nEdit;
    [SerializeField]private GameObject BotoesDeMudarPerfil;
    [SerializeField] private GUISkin skin;
    [SerializeField] private GUISkin skin2;

    int w = 320;
    int h = 240;
        
    // Use this for initialization
    new void Start()
    {
        UnityEngine.N3DS.Keyboard.SetType(N3dsKeyboardType.NNID);

        base.Start();
        VerificaPerfis();
        EventAgregator.AddListener(EventKey.ClickButtonChangeProfile, OnClickButtonChangeProfile);
    }

    private void OnDestroy()
    {
        EventAgregator.RemoveListener(EventKey.ClickButtonChangeProfile, OnClickButtonChangeProfile);
    }

    private void OnClickButtonChangeProfile(IGameEvent obj)
    {
        TemPerfilInicializado();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TemPerfilInicializado()
    {
        Perfil P = dadosGlobais.PerfilAtualSelecionado;

        if (P.NivelMaximoAlcancado > 0)
            dadosGlobais.fizTutorial = true;

        nomeDoPerfil.text = P.NomeDoPerfil;
        numeroDeEstrelas.text = P.EstrelasDeCristal.ToString();
        numeroDeMoedas.text = P.Dinheiro.ToString();

        if(dadosGlobais.Perfis.Count>1 && BotoesDeMudarPerfil!=null)
            BotoesDeMudarPerfil.SetActive(true);

        //CameraDosCabecudinhos.c.EscolheCabecudinho(P.IndiceDoPersonagemSelecionado);
    }

    protected override void DesligarComponentesEspecificos()
    {
        ModificadorDoContainerPrincipal.DesligarBotoes(containerDoLayoutPrincipal);
    }

    public override void ReligarComponentesEspecificos()
    {
        ModificadorDoContainerPrincipal.ReligarBotoes(containerDoLayoutPrincipal);

        if (dadosGlobais.Perfis.Count == 0)
            BotaoAbrirJanelaDeCriarPerfil();
    }

    public void VerificaPerfis()
    {
        if (dadosGlobais.Perfis.Count <= 0)
        {
            nomeDoPerfil.text = "Criar um perfil";
            numeroDeEstrelas.text = "0";
            numeroDeMoedas.text = "0";

            BotaoAbrirJanelaDeCriarPerfil();
        }
        //else
          //  TemPerfilInicializado();

        if (dadosGlobais.Perfis.Count <= 1 && BotoesDeMudarPerfil!=null)
        {
            BotoesDeMudarPerfil.SetActive(false);
        }
    }

    public void BotaoSobre()
    {
        SceneManager.LoadScene("Sobre");
    }

    public void BotaoEditar()
    {
        ModificadorDoContainerPrincipal.DesligarBotoes(containerDoLayoutPrincipal);
        nEdit.gameObject.SetActive(true);
    }

    public void BotaoAvancar()
    {
        SceneManager.LoadScene("PreJogo");
    }

    public void BotaoSair()
    {
        SceneManager.LoadScene("novoTitulo");
    }

    public void BotaoAvancaPerfil()
    {
        EventAgregator.Publish(EventKey.ClickButtonChangeProfile,null);
        if (dadosGlobais.IndiceDoPerfilSelecionado + 1 < dadosGlobais.Perfis.Count)
            dadosGlobais.SelecionarPerfil(
                dadosGlobais.IndiceDoPerfilSelecionado + 1
                );
        else
            dadosGlobais.SelecionarPerfil(0);

        //TemPerfilInicializado();
    }

    public void BotaoRetrocedePerfil()
    {
        if (dadosGlobais.IndiceDoPerfilSelecionado> 0)
            dadosGlobais.SelecionarPerfil(
                dadosGlobais.IndiceDoPerfilSelecionado - 1
                );
        else
            dadosGlobais.SelecionarPerfil(dadosGlobais.Perfis.Count-1);

        //TemPerfilInicializado();
    }

    private void OnGUI()
    {
        if (botaoIniciarJogo != null)
        {
            if (!nEdit.gameObject.activeSelf && !containerDeCriacaoDePerfil.activeSelf && !painelUmaMensagem.gameObject.activeSelf)
            {
                if (dadosGlobais.Perfis.Count > 1)
                {
                    if (GUI.Button(new Rect(0.01f * w, 0.4f * h, 0.1f * w, 0.16f * h), "<<", skin.button))
                    {
                        BotaoRetrocedePerfil();
                    }

                    if (GUI.Button(new Rect(0.89f * w, 0.4f * h, 0.1f * w, 0.16f * h), ">>", skin.button))
                    {
                        BotaoAvancaPerfil();
                    }
                }

                if (GUI.Button(new Rect(0.12f * w, 0.75f * h, 0.22f * w, 0.1f * h), "Sobre", skin.button))
                {
                    BotaoSobre();
                }

                if (GUI.Button(new Rect(0.35f * w, 0.75f * h, 0.22f * w, 0.1f * h), "Editar", skin.button))
                {
                    BotaoEditar();
                }

                if (GUI.Button(new Rect(0.62f * w, 0.7f * h, 0.25f * w, 0.15f * h), "Avançar", skin2.button))
                {
                    BotaoAvancar();
                }

                if (GUI.Button(new Rect(0.11f * w, 0.87f * h, 0.26f * w, 0.12f * h), "Sair", skin.button))
                {
                    BotaoSair();
                }

                if (GUI.Button(new Rect(0.4f * w, 0.87f * h, 0.26f * w, 0.12f * h), "Novo", skin.button))
                {
                    BotaoAbrirJanelaDeCriarPerfil();
                }
            }
            else if (containerDeCriacaoDePerfil.activeSelf && !painelUmaMensagem.gameObject.activeSelf)
            {
                if (GUI.Button(new Rect(0.06f * w, 0.1f * h, 0.22f * w, 0.1f * h), "Sair", skin.button))
                {
                    BotaoCancelarCriacaoDePerfil();
                }

                if (GUI.Button(new Rect(0.5f * w, 0.62f * h, 0.32f * w, 0.15f * h), "Criar", skin2.button))
                {
                    BotaoCriarPerfil();
                }

                N3dsKeyboardResult result = UnityEngine.N3DS.Keyboard.GetResult();
                GUI.Label(new Rect(0, 220, 60, 20), result.ToString());

                string inputString = UnityEngine.N3DS.Keyboard.GetText();
                GUI.Label(new Rect(70, 220, 250, 20), inputString);

                /*
                if (T == null)
                    T = GameObject.Find("CriarPerfil").GetComponent<Text>();*/

                GUI.TextField(new Rect(0.1f * w, 0.43f * h, 0.7f * w, 0.1f * h), "",skin.textField);
                if(result==N3dsKeyboardResult.Okay)
                    nomeNoNovoPerfil.text = inputString;
                /*
                if (UnityEngine.N3DS.Keyboard.GetResult() == (int)N3dsKeyboardResult.Okay)
                {
                    N3dsKeyboardResult.
                    string text = UnityEngine.N3DS.Keyboard.GetText();
                    T.text = text;
                    nomeDoPerfil.text = text;
                }*/
                //N3dsKeyboardResult result = UnityEngine.N3DS.Keyboard.GetResult();
                
                //nomeDoPerfil.text = UnityEngine.N3DS.Keyboard.GetText();
                //nomeDoPerfil.text = result.ToString();


            }
        }
    }

    Text T;



}
