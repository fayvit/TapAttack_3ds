using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EscolhaDeMissao
{
    [SerializeField]private List<TaxaDeMissao> listaDeTaxas = new List<TaxaDeMissao>();

    private const float TAXA_DE_VARIACAO = 0.03f;
    public EscolhaDeMissao()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(TipoMissao)).Length; i++)
        {
            ListaDeTaxas.Add(
                new TaxaDeMissao()
                {
                    Tipo = (TipoMissao)i,
                    TaxaDeEscolha = PegueUmaMissao.TaxaInicialDaMissao((TipoMissao)i),
                    Level = 1
                });
        }
    }

    public List<TaxaDeMissao> ListaDeTaxas
    {
        get { return listaDeTaxas; }
    }

    public Missoes SelecionarUmaMissao()
    {
        Missoes M =  new Missoes();
        bool foi = false;
        float somaDasTaxas = 0;
        int i;
        for (i = 0; i < listaDeTaxas.Count; i++)
            somaDasTaxas += listaDeTaxas[i].TaxaDeEscolha;

        float sorteado = Random.Range(0, somaDasTaxas);
        somaDasTaxas = 0;

        for (i = 0; i < listaDeTaxas.Count; i++)
        {
            somaDasTaxas += listaDeTaxas[i].TaxaDeEscolha;
            if (sorteado <= somaDasTaxas && !foi)
            {
              
                foi = true;
                M = PegueUmaMissao.Missao(listaDeTaxas[i]);
                AtualizaListaDeTaxas(M,1);
            }
        }
        return M;
    }

    public void AtualizaListaDeTaxas(Missoes aMissao,int tanto)
    {
        for (int i = 0; i < listaDeTaxas.Count; i++)
        {
            if (listaDeTaxas[i].Tipo == aMissao.Tipo)
            {
                listaDeTaxas[i] = new TaxaDeMissao()
                {
                    Level = Mathf.Max(listaDeTaxas[i].Level+tanto,1),
                    Tipo = aMissao.Tipo,
                    TaxaDeEscolha = Mathf.Max(0.01f,
                    listaDeTaxas[i].TaxaDeEscolha - (TAXA_DE_VARIACAO * (listaDeTaxas.Count - 1))                    
                    )
                };
            }
            else
            {
                listaDeTaxas[i] = new TaxaDeMissao()
                {
                    Level = listaDeTaxas[i].Level,
                    Tipo = listaDeTaxas[i].Tipo,
                    TaxaDeEscolha = listaDeTaxas[i].TaxaDeEscolha+TAXA_DE_VARIACAO
                };
            }
        }
    }
}

[System.Serializable]
public struct TaxaDeMissao
{
    [SerializeField]private string tipo;
    [SerializeField]private float taxaDeEscolha;
    [SerializeField]private int level;

    public TipoMissao Tipo
    {
        get { return (TipoMissao)System.Enum.Parse(typeof(TipoMissao), tipo); }
        set {  tipo = value.ToString(); }
    }

    public float TaxaDeEscolha
    {
        get { return taxaDeEscolha; }
        set { taxaDeEscolha = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }
}
