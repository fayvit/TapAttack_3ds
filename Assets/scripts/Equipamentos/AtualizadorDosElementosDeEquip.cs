using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AtualizadorDosElementosDeEquip : MonoBehaviour
{

    [SerializeField]private Text sempreOuNao;
    [SerializeField]private Text nivelDoEquip;
    [SerializeField]private Image imagemDoNivel; 
    [SerializeField]private Image imagemDoEquipamento;    
    [SerializeField]private Image imagemDeSelecao;

    private EquipamentoBase equip;
    private MostrarEquipadosParaTroca most;
    public void AtualizaElementos(int i)
    {
        equip = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.MeusEquipamentos[i];

        if (equip.NivelDoEquipamento < 1)
        {
            sempreOuNao.text = "uso único";
            nivelDoEquip.enabled = false;
            imagemDoNivel.enabled = false;
        }
        else
        {
            sempreOuNao.text = "para sempre";
            nivelDoEquip.text = "nivel " + equip.NivelDoEquipamento;
        }

        if (equip.EstaEquipado)
            imagemDeSelecao.color = new Color(1, 0.35f, 0);//Color.cyan;

        imagemDoEquipamento.sprite = SpriteDeEquipamento.s.RetornaSprite(equip.Tipo);
    }

    public void BotatoCliqueiNoEquipamento()
    {
        if (most == null)
            most = FindObjectOfType<MostrarEquipadosParaTroca>();

        if (most == null)
            Debug.LogError("O script mostrador de equipamentos não foi encontrado");
        else
        {
            most.ColoqueEquipamentoNoSlote(equip);
        }
    }

    public void EstouEquipado()
    {
        if(equip!=null)
            imagemDeSelecao.color = equip.EstaEquipado? new Color(1, 0.35f, 0)/*Color.cyan;*/:Color.black;
    }
}
