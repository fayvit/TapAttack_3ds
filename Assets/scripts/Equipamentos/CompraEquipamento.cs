using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CompraEquipamento : MonoBehaviour
{

    [SerializeField]private PainelDeConfirmacao confirma;
    [SerializeField]private PainelUmaMensagem umaMensagem;
    [SerializeField]private RecebiAlgo recebido;
    [SerializeField]private Text txtMoedas;
    [SerializeField]private Text txtEstrelas;

    private Perfil P;

    private const int VALOR_DO_EQUIPOAMENTO_DEFINITIVO = 13;
    private const int VALOR_DO_EQUIPOAMENTO_DE_USO_UNICO = 100;
    // Use this for initialization
    void Start()
    {
        P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        AtualizaValores();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AtualizaValores()
    {
        txtMoedas.text= P.Dinheiro.ToString();
        txtEstrelas.text = P.EstrelasDeCristal.ToString();
    }

    void ReligarBotoes()
    {
        ModificadorDoContainerPrincipal.ReligarBotoes(gameObject);
    }

    void FinalisaCompra(EquipamentoBase equip)
    {
        P.AdicionaEquipamentoNaLita(equip);
        
        AtualizaValores();
        recebido.ConstroiObjeto(ReligarBotoes, equip);
        ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
    }

    void ComprarDefinitivo()
    {
        EquipamentoBase equip = PegueUmEquipamento.SorteiaEquipamentoDefinitivo();
        P.EstrelasDeCristal -= VALOR_DO_EQUIPOAMENTO_DEFINITIVO;
        FinalisaCompra(equip);
        
    }
    void ComprarUsoUnico()
    {
        EquipamentoBase equip = PegueUmEquipamento.SorteiaEquipamentoDeUsoUnico();
        P.Dinheiro -= VALOR_DO_EQUIPOAMENTO_DE_USO_UNICO;
        FinalisaCompra(equip);
    }

    public void BotaoCompraUsoUnico()
    {
        if (P.Dinheiro >= VALOR_DO_EQUIPOAMENTO_DE_USO_UNICO)
        {
            ModificadorDoContainerPrincipal.DesligarBotoes(gameObject);
            confirma.AtivarPainelDeConfirmacao(ComprarUsoUnico, ReligarBotoes,
                "O equipamento de uso único será gerado aleatóriamente, tem certeza que quer compra-lo??");
        }
        else
        {
            ModificadorDoContainerPrincipal.DesligarBotoes(gameObject);
            umaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes, "Você ainda não tem as moedas necessárias");
        }
    }

    public void BotaoCompraDefinitivo()
    {
        if (P.EstrelasDeCristal >= VALOR_DO_EQUIPOAMENTO_DEFINITIVO)
        {
            ModificadorDoContainerPrincipal.DesligarBotoes(gameObject);
            confirma.AtivarPainelDeConfirmacao(ComprarDefinitivo,ReligarBotoes,
                "O equipamento definitivo será gerado aleatóriamente, tem certeza que quer compra-lo??");
        }
        else
        {
            ModificadorDoContainerPrincipal.DesligarBotoes(gameObject);
            umaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes, "Você ainda não tem as estrelas necessárias");
        }
    }

    public void BotaoVoltarAosEquipamentos()
    {
        SceneManager.LoadScene("equipamentos_plus");
    }

    
}
