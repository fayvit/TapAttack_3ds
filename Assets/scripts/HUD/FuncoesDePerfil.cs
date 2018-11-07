using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FuncoesDePerfil : MonoBehaviour
{
    [SerializeField]protected Button botaoIniciarJogo;
    [SerializeField]protected GameObject containerDoLayoutPrincipal;
    [SerializeField]protected GameObject containerDeCriacaoDePerfil;
    [SerializeField]protected InputField nomeNoNovoPerfil;
    [SerializeField]protected PainelUmaMensagem painelUmaMensagem;

    protected DadosGlobais dadosGlobais;
    // Use this for initialization
    protected void Start()
    {
        dadosGlobais = ControladorGlobal.c.DadosGlobais;

        if (dadosGlobais.Perfis.Count > 0)
        {
            TemPerfilInicializado();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void TemPerfilInicializado()
    {
       
    }

    protected virtual void DesligarComponentesEspecificos()
    {

    }

    protected virtual void AtualizaComponentesEspecificos()
    {

    }

    public virtual void ReligarComponentesEspecificos()
    {
        /*
        ModificadorDoContainerPrincipal.ReligarComponentesDoPrincipal(
            containerDoLayoutPrincipal,
            dadosGlobais,
            botaoIniciarJogo,
            drop);*/
    }

    public void BotaoAbrirJanelaDeCriarPerfil()
    {
        containerDeCriacaoDePerfil.SetActive(true);
        nomeNoNovoPerfil.text = string.Empty;
        ModificadorDoContainerPrincipal.ReligarBotoes(containerDeCriacaoDePerfil);
        DesligarComponentesEspecificos();
    }

    public void BotaoCriarPerfil()
    {
        if (!string.IsNullOrEmpty(nomeNoNovoPerfil.text))
        {
            CriarPerfil(nomeNoNovoPerfil.text);
            dadosGlobais.SelecionarPerfil(dadosGlobais.Perfis.Count - 1);
            dadosGlobais.SalvarSeNaoForTesteDeCena();
            AtualizaComponentesEspecificos();
            ModificadorDoContainerPrincipal.DesligarBotoes(containerDeCriacaoDePerfil);
            
            painelUmaMensagem.gameObject.SetActive(true);
            painelUmaMensagem.retornar += new PainelUmaMensagem.RetornarParaAntecessor(BotaoCancelarCriacaoDePerfil);
            painelUmaMensagem.AtualizarTextoDaMensagem(
                string.Format(
                BancoDeTextos.TextosDoIdioma(ChavesDeTexto.perfilCriado),
                nomeNoNovoPerfil.text
                )
                );

            TemPerfilInicializado();
        }
        else
        {
            ModificadorDoContainerPrincipal.DesligarBotoes(containerDeCriacaoDePerfil);
            painelUmaMensagem.gameObject.SetActive(true);
            painelUmaMensagem.retornar += new PainelUmaMensagem.RetornarParaAntecessor(MensagemPerfilPrecisaDeString);
            painelUmaMensagem.AtualizarTextoDaMensagem(
                BancoDeTextos.TextosDoIdioma(ChavesDeTexto.nomeDoPerfilNulo)
                );
        }

    }

    void CriarPerfil(string nomeDoPerfil)
    {
        Perfil novo = new Perfil() { NomeDoPerfil = nomeDoPerfil };
        dadosGlobais.CriarNovoPerfil(novo);
        botaoIniciarJogo.interactable = true;
    }

    public void BotaoCancelarCriacaoDePerfil()
    {

        containerDeCriacaoDePerfil.SetActive(false);
        ReligarComponentesEspecificos();
    }

    public void MensagemPerfilPrecisaDeString()
    {
        ModificadorDoContainerPrincipal.ReligarBotoes(containerDeCriacaoDePerfil);
    }
}
