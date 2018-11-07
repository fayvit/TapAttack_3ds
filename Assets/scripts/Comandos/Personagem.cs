using UnityEngine;
using System.Collections;

[System.Serializable]
public class Personagem
{
    [SerializeField]private string nomeDoPersonagem = "Short Azul";
    [SerializeField]private bool bloqueado = true;
    [SerializeField]private int custoDeDesbloqueio = 30;
    [SerializeField]private HabilidadePersonagem habilidade = HabilidadePersonagem.tempoDeCombo;
    [SerializeField]private BonusDePersonagem bonus = BonusDePersonagem.moedasEspecial;

    private int nivelDaHabilidade = 1;

    private int valorInicialDaHabilidade = 10;
    private int valorCorrenteDaHabilidade = 10;
    private float taxaDaEvolucaoDaHabilidade = 1.25f;    

    private int custoInicialDaHabilidade = 33;
    private int custoCorrenteDaHabilidade = 33;
    private float taxaDaEvolucaoDoCustoDaHabilidade = 2;

    

    public string NomeDoPersonagem
    {
        get { return nomeDoPersonagem; }
        set { nomeDoPersonagem = value; }
    }

    public HabilidadePersonagem Habilidade
    {
        get { return habilidade; }
        set { habilidade = value; }
    }

    public BonusDePersonagem Bonus
    {
        get { return bonus; }
        set { bonus = value; }
    }

    public int NivelDaHabilidade
    {
        get { return nivelDaHabilidade; }
        set { nivelDaHabilidade = value; }
    }

    public int ValorInicialDaHabilidade
    {
        get { return valorInicialDaHabilidade; }
        set { valorInicialDaHabilidade = value; }
    }

    public int ValorCorrenteDaHabilidade
    {
        get { return valorCorrenteDaHabilidade; }
        set { valorCorrenteDaHabilidade = value; }
    }

    public int ProximoValorParaHabilidade
    {
        get { return (int)(taxaDaEvolucaoDaHabilidade * valorCorrenteDaHabilidade); }
    }

    public float TaxaDaEvolucaoDaHabilidade
    {
        get { return taxaDaEvolucaoDaHabilidade; }
        set { taxaDaEvolucaoDaHabilidade = value; }
    }

    public int CustoInicialDaHabilidade
    {
        get { return custoInicialDaHabilidade; }
        set { custoInicialDaHabilidade = value; }
    }

    public int CustoCorrenteDaHabilidade
    {
        get { return custoCorrenteDaHabilidade; }
        set { custoCorrenteDaHabilidade = value; }
    }

    public float TaxaDaEvolucaoDoCustoDaHabilidade
    {
        get { return taxaDaEvolucaoDoCustoDaHabilidade; }
        set { taxaDaEvolucaoDoCustoDaHabilidade = value; }
    }

    public bool Bloqueado
    {
        get { return bloqueado; }
        set { bloqueado = value; }
    }

    public int CustoDeDesbloqueio
    {
        get { return custoDeDesbloqueio; }
        set { custoDeDesbloqueio = value; }
    }

    public int ModDoTempoDeCombo
    {
        get{
            if (habilidade == HabilidadePersonagem.tempoDeCombo)
                return valorCorrenteDaHabilidade;
            else
                return 0;
        }
    }

    public int ModDoGanhoDePontosPorInimigo
    {
        get {
            if (habilidade == HabilidadePersonagem.pontoPorInimigo)
                return valorCorrenteDaHabilidade;
            else
                return 0;
        }
    }

    public int ModDosPontosPorCombo
    {
        get {
            if (habilidade == HabilidadePersonagem.pontoDeCombo)
                return valorCorrenteDaHabilidade;
            else
                return 0;
        }
    }
}

public enum HabilidadePersonagem
{
    tempoDeCombo,
    pontoPorInimigo,
    pontoDeCombo
}

public enum BonusDePersonagem
{
    moedasEspecial,
    checkComboEspecial,
    naoPerdeCheckCombo,
    maisAtaque,
    vidaExtra,
    esferasDeFogo
}
