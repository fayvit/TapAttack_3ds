using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EditarPerfil_Base : MonoBehaviour
{

    [SerializeField]protected PainelUmaMensagem painelUmaMensagem;
    [SerializeField]protected PainelDeConfirmacao painelMensagemConfirmacao;
    [SerializeField]protected InputField input;
    protected DadosGlobais dados;

    protected virtual void OnEnable()
    {
        if (dados == null)
            dados = ControladorGlobal.c.DadosGlobais;

        input.text = dados.PerfilAtualSelecionado.NomeDoPerfil;
    }
        // Use this for initialization
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual int IndiceDoPerfilSelecionado()
    {
        return -1;
    }

    protected virtual void AlteracoesEspecificasAoDeletar()
    {

    }

    protected virtual void AtualizacoesEspecificasDaTrocaDeNome(int esse)
    {

    }
    

    public void BotaoAlterarPerfil()
    {
        int indice = IndiceDoPerfilSelecionado();
        if (dados.Perfis[indice].NomeDoPerfil == input.text)
        {
            painelUmaMensagem.gameObject.SetActive(true);
            painelUmaMensagem.AtualizarTextoDaMensagem(BancoDeTextos.TextosDoIdioma(ChavesDeTexto.nomesIguais));
            painelUmaMensagem.retornar += ReligarBotoes;
            ModificadorDoContainerPrincipal.DesligarBotoes(gameObject);
        }
        else if (string.IsNullOrEmpty(input.text))
        {
            painelUmaMensagem.gameObject.SetActive(true);
            painelUmaMensagem.AtualizarTextoDaMensagem(BancoDeTextos.TextosDoIdioma(ChavesDeTexto.nomeDoPerfilNulo));
            painelUmaMensagem.retornar += ReligarBotoes;
            ModificadorDoContainerPrincipal.DesligarBotoes(gameObject);
        }
        else
        {
            ModificadorDoContainerPrincipal.DesligarBotoes(gameObject);
            painelMensagemConfirmacao.gameObject.SetActive(true);
            painelMensagemConfirmacao.AlteraTextoDoPainel(
                string.Format(
                BancoDeTextos.TextosDoIdioma(ChavesDeTexto.temCertezaQueQuerTrocarNome),
                dados.Perfis[indice].NomeDoPerfil,
                input.text
                )
                );
            painelMensagemConfirmacao.botaoNao += ReligarBotoes;
            painelMensagemConfirmacao.botaoSim += TrocarNome;
        }
    }

    void TrocarNome()
    {
        int esse = IndiceDoPerfilSelecionado();
        dados.Perfis[esse].NomeDoPerfil = input.text;

        AtualizacoesEspecificasDaTrocaDeNome(esse);

        painelMensagemConfirmacao.gameObject.SetActive(false);
        painelUmaMensagem.gameObject.SetActive(true);
        painelUmaMensagem.AtualizarTextoDaMensagem(
            BancoDeTextos.TextosDoIdioma(ChavesDeTexto.nomeTrocado)
            );
        painelUmaMensagem.retornar += ReligarBotoes;
        dados.SalvarSeNaoForTesteDeCena();
        input.text = "";
    }

    void ReligarBotoes()
    {
        ModificadorDoContainerPrincipal.ReligarBotoes(gameObject);
    }

    void NegarDeletar()
    {
        ReligarBotoes();
        painelMensagemConfirmacao.gameObject.SetActive(false);
    }


    void AceitarDeletar()
    {
        int indice = IndiceDoPerfilSelecionado();

        print(IndiceDoPerfilSelecionado() + " : " + dados.Perfis.Count);
        string nomeDoPerfilDeletado = dados.Perfis[indice].NomeDoPerfil;
        dados.DeletarUmPerfil(dados.Perfis[indice]);

        AlteracoesEspecificasAoDeletar();

        painelMensagemConfirmacao.gameObject.SetActive(false);
        painelUmaMensagem.gameObject.SetActive(true);
        painelUmaMensagem.retornar += ReligarBotoes;
        painelUmaMensagem.AtualizarTextoDaMensagem(string.Format(
            BancoDeTextos.TextosDoIdioma(ChavesDeTexto.deletouOPerfil),
            nomeDoPerfilDeletado
            ));
        dados.SalvarSeNaoForTesteDeCena();

    }

    public void BotaoDeletar()
    {
        painelMensagemConfirmacao.AlteraTextoDoPainel("Tem certeza que deseja excluir o perfil");
        painelMensagemConfirmacao.gameObject.SetActive(true);
        painelMensagemConfirmacao.botaoNao += NegarDeletar;
        painelMensagemConfirmacao.botaoSim += AceitarDeletar;
        ModificadorDoContainerPrincipal.DesligarBotoes(gameObject);
    }
}
