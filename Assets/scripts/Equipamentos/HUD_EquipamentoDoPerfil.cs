using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HUD_EquipamentoDoPerfil : MonoBehaviour
{
    [SerializeField]private Image[] iconesDeMais;
    [SerializeField]private GameObject[] iconesDeCadeado;
    // Use this for initialization
    void OnEnable()
    {
        SloteDeEquipamento[] slotes = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.Slotes;
        for (int i = 0; i < slotes.Length; i++)
        {
            if (slotes[i].Desbloqueado)
            {
                iconesDeCadeado[i].SetActive(false);
                

                if (slotes[i].Preenchido)
                {
                    iconesDeMais[i].gameObject.SetActive(true);
                    iconesDeMais[i].sprite = SpriteDeEquipamento.s.RetornaSprite(slotes[i].EquipamentoNoSlote.Tipo);
                }else
                    iconesDeMais[i].gameObject.SetActive(true);
            }
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < 3; i++)
        {
            iconesDeCadeado[i].SetActive(true);
            iconesDeMais[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BotaoEquipamento()
    {
        SceneManager.LoadScene("equipamentos");
    }
}
