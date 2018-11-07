using UnityEngine;
using System.Collections;

[System.Serializable]
public class EquipamentoBase
{
    [SerializeField]private string nomeEquipamento = "Não Setado Corretamente";
    [SerializeField]private TiposDeEquipamento tipo;
    [SerializeField]private int nivelDoEquipamento = 1;
    [SerializeField]private int custoBaseParaNivel = 45;
    [SerializeField]private float taxaDeModificacaoDoEquipamento = 0.1f;
    [SerializeField]private float taxaDeEvolucaoDaModificacao = 0.4f;
    [SerializeField]private bool estaEquipado = false;

    public int NivelDoEquipamento
    {
        get { return nivelDoEquipamento; }
        set { nivelDoEquipamento = value; }
    }

    public int PercentagemDeMod
    {
        get { return (int)(TaxaDeModificacaoDoEquipamento * 100); }
    }

    public int CustoParaNivel
    {
        get { return (int)(custoBaseParaNivel * Mathf.Pow(2,nivelDoEquipamento-1)); }
    }

    public int ValorDeVenda
    {
        get { return Mathf.Max((int)(CustoParaNivel * 0.1f), 1); }
    }

    public int ProximoValorDeModificacao
    {
        get { return (int)((1 + taxaDeEvolucaoDaModificacao) * taxaDeModificacaoDoEquipamento*100); }
    }

    public float TaxaDeModificacaoDoEquipamento
    {
        get { return taxaDeModificacaoDoEquipamento; }
        set { taxaDeModificacaoDoEquipamento = value; }
    }

    public string NomeEquipamento
    {
        get { return nomeEquipamento; }
        set { nomeEquipamento = value; }
    }

    public bool EstaEquipado
    {
        get { return estaEquipado; }
        set { estaEquipado = value; }
    }

    public TiposDeEquipamento Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }

    public float TaxaDeEvolucaoDaModificacao
    {
        get { return taxaDeEvolucaoDaModificacao; }
        set { taxaDeEvolucaoDaModificacao = value; }
    }

    public virtual void EfeitoDoEquipamento()
    {
        Debug.Log("Deve ser sobreposto pela implementação do equipamento");
    }
}

public enum TiposDeEquipamento
{
    nulo,
    anelMaisMoeda,
    anelMaisEstamina,
    anelMaisCheckCombos,
    anelMaisEsferas,
    anelMenosCustoDeEsfera,
    anelEspecialMaisPotente,

    //anelMaisX2,
    //anelMaisTempoDeX2,

    //Aneis de uso Unico
    anelVidaExtra,
    anelMagnetico,//Moedas douradas são atraidas [Poderia criar um item magnetico]
    anelMaisDefesa,
    anelMaisAtaque,
    anelMaisTempoDeCombo
}
