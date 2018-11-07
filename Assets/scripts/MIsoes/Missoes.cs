using UnityEngine;
using System.Collections;

[System.Serializable]
public class Missoes
{
    [SerializeField]    private TipoMissao tipo;
    [SerializeField]    private int meta;
    [SerializeField]    private int tentativas = 0;

    [SerializeField]    protected int alcancado = 0;

    protected int MetaPeloLevel(int metaBase,int level,float taxa)
    {
        return (int)((1 + (level - 1) * taxa) * metaBase);
    }
    public TipoMissao Tipo
    {
        get { return tipo; }
        protected set { tipo = value; }
    }

    public int Meta
    {
        get { return meta; }
        protected set { meta = value; }
    }

    public int Tentativas
    {
        get { return tentativas; }
        set { tentativas = value; }
    }

    public virtual void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        Debug.LogWarning("SomaAlcancado precisa ser sobrecarregado");
    }

    public virtual int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        Debug.LogWarning("Mostra Soma precisa ser sobrecarregado");
        return -1;
    }
    public virtual bool AlcancouAMeta()
    {
        if (alcancado >= Meta)
            return true;
        else
            return false;
    }

    public virtual bool VerifiqueSeComIssoBatoAMeta(ContainerDosDadosEmJogo dados)
    {
        SomaAlcancado(dados);
        return AlcancouAMeta();
    }
}

public enum TipoMissao
{
    coleteMoedas,
    moedasEmUnicoJogo,
    coleteEstaminas,
    estaminasEmUnicoJogo,
    coleteEsferas,
    esferasEmUnicoJogo,
    coleteCheckCombos,
    checkCombosEmUnicoJogo,
    derroteInimigos,
    inimigosEmUnicoJogo,
    somePontuacao,
    pontuacaoEmUnicoJogo,
    passeDeNivel,
    alcanceCombo,
    alcanceNivel
}
