using UnityEngine;
using System;
using System.Collections.Generic;
class EstrelaDeCristal : Coletavel
{
    private static List<GameObject> estrelasEmCampo = new List<GameObject>();

    public static List<GameObject> EstrelasEmCampo
    {
        get { return estrelasEmCampo; }
        set { estrelasEmCampo = value; }
    }

    public static bool PodeSpawnarMaisEstrela()
    {
        bool retorno = true;
        Dictionary<string, int> estrelasPorDia = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.EstrelasPorDia;
        string hoje = DateTime.Now.ToString("dd/MM/yyyy");

        VerificaObjetoNuloEmLista.RetiraObjetosNulos(EstrelasEmCampo);
        
        if (estrelasPorDia.ContainsKey(hoje))
        {
            if (EstrelasEmCampo.Count + estrelasPorDia[hoje] >= 5)
                retorno = false;
        }
        else if (EstrelasEmCampo.Count >= 5)
            retorno = false;

        return retorno && !JaForamCincoHoje();
    }
    protected override GameObject ParticulaDeColetavelEAcaoColetavel(DadosDoPersonagem dados)
    {        
        ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.EstrelasDeCristal++;

        Dictionary<string, int> x = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.EstrelasPorDia;
        string data = DateTime.Now.ToString("dd/MM/yyyy");
         
        if (x.ContainsKey(data))
            x[data]++;
        else
            x[data] = 1;

        ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
        return ControladorDeJogo.c.RetornaElemento(Elementos.parMoeda);
    }

    public static int NumeroDeEstrelasHoje
    {
        get {
            int retorno = 0;
            Dictionary<string, int> x = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.EstrelasPorDia;
            string data = DateTime.Now.ToString("dd/MM/yyyy");

            if (x.ContainsKey(data))
                retorno = x[data];

            return retorno;
        }
    }
    

    public static bool JaForamCincoHoje()
    {
        bool retorno = false;
        Dictionary<string, int> estrelasPorDia = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.EstrelasPorDia;
        if (estrelasPorDia.ContainsKey(DateTime.Now.ToString("dd/MM/yyyy")))
        {
            if(estrelasPorDia[DateTime.Now.ToString("dd/MM/yyyy")]>=5)                
                retorno = true;
        }

        return retorno;
    }
}

public class VerificaObjetoNuloEmLista
{
    public static List<int> VerificaIndicesDeNulos(List<GameObject> lista)
    {
        List<int> retorno = new List<int>();

        for (int i = 0; i < lista.Count; i++)
            if (lista[i] == null)
                retorno.Add(i);

        return retorno;
    }

    public static void RetiraObjetosNosIndices(List<GameObject> lista, List<int> indices)
    {
        for (int i = indices.Count; i > 0; i--)
        {
            lista.RemoveAt(indices[i-1]);
        }
    }

    public static void RetiraObjetosNulos(List<GameObject> lista)
    {
        RetiraObjetosNosIndices(lista, VerificaIndicesDeNulos(lista));
    }
}

