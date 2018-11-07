using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InicializacaoDePerfil : FuncoesDePerfil
{
    [SerializeField]private Dropdown drop;
    [SerializeField]private GameObject containerDoPerfil;
    [SerializeField]private GameObject containerDeEdicaoDoPerfil;
    

    public static InicializacaoDePerfil I;

    
    // Use this for initialization
    new void Start()
    { 
        I = this;

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TemPerfilInicializado()
    {
        botaoIniciarJogo.interactable = true;
        AtualizaDropDown();
    }

    protected override void AtualizaComponentesEspecificos()
    {
        ModificadorDoContainerPrincipal.AtualizaDropDown(drop, dadosGlobais);
    }

    public void AtualizaDropDown()
    {
        ModificadorDoContainerPrincipal.AtualizaDropDown(drop, dadosGlobais);
    }

    public void SelecionarPerfil(int num)//função do DropDown
    {
        if (dadosGlobais.Perfis.Count == 0)
        {
            BotaoAbrirJanelaDeCriarPerfil();
            drop.Hide();
        }
        else if (num != -1)
            dadosGlobais.SelecionarPerfil(num);        
    }
    protected override void DesligarComponentesEspecificos()
    {
        ModificadorDoContainerPrincipal.DesligarComponentesDoPrincipal(
            containerDoLayoutPrincipal, drop
            );
    }

    public override void ReligarComponentesEspecificos()
    {
        ModificadorDoContainerPrincipal.ReligarComponentesDoPrincipal(
            containerDoLayoutPrincipal,
            dadosGlobais,
            botaoIniciarJogo,
            drop);
    }   

    public void BotaoSairDoJogo()
    {
        Application.Quit();
    }

    public void BotaoAvancar()
    {
        containerDoLayoutPrincipal.SetActive(false);
        containerDoPerfil.SetActive(true);
        PainelDoPerfil.t.AtualizaExibicao(dadosGlobais.PerfilAtualSelecionado);
    }

    public void ZerarDrop()
    {
        ModificadorDoContainerPrincipal.ZerarDrop(drop,BancoDeTextos.TextosDoIdioma(ChavesDeTexto.DropZerado));
    }

    public void ReligarEssePainel()
    {
        containerDoLayoutPrincipal.SetActive(true);
        containerDoPerfil.SetActive(false);
    }

    public void BotaoEdicaoDoPerfil()
    {
        ModificadorDoContainerPrincipal.DesligarComponentesDoPrincipal(containerDoLayoutPrincipal, drop);
        containerDeEdicaoDoPerfil.SetActive(true);
    }
}
