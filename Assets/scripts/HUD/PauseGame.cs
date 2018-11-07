using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseGame : MonoBehaviour
{
    [SerializeField] Button botaoDeEspecial;
    [SerializeField] GameObject painelDePause;
    [SerializeField] Button esseBotao;

    [SerializeField]Text Tvd;//TextoDaMissaoVerde
    [SerializeField]Text Tvm;//TextoDaMIssaoVermelha

    [SerializeField]PainelDeConfirmacao painelDeConfirmacao;

    private bool preencheuTextosDasMissoes = false;
    // Use this for initialization
    void Start()
    {
        if (ControladorGlobal.c != null)
        {
            Missoes[] Ms = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.GMissoes.MissoesAtuais;
            if (Ms != null)
                if (Ms.Length > 0)
                {
                    if (Tvd != null)
                    {
                        MeDigaMinhaMissao.TextosDeMissao(Tvd, Tvm, Ms);
                        preencheuTextosDasMissoes = true;
                    }
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!preencheuTextosDasMissoes)
        {
            Start();
        }
    }

    public void SimEuQueroVoltar()
    {
        Time.timeScale = 1;
        ControladorGlobal.c.Musicas.ReiniciarMusicas();
        SceneManager.LoadScene("PreJogo");
    }

    public void NaoQuero()
    {
        ModificadorDoContainerPrincipal.ReligarBotoes(painelDePause);
    }

    public void SimEuQueroReiniciar()
    {
        Time.timeScale = 1;
        ControladorGlobal.c.Musicas.ReiniciarMusicas(true);
        SceneManager.LoadScene("inicial");
    }

    public void ReiniciarJogo()
    {
        ModificadorDoContainerPrincipal.DesligarBotoes(painelDePause);
        painelDeConfirmacao.AtivarPainelDeConfirmacao(
            SimEuQueroReiniciar,
            NaoQuero, 
            BancoDeTextos.TextosDoIdioma(ChavesDeTexto.certezaDeReiniciarJogo)
            );

        
    }

    public void VoltarAoTitulo()
    {
        ModificadorDoContainerPrincipal.DesligarBotoes(painelDePause);
        painelDeConfirmacao.AtivarPainelDeConfirmacao(
            SimEuQueroVoltar,
            NaoQuero,
            BancoDeTextos.TextosDoIdioma(ChavesDeTexto.certezaVoltarAoTitulo)
            );

        
    }

    public void BotaoDePause()
    {
        if (!ControladorDeJogo.c.Pause)
        {
            painelDePause.SetActive(true);

            if(botaoDeEspecial)
                botaoDeEspecial.interactable = false;

            esseBotao.interactable = false;
            ControladorDeJogo.c.Pause = true;
            Time.timeScale = 0;

            ControladorGlobal.c.Musicas.PararMusicas();
        }
    }

    public void BotaoVoltarAoJogo()
    {
        if (ControladorDeJogo.c.Pause)
        {
            painelDePause.SetActive(false);
            if(botaoDeEspecial)
                botaoDeEspecial.interactable = true;
            esseBotao.interactable = true;
            ControladorDeJogo.c.Pause = false;
            Time.timeScale = 1;
            GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().ApComandos.RetornarDoPause();

            ControladorGlobal.c.Musicas.ReiniciarMusicas();
        }
    }

    public void BotaoMissaoCumprida()
    {
        ControladorGlobal.c.EmJogo.Pontuacao = ControladorDeJogo.c.G_Pontos.PontosTotais;
        ControladorGlobal.c.EmJogo.Nivel = GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().Dados.NivelParaMostrador;
        ModificadorDoContainerPrincipal.DesligarBotoes(painelDePause);
        painelDeConfirmacao.AtivarPainelDeConfirmacao(
            CertezaMissaoCumprida,
            NaoQuero,
            "Are you sure you want to end this game and count your points?"
            );
    }

    void CertezaMissaoCumprida()
    {
        ControladorGlobal.c.Musicas.ReiniciarMusicas();
        Time.timeScale = 1;
        SceneManager.LoadScene("contadoraDePontos_plus");
    }

    private void OnGUI()
    {
        if (!ControladorDeJogo.c.Pause)
        {
            Rect R = default(Rect);
            GUISkin skin = (GUISkin)Resources.Load("meuSkin");
            int w = 320;
            int h = 240;

            R = new Rect(0.01f * w, 0.02f * h, 0.09f * w, 0.11f * h);


            if (GUI.Button(R, "", skin.button))
            {
                BotaoDePause();
            }

            GUIStyle tempStyle = new GUIStyle(skin.box);
            Texture2D texturaSacana = (Texture2D)(Resources.Load("pause"));
            tempStyle.normal.background = texturaSacana;
            tempStyle.hover.background = texturaSacana;
            tempStyle.active.background = texturaSacana;

            R = new Rect(0.02f * w, 0.03f * h, 0.07f * w, 0.09f * h);

            GUI.Box(R, "", tempStyle);
        }
    }
}
