using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorDaHUD_Equipamentos : MonoBehaviour
{
    [SerializeField]private int quantidadeEmUmaLinha = 5;
    [SerializeField]private RectTransform containerDeTamanhoVariavel;
    [SerializeField]private GameObject itemDoContainer;
    [SerializeField]private Text numDinheiro;
    [SerializeField]private Text numEstrelas;

    private Perfil P;

    // Use this for initialization
    void Start()
    {
        RecalculaTamanhoDoContainer();
        P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        for (int i = 0; i <P.MeusEquipamentos.Count; i++)
        {
            ParentearNaHUD.Parentear(itemDoContainer, containerDeTamanhoVariavel)
            .GetComponent<AtualizadorDosElementosDeEquip>().AtualizaElementos(i);
        }

        AtualizaMoeda();

        itemDoContainer.SetActive(false);
        itemDoContainer.transform.SetAsLastSibling();
    }

    void AtualizaMoeda()
    {
        numDinheiro.text = P.Dinheiro.ToString();
        numEstrelas.text = P.EstrelasDeCristal.ToString();
    }

    void RecalculaTamanhoDoContainer()
    {
        int numeroDeLinhas = (ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.MeusEquipamentos.Count / quantidadeEmUmaLinha) + 1;
        containerDeTamanhoVariavel.sizeDelta
                    = new Vector2(0,Mathf.Max(numeroDeLinhas *(itemDoContainer.GetComponent<LayoutElement>().preferredHeight
                    +containerDeTamanhoVariavel.GetComponent<GridLayoutGroup>().spacing.y),
                    containerDeTamanhoVariavel.sizeDelta.y));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AtualizaNivelDoEquip(EquipamentoBase equip)
    {
        AtualizaMoeda();
        int numItem = P.MeusEquipamentos.IndexOf(equip);
        containerDeTamanhoVariavel.GetChild(numItem).GetComponent<AtualizadorDosElementosDeEquip>().AtualizaElementos(numItem);
    }

    public void AtualizaSelecionados()
    {
        for (int i = 0; i < containerDeTamanhoVariavel.childCount; i++)
        {
            AtualizadorDosElementosDeEquip atEquip =  containerDeTamanhoVariavel.GetChild(i).GetComponent<AtualizadorDosElementosDeEquip>();
            if (atEquip)
                atEquip.EstouEquipado();
            else
                Debug.Log("O que houve"+transform.childCount);
        }
    }

    public void VendiItem(int numItem)
    {
        Transform T = containerDeTamanhoVariavel.GetChild(numItem);
        T.gameObject.SetActive(false);
        T.SetAsLastSibling();
        RecalculaTamanhoDoContainer();
        AtualizaMoeda();
    }

    public void BotaoComprarMaisEquipamentos()
    {
        SceneManager.LoadScene("compraEquip_plus");
    }
}
