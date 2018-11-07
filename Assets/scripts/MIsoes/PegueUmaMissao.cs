using UnityEngine;
using System.Collections;

public static class PegueUmaMissao
{
    public static float TaxaInicialDaMissao(TipoMissao tipo)
    {
        float M = 1;
        switch (tipo)
        {
            case TipoMissao.coleteMoedas:
            case TipoMissao.coleteEstaminas:
            case TipoMissao.coleteEsferas:
            case TipoMissao.coleteCheckCombos:
            case TipoMissao.derroteInimigos:
            case TipoMissao.somePontuacao:
            case TipoMissao.passeDeNivel:
                M = 1;
            break;

            case TipoMissao.moedasEmUnicoJogo:                
            case TipoMissao.estaminasEmUnicoJogo:
            case TipoMissao.esferasEmUnicoJogo:
            case TipoMissao.checkCombosEmUnicoJogo:
            case TipoMissao.pontuacaoEmUnicoJogo:
            case TipoMissao.alcanceCombo:
            case TipoMissao.alcanceNivel:
                M = 0.5f;
            break;

            case TipoMissao.inimigosEmUnicoJogo:
                M = 0.75f;
            break; 
            default:
                Debug.Log("tipo de missão não incluido ao case");
            break;
        }

        return M;
    }
    public static Missoes Missao(TaxaDeMissao taxa)
    {
        Missoes M = new Missoes();

        switch (taxa.Tipo)
        {
            case TipoMissao.coleteMoedas:
                M = new ColeteMoedas(taxa.Level);
            break;
            case TipoMissao.moedasEmUnicoJogo:
                M = new MoedasEmUnicoJogo(taxa.Level);
            break;
            case TipoMissao.coleteEstaminas:
                M = new ColeteEstaminas(taxa.Level);
            break;
            case TipoMissao.estaminasEmUnicoJogo:
                M = new EstaminasEmUnicoJogo(taxa.Level);
            break;
            case TipoMissao.coleteEsferas:
                M = new ColeteEsferas(taxa.Level);
            break;
            case TipoMissao.esferasEmUnicoJogo:
                M = new EsferasEmUnicoJogo(taxa.Level);
            break;
            case TipoMissao.coleteCheckCombos:
                M = new ColeteCheckCombos(taxa.Level);
            break;
            case TipoMissao.checkCombosEmUnicoJogo:
                M = new CheckCombosEmUnicoJogo(taxa.Level);
            break;
            case TipoMissao.derroteInimigos:
                M = new DerroteInimigos(taxa.Level);
            break;
            case TipoMissao.inimigosEmUnicoJogo:
                M = new InimigosEmUnicoJogo(taxa.Level);
            break;
            case TipoMissao.somePontuacao:
                M = new SomePontuacao(taxa.Level);
            break;
            case TipoMissao.pontuacaoEmUnicoJogo:
                M = new PontuacaoEmUnicoJogo(taxa.Level);
            break;
            case TipoMissao.passeDeNivel:
                M = new PasseDeNivel(taxa.Level);
            break;
            case TipoMissao.alcanceCombo:
                M = new AlcanceCombo(taxa.Level);
            break;
            case TipoMissao.alcanceNivel:
                M = new AlcanceNivel(taxa.Level);
            break;
            
            default:
                Debug.Log("tipo de missão não incluido ao case");                
            break;
        }

        return M;

    }
    
}
