using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DadosGlobais
{
    [SerializeField]private List<Perfil> perfis = new List<Perfil>();
    [SerializeField]private int perfilAtualSelecionado = 0;
    [SerializeField]private bool perfilDeTesteDeCena = false;
    public bool fizTutorial = false;


    public Perfil PerfilAtualSelecionado
    {
        get {
            if (perfilAtualSelecionado < 0)
                perfilAtualSelecionado = 0;

            return Perfis[perfilAtualSelecionado];
        }
    }

    public int IndiceDoPerfilSelecionado
    {
        get { return perfilAtualSelecionado; }
    }

    public List<Perfil> Perfis
    {
        get { return perfis; }
    }

    public void SalvarSeNaoForTesteDeCena()
    {
        if (!perfilDeTesteDeCena)
            SaveGame.Save(this);
    }

    public void CriarUmPerfilDeTesteParaCena()
    {
        perfis.Add(new Perfil() { NomeDoPerfil="Perfil para teste de cena"});
        perfilDeTesteDeCena = true;
    }

    public void CriarNovoPerfil(Perfil novo)
    {
        perfis.Add(novo);
    }

    public void DeletarUmPerfil(Perfil oDeletado)
    {
        int indiceDoDeletado = perfis.IndexOf(oDeletado);
        if (indiceDoDeletado == IndiceDoPerfilSelecionado && perfis.Count > 1)
            perfilAtualSelecionado = 0;
        else if (indiceDoDeletado <= IndiceDoPerfilSelecionado)
            perfilAtualSelecionado--;

        perfis.Remove(oDeletado);
    }

    public void SelecionarPerfil(int indice)
    {
        perfilAtualSelecionado = indice;
       
    }

    public void ZerarJogosSeguidos()
    {
        for (int i = 0; i < Perfis.Count; i++)
            Perfis[i].JogosSeguidos = 0;
    }
}


