using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EdicaoDePerfil_HUD : EditarPerfil_Base
{
    [SerializeField]private Dropdown drop;
        
    // Use this for initialization

    protected override void OnEnable()
    {
        base.OnEnable();
        ModificadorDoContainerPrincipal.AtualizaDropDown(drop, dados);
        input.text = dados.PerfilAtualSelecionado.NomeDoPerfil;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override int IndiceDoPerfilSelecionado()
    {
        return drop.value;
    }
    protected override void AtualizacoesEspecificasDaTrocaDeNome(int esse)
    {
        ModificadorDoContainerPrincipal.AtualizaDropDown(drop, dados);
        InicializacaoDePerfil.I.AtualizaDropDown();
        drop.value = esse;
    }

    protected override void AlteracoesEspecificasAoDeletar()
    {
        if (dados.Perfis.Count > 0)
        {
            AtualizaOsDropDown();
        }
        else
        {
            AtualizaComDropZerado();
        }
    }

    void AtualizaOsDropDown()
    {
        ModificadorDoContainerPrincipal.AtualizaDropDown(drop, dados);
        input.text = dados.PerfilAtualSelecionado.NomeDoPerfil;
        InicializacaoDePerfil.I.AtualizaDropDown();
    }

    void AtualizaComDropZerado()
    {
        InicializacaoDePerfil.I.ZerarDrop();
        painelUmaMensagem.retornar += BotaoRetornar;
    }
    
    public void BotaoRetornar()
    {
        InicializacaoDePerfil.I.ReligarComponentesEspecificos();
        gameObject.SetActive(false);
    }

    public void MudouDropDown(int qual)
    {
        input.text = dados.Perfis[qual].NomeDoPerfil;
    }
}
