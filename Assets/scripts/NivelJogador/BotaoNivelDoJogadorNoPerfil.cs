using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BotaoNivelDoJogadorNoPerfil : MonoBehaviour
{
    [SerializeField]private Text nivelDoJogador;
    [SerializeField]private RectTransform imagemParaNIvel;
    private bool iniciou = false;

    private float posOriginalMaxDaAncora = 0;
    private float posOriginalMinDaAncora = 0;

    private RectTransform me;
    // Use this for initialization
    void Start()
    {
        me = GetComponent<RectTransform>();
    }
    void OnEnable()
    {
        if (ControladorGlobal.c != null)
        {
            IGerenciadorDeExperiencia gXP = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.NivelJogador;
            nivelDoJogador.text = gXP.Nivel.ToString();
            iniciou = true;

            if (imagemParaNIvel != null)
            {
                if (posOriginalMaxDaAncora == 0)
                {
                    posOriginalMaxDaAncora = imagemParaNIvel.anchorMax.x;
                    posOriginalMinDaAncora = imagemParaNIvel.anchorMin.x;
                }
                PercentagemDeBarraNoY(imagemParaNIvel, ((float)gXP.XP - gXP.UltimoPassaNivel) / (gXP.ParaProxNivel - gXP.UltimoPassaNivel));
            }
           
            
           
        }
    }

    void PercentagemDeBarraNoY(RectTransform barra, float percentagem)
    {
        barra.anchorMax = new Vector2(
            (posOriginalMaxDaAncora - posOriginalMinDaAncora) * percentagem + posOriginalMinDaAncora,
            barra.anchorMax.y
            );
    }

    void OnDisable()
    {
        iniciou = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!iniciou)
            OnEnable();
    }

    public void PassouDeNivel()
    {
        nivelDoJogador.text = "+++";
        nivelDoJogador.color = Color.red;
        
    }

    public void BotaoNivelJogador()
    {
        SceneManager.LoadScene("niveis_plus");
    }

    private void OnGUI()
    {
        Rect R = default(Rect);
        int w = 320;
        int h = 240;
        R = TamanhoDoUI.RectSize(me);

        if (GUI.Button(R, "", ((GUISkin)Resources.Load("meuSkin")).button))
        {
            BotaoNivelJogador();
        }

        GUIStyle tempStyle = new GUIStyle(((GUISkin)Resources.Load("meuSkin")).label);
        tempStyle.fontSize = 7;
        tempStyle.alignment = TextAnchor.UpperCenter;
        tempStyle.font = (Font)Resources.Load("MYRIADPRO-BOLDIT");


        GUI.Label(new Rect(R.x + 0.01f * w, R.y + 0.01f * h, R.width - 0.02f * w, R.height - 0.02f * h), "Nivel de Jogador",tempStyle);
        tempStyle.alignment = TextAnchor.LowerCenter;
        tempStyle.fontSize = 10;
        GUI.Label(new Rect(R.x + 0.01f * w, R.y + 0.01f * h, R.width - 0.02f * w, R.height - 0.02f * h), nivelDoJogador.text, tempStyle);
    }
}
