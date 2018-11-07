using UnityEngine;
using System.Collections;

[System.Serializable]
public class TentativasExcedidas
{
    [SerializeField] private PainelUmaMensagem umaMensagem;
    [SerializeField] private PainelDeConfirmacao confirmacao;

    private GameObject pai;
    private PainelDeConfirmacao.Confirmacao finalisar;
    private int qualFoi = 0;
    private Missoes[] Ms;


    public void MostrarTentativasExcedidas(GameObject pai,PainelDeConfirmacao.Confirmacao finalisar)
    {
        if (this.finalisar == null)
            this.finalisar += finalisar;

        this.pai = pai;

        Ms = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.GMissoes.MissoesAtuais;

        if (qualFoi > Ms.Length - 1)
        {
            finalisar();
            return;
        }

        if (Ms[qualFoi].Tentativas == 20 || (Ms[qualFoi].Tentativas > 20 && Ms[qualFoi].Tentativas % 10 == 0))
        {
            string s = string.Format(
            "Você atingiu 20 tentativas de completar a missão {0}, gostaria de desistir dessa missão",
            string.Format(BancoDeTextos.TextosDoIdioma(
            (ChavesDeTexto)System.Enum.Parse(typeof(ChavesDeTexto), "indicativoDaMissao" + Ms[qualFoi].Tipo.ToString())),
            Ms[qualFoi].Meta
            ));

            ModificadorDoContainerPrincipal.DesligarBotoes(pai);
            confirmacao.AtivarPainelDeConfirmacao(sim, nao, s);
            confirmacao.AlteraTextoDoBotaoSim("quero desistir");
            confirmacao.AlteraTextoDoBotaoNao("quero tentar mais");
        }
        else
        {
            qualFoi++;
            MostrarTentativasExcedidas(pai, finalisar);
        }

        

        
    }

    void sim()
    {
        EscolhaDeMissao e = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.GMissoes.Escolhas;
        e.AtualizaListaDeTaxas(Ms[qualFoi], -1);
        Ms[qualFoi] = e.SelecionarUmaMissao();
        qualFoi++;
        MostrarTentativasExcedidas(pai, finalisar);
    }

    void nao()
    {
        qualFoi++;
        umaMensagem.ConstroiPainelUmaMensagem(VaiDeNovo, "Se após mais 10 tentativas ainda não tiver conseguido, perguntaremos de novo");
    }

    void VaiDeNovo()
    {
        MostrarTentativasExcedidas(pai, finalisar);
    }
}
