using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ModificadorDoContainerPrincipal
{

    public static void ZerarDrop(Dropdown drop, string texto)
    {
        drop.value = 0;
        drop.options = new List<Dropdown.OptionData>()
        {new  Dropdown.OptionData() { text = texto } };
    }

    public static void AtualizaDropDown(Dropdown drop, DadosGlobais dadosGlobais)
    {
        drop.options = new List<Dropdown.OptionData>();

        for (int i = 0; i < dadosGlobais.Perfis.Count; i++)
            drop.options.Add(new Dropdown.OptionData() { text = dadosGlobais.Perfis[i].NomeDoPerfil });

        drop.value = dadosGlobais.IndiceDoPerfilSelecionado;
        drop.captionText.text = dadosGlobais.PerfilAtualSelecionado.NomeDoPerfil;
    }

    public static void DesligarBotoes(GameObject container)
    {
        Button[] Bs = container.GetComponentsInChildren<Button>();

        foreach (Button B in Bs)
            B.interactable = false;
    }

    public static void DesligarComponentesDoPrincipal(GameObject containerDoLayoutPrincipal,Dropdown drop)
    {

        DesligarBotoes(containerDoLayoutPrincipal);
        drop.interactable = false;
    }

    public static void ReligarBotoes(GameObject G)
    {

        Button[] Bs = G.GetComponentsInChildren<Button>();

        foreach (Button B in Bs)
            B.interactable = true;
    }

    public static void ReligarComponentesDoPrincipal(GameObject containerDoLayoutPrincipal,DadosGlobais dadosGlobais,Button botaoIniciarJogo,Dropdown drop)
    {

        ReligarBotoes(containerDoLayoutPrincipal);

        if (dadosGlobais.Perfis.Count == 0)
        {
            botaoIniciarJogo.interactable = false;
        }

        drop.interactable = true;
    }
}
