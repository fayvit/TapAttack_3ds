using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GerenciadorDeHUD : MonoBehaviour
{
    public RectTransform imgEstamina;
    public RectTransform imgVida;
    public RectTransform imgXp;
    public RectTransform imgEspecial;
    public RectTransform imgTempoCombo;
    public RawImage faltouEstamina;
    public RawImage temEspecial;
    public Text txtDinheiro;
    public Text txtEstrela;
    public Text txtNivel;
    public Text txtEstamina;
    public Text txtXP;
    public Text contadorDePontosCorrente;
    public Text contadorDePontosEmCombo;
    public Text contadorDePontosSomados;
    public Text xCombos;
    public Text multiplicadorAtualDePontos;

    public GameObject containerDoContadorDeCombos;
    public GameObject containerDePOntosDeCombos;

    public static PiscarBarra piscaEstamina;

    private PiscarBarra piscaEspecial;
    private float posOriginalMaxDaAncora;
    private float posOriginalMinDaAncora;
    private float posOriginalMaxDaAncoraCombo;
    private float posOriginalMinDaAncoraCombo;

    private DadosDoPersonagem dados;

    [SerializeField]private GerenciadorDoContainerDasMissoes gC_Missoes;
    // Use this for initialization
    void Start()
    {

        
        piscaEspecial = new PiscarBarra(temEspecial, 5);
        if (gameObject.name != "lowerCanvas")
        {
            piscaEstamina = new PiscarBarra(faltouEstamina);
            posOriginalMaxDaAncora = imgEstamina.anchorMax.x;
            posOriginalMinDaAncora = imgEstamina.anchorMin.x;
        }

        posOriginalMaxDaAncoraCombo = imgTempoCombo.anchorMax.y;
        posOriginalMinDaAncoraCombo = imgTempoCombo.anchorMin.y;

        dados = GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().Dados;
    }

    // Update is called once per frame
    void Update()
    {
        HUD_Combos();

        if (gameObject.name != "lowerCanvas")
        {
            HUD_Pontos();
            piscaEstamina.PiscarComTempo();

            txtXP.text = dados.G_XP.XP + " / " + dados.G_XP.ParaProxNivel;
            txtEstamina.text = dados.EstaminaCorrente.ToString();
            txtNivel.text = "NV: " + dados.NivelParaMostrador;
            ControladorGlobal.c.EmJogo.Nivel = dados.NivelParaMostrador;
            txtDinheiro.text = "x" + dados.Dinheiro;

            PercentagemDeBarraNoY(imgEstamina, ((dados.EstaminaCorrente + dados.EstaminaPeloTempo()) / dados.EstaminaMax));
            PercentagemDeBarraNoY(imgVida, (float)dados.VidaCorrente / dados.VidaMax);
            PercentagemDeBarraNoY(imgXp,
                ((float)dados.G_XP.XP - dados.G_XP.UltimoPassaNivel) / (dados.G_XP.ParaProxNivel - dados.G_XP.UltimoPassaNivel));
            PercentagemDeBarraNoY(imgEspecial, ((float)dados.CristaisEspeciais / dados.CristaisParaAtivar));

            
        }
        
        gC_Missoes.AtualizaMisoes();

        Color C = temEspecial.color;

        if (dados.CristaisEspeciais >= dados.CristaisParaAtivar)
            piscaEspecial.PiscarSemTempo();
        else
            temEspecial.color = new Color(C.r, C.g, C.b, 0);

        if (txtEstrela!=null)
            txtEstrela.text = "x"+EstrelaDeCristal.NumeroDeEstrelasHoje.ToString()+"/5";        

        
    }

    void PercentagemDeBarraNoY(RectTransform barra, float percentagem)
    {
        barra.anchorMax = new Vector2(
            (posOriginalMaxDaAncora - posOriginalMinDaAncora) * percentagem + posOriginalMinDaAncora,
            barra.anchorMax.y
            );
        }

    void PercentagemDeBarraNoX(RectTransform barra, float percentagem)
    {
        barra.anchorMax = new Vector2(
            barra.anchorMax.x,
            (posOriginalMaxDaAncoraCombo - posOriginalMinDaAncoraCombo) * percentagem + posOriginalMinDaAncoraCombo
            );
    }

    void HUD_Combos()
    {
        GerenciadorDeCombos gCombo = ControladorDeJogo.c.G_Combos;
        if (gCombo.ContadorDoCombo > 0)
        {
            containerDoContadorDeCombos.SetActive(true);
            xCombos.text = "x" + gCombo.ContadorDoCombo.ToString();
            PercentagemDeBarraNoX(imgTempoCombo, gCombo.PercentagemDeTempoParaFimDoCombo);
        }else
            containerDoContadorDeCombos.SetActive(false);
    }

    void HUD_Pontos()
    {
        GerenciadorDeCombos gCombo = ControladorDeJogo.c.G_Combos;
        GerenciadorDePontos gPontos = ControladorDeJogo.c.G_Pontos;
        contadorDePontosCorrente.text = gPontos.PontosTotais.ToString();
        multiplicadorAtualDePontos.text = "("+gCombo.ModDoMultiplicadorDePontos+"x)";

        if (gCombo.ContadorDoCombo > 0)
        {
            containerDePOntosDeCombos.SetActive(true);
            contadorDePontosEmCombo.text = gCombo.PontosPorAdicionar.ToString();
            contadorDePontosSomados.text = 
                ((int)(gCombo.PontosPorAdicionar*gCombo.ModDoMultiplicadorDePontos) + gPontos.PontosTotais).ToString();
            ControladorGlobal.c.EmJogo.Pontuacao = (gCombo.PontosPorAdicionar + gPontos.PontosTotais);

        }
        else
            containerDePOntosDeCombos.SetActive(false);

    }

    public void DisparaEspecial()
    {
        if (dados.CristaisEspeciais >= dados.CristaisParaAtivar)
        {
            dados.ZeraCristais();
            ControladorDeJogo.c.DisparaEspecial();            
        }
    }

    private void OnGUI()
    {
        if (dados.CristaisEspeciais >= dados.CristaisParaAtivar && name == "lowerCanvas"
            &&
            !ControladorDeJogo.c.Pause
            )
        {
            Rect R = default(Rect);
            GUISkin skin = (GUISkin)Resources.Load("meuSkin");
            int w = 320;
            int h = 240;

            R = new Rect(0.01f * w, 0.86f * h, 0.09f * w, 0.11f * h);


            if (GUI.Button(R, "", skin.button))
            {
                DisparaEspecial();
            }

            GUIStyle tempStyle = new GUIStyle(skin.box);
            Texture2D texturaSacana = (Texture2D)(Resources.Load("raio"));
            tempStyle.normal.background = texturaSacana;
            tempStyle.hover.background = texturaSacana;
            tempStyle.active.background = texturaSacana;

            R = new Rect(0.02f * w, 0.87f * h, 0.07f * w, 0.09f * h);

            GUI.Box(R, "", tempStyle);

           
        }

    }
}
