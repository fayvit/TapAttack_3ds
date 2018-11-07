using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class NovaHUD_Equipamentos : MonoBehaviour
{
    [SerializeField]private Image[] imagensDosEquipamentos;

    private bool foi;
    // Use this for initialization
    void Start()
    {
        if (!foi && ControladorGlobal.c != null)
        {
            SloteDeEquipamento[] slotes = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.Slotes;
            for (int i = 0; i < imagensDosEquipamentos.Length; i++)
            {
                if (!slotes[i].Desbloqueado)
                {
                    imagensDosEquipamentos[i].sprite = SpriteDeEquipamento.s.RetornaSprite("cadeado 1");
                    imagensDosEquipamentos[i].GetComponent<Button>().interactable = false;
                }else
                if (slotes[i].Preenchido)
                {
                    imagensDosEquipamentos[i].sprite = SpriteDeEquipamento.s.RetornaSprite(slotes[i].EquipamentoNoSlote.Tipo);
                }
                else
                    imagensDosEquipamentos[i].sprite = SpriteDeEquipamento.s.RetornaSprite("plus");
            }

            foi = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Start();
    }

    public void BotaoEquipamentos()
    {
        SceneManager.LoadScene("equipamentos_plus");
    }

    private void OnGUI()
    {
        int w = 320;
        int h = 320;
        GUIStyle tempStyle = new GUIStyle(((GUISkin)Resources.Load("meuSkin")).box);
        Texture2D texturaSacana = imagensDosEquipamentos[0].sprite.texture;
        tempStyle.normal.background = texturaSacana;
        tempStyle.hover.background = texturaSacana;
        tempStyle.active.background = texturaSacana;

        for (int i = 0; i < 3; i++)
        {
            texturaSacana = imagensDosEquipamentos[i].sprite.texture;
            if (GUI.Button(new Rect((0.035f +i*0.16f)*w, 0.1f * h, 0.12f * w, 0.12f * h), "", ((GUISkin)Resources.Load("meuSkin")).button))
            {
                BotaoEquipamentos();
            }


            GUI.Box(new Rect((0.035f  + i * 0.16f)*w, 0.1f * h, 0.12f * w, 0.12f * h), "", tempStyle);
        }
    }
}
