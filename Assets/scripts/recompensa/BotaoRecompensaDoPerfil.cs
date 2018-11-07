using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BotaoRecompensaDoPerfil : MonoBehaviour
{
    [SerializeField]private Text numRecompensas;

    private bool iniciou = false;
    private RectTransform me;

    // Use this for initialization
    void OnEnable()
    {
        if (ControladorGlobal.c != null)
        {
            int num  = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.Recompensas.Count;
            if (num > 0)
                numRecompensas.text = num.ToString();
            else
                numRecompensas.enabled = false;
            iniciou = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!iniciou)
            OnEnable();
    }
    public void BotaoRecompensas()
    {
        SceneManager.LoadScene("recompensas_plus");
    }

    private void OnGUI()
    {
        
        int w = 320;
        int h = 240;
        if (me == null)
            me = GetComponent<RectTransform>();

        Rect R = TamanhoDoUI.RectSize(me);

        if (GUI.Button(R, "", ((GUISkin)Resources.Load("meuSkin")).button))
        {
            BotaoRecompensas();
        }

        GUIStyle tempStyle = new GUIStyle(((GUISkin)Resources.Load("meuSkin")).label);
        tempStyle.fontSize = 7;
        tempStyle.alignment = TextAnchor.UpperCenter;
        tempStyle.font = (Font)Resources.Load("MYRIADPRO-BOLDIT");


        GUI.Label(new Rect(R.x + 0.01f * w, R.y + 0.01f * h, R.width - 0.02f * w, R.height - 0.02f * h), "Premios", tempStyle);


        
        if ( numRecompensas.enabled)
        {
            tempStyle.alignment = TextAnchor.LowerCenter;
            tempStyle.fontSize = 10;
            GUI.Label(new Rect(R.x + 0.01f * w, R.y + 0.01f * h, R.width - 0.02f * w, R.height - 0.02f * h), numRecompensas.text, tempStyle);
        }
    }
}
