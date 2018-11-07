using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MostrarEquipadosParaTroca : MonoBehaviour
{
    [SerializeField]private Text textoDeInfosDoEquipamento;
    [SerializeField]private Image[] imagensDosEquipamentos;
    [SerializeField]private Outline[] destaqueDoSelecionado;
    [SerializeField]private int selecionado = 0;
    [SerializeField]private PainelUmaMensagem umaMensagem;
    [SerializeField]private PainelDeConfirmacao confirmacao;
    [SerializeField]private BotoesAtualizaveis btns;

    [System.Serializable]
    private class BotoesAtualizaveis
    {
        [SerializeField]public Button btnMelhorar;
        [SerializeField]public Button btnVender;
        [SerializeField]public Text textoDoBotaoMelhorar;
        [SerializeField]public Text textoDoBotaoVender;
    }

    private SloteDeEquipamento[] slotes;
    // Use this for initialization
    void Start()
    {
         slotes = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.Slotes;
        for (int i = 0; i < 3; i++)
        {
            if (slotes[i].Desbloqueado)
            {
                if (slotes[i].Preenchido)
                    imagensDosEquipamentos[i].sprite = SpriteDeEquipamento.s.RetornaSprite(
                        slotes[i].EquipamentoNoSlote.Tipo
                        );
                else
                    imagensDosEquipamentos[i].sprite = SpriteDeEquipamento.s.RetornaSprite("plus");
            }
            else
            {
                imagensDosEquipamentos[i].sprite = SpriteDeEquipamento.s.RetornaSprite("cadeado 1");
            }

            AtualizaSlotes();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MostrarBtnsDeCompraEVenda()
    {
        btns.btnMelhorar.interactable = true;
        btns.btnVender.interactable = true;
        btns.textoDoBotaoMelhorar.text = MelhoraEquipamento.TextoDeMelhora(slotes[selecionado].EquipamentoNoSlote);
        btns.textoDoBotaoVender.text = "Vender esse equipamento por: \r\n" + slotes[selecionado].EquipamentoNoSlote.ValorDeVenda + " Moedas";

    }

    void AtualizaSlotes()
    {
        for (int i = 0; i < 3; i++)
        {
            if (selecionado == i)
            {
                if (slotes[i].Preenchido)
                {
                    EquipamentoBase equip = slotes[i].EquipamentoNoSlote;
                    textoDeInfosDoEquipamento.text = equip.NomeEquipamento + ":\r\n"
                        + string.Format(BancoDeTextos.TextosDoIdioma("descricaoEquip" + equip.Tipo), equip.PercentagemDeMod);

                    if(slotes[i].EquipamentoNoSlote.NivelDoEquipamento>0)
                        MostrarBtnsDeCompraEVenda();
                    else
                        DesabilitarBotoesDeCompra();

                }
                else
                {
                    DesabilitarBotoesDeCompraEVenda();
                }

                destaqueDoSelecionado[i].enabled = true;
            }
            else
            {
                destaqueDoSelecionado[i].enabled = false;
            }
        }
    }

    void DesabilitarBotoesDeCompra()
    {
        btns.btnMelhorar.interactable = false;
        btns.textoDoBotaoMelhorar.text = "Melhorar";

        if(btns.btnVender!=null)
            btns.btnVender.interactable = true;

        if (btns.btnVender != null)
        {
            if (slotes[selecionado].Preenchido)
                btns.textoDoBotaoVender.text = "Vender esse equipamento por: \r\n"
                    + slotes[selecionado].EquipamentoNoSlote.ValorDeVenda + " Moedas";
            else
                btns.btnVender.interactable = false;
        }
    }

    void DesabilitarBotoesDeCompraEVenda()
    {
        DesabilitarBotoesDeCompra();
        if (btns.btnVender != null)
        {
            btns.btnVender.interactable = false;
            btns.textoDoBotaoVender.text = "Vender";
        }
        textoDeInfosDoEquipamento.text = "Clique em um anel para adiciona-lo ao slote";
    }

    public void ColoqueEquipamentoNoSlote(EquipamentoBase equip)
    {
        bool continua = true;
        for (int i = 0; i < 3; i++)
        {
            if(slotes[i].EquipamentoNoSlote!=null)
                if (slotes[i].EquipamentoNoSlote.Tipo == equip.Tipo && i!=selecionado)
                {
                    ModificadorDoContainerPrincipal.DesligarBotoes(transform.parent.gameObject);
                    umaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes, "Você não pode equipar dois aneis com o mesmo efeito");
                    continua = false;
                }
        }


        if (continua)
        {
            if (slotes[selecionado].Preenchido)
            {
                slotes[selecionado].EquipamentoNoSlote.EstaEquipado = false;
            }

            slotes[selecionado].EquipamentoNoSlote = equip;
            slotes[selecionado].Preenchido = true;
            imagensDosEquipamentos[selecionado].sprite = SpriteDeEquipamento.s.RetornaSprite(equip.Tipo);
            

            if (slotes[selecionado].EquipamentoNoSlote.NivelDoEquipamento > 0)
                MostrarBtnsDeCompraEVenda();
            else
                DesabilitarBotoesDeCompra();

            textoDeInfosDoEquipamento.text = equip.NomeEquipamento + ":\r\n"
                + string.Format(BancoDeTextos.TextosDoIdioma("descricaoEquip" + equip.Tipo), equip.PercentagemDeMod);

            equip.EstaEquipado = true;

            FindObjectOfType<ControladorDaHUD_Equipamentos>().AtualizaSelecionados();

            ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
        }
    }

    public void BotaoSlote(int numSlote)
    {
        if (slotes[numSlote].Desbloqueado)
        {
            selecionado = numSlote;

            AtualizaSlotes();
        }
    }

    public void BotaoMelhorar()
    {
        if (MelhoraEquipamento.Melhorar(
            slotes[selecionado].EquipamentoNoSlote,
            umaMensagem,
            transform.parent.gameObject,
            ReligarBotoes))
        {
            FindObjectOfType<ControladorDaHUD_Equipamentos>().AtualizaNivelDoEquip(slotes[selecionado].EquipamentoNoSlote);
            AtualizaSlotes();
        }
    }

    void VenderSim()
    {
        Perfil P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        int indiceDoEquip = P.MeusEquipamentos.IndexOf(slotes[selecionado].EquipamentoNoSlote);

        P.Dinheiro += slotes[selecionado].EquipamentoNoSlote.ValorDeVenda;

        P.MeusEquipamentos.Remove(slotes[selecionado].EquipamentoNoSlote);
        FindObjectOfType<ControladorDaHUD_Equipamentos>().VendiItem(indiceDoEquip);
        slotes[selecionado].EquipamentoNoSlote = new EquipamentoBase();
        slotes[selecionado].Preenchido = false;
        
        imagensDosEquipamentos[selecionado].sprite = SpriteDeEquipamento.s.RetornaSprite("plus");

        ReligarBotoes();

        
        ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
    }

    public void ReligarBotoes()
    {
        ModificadorDoContainerPrincipal.ReligarBotoes(transform.parent.gameObject);
        AtualizaSlotes();
    }

    public void BotaoVender()
    {
        ModificadorDoContainerPrincipal.DesligarBotoes(transform.parent.gameObject);
        confirmacao.AtivarPainelDeConfirmacao(VenderSim, ReligarBotoes, "Você tem certeza que deseja Vender esse equipamento");
    }
}
