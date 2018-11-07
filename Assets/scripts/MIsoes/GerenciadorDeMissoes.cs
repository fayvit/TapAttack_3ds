using UnityEngine;
using System.Collections;

[System.Serializable]
public class GerenciadorDeMissoes
{
    [SerializeField]private Missoes[] missoesAtuais = new Missoes[0];
    [SerializeField]private EscolhaDeMissao escolhas = new EscolhaDeMissao();

    public Missoes[] MissoesAtuais
    {
        get { return missoesAtuais; }
    }

    public EscolhaDeMissao Escolhas
    {
        get { return escolhas; }
    }

    public int LevelDeEscolhaDeMissao(TipoMissao tipo)
    {
        int retorno = 1;
        for (int i = 0; i < escolhas.ListaDeTaxas.Count; i++)
        {
            if (escolhas.ListaDeTaxas[i].Tipo == tipo)
                retorno = escolhas.ListaDeTaxas[i].Level;
        }

        return retorno;
    }

    public void SetarMissoes()
    {
        if (escolhas.ListaDeTaxas.Count == 0)
            escolhas = new EscolhaDeMissao();

        if (missoesAtuais.Length == 0)
            missoesAtuais = new Missoes[2];

        int cont = 0;
        for (int i = 0; i < 2; i++)
        {
            cont = 0;
            do
            {               
                cont++;
                if (MissoesAtuais[i] == null)
                    MissoesAtuais[i] = escolhas.SelecionarUmaMissao();
                else if (MissoesAtuais[i].AlcancouAMeta())
                    MissoesAtuais[i] = escolhas.SelecionarUmaMissao();
                    
            } while (MissoesSaoIguais() && cont < 100);
            //Debug.Log("fiz isso tantas vezes " + cont);
        }
    }

    bool MissoesSaoIguais()
    {
        if (MissoesAtuais[0] == null || MissoesAtuais[1] == null)
            return false;
        else
            return MissoesAtuais[0].Tipo == MissoesAtuais[1].Tipo;
    }

    public void InserirMissaoVencida()
    {
        Debug.Log(missoesAtuais+" : "+missoesAtuais.Length);

        if (missoesAtuais.Length==0)
            missoesAtuais = new Missoes[2] {
                (PegueUmaMissao.Missao(new TaxaDeMissao() { Tipo = TipoMissao.alcanceCombo })),
                (PegueUmaMissao.Missao(new TaxaDeMissao() { Tipo = TipoMissao.alcanceCombo })) };

        missoesAtuais[0].Tentativas = 19;

    }
}
