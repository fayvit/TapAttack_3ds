using UnityEngine;
using System.Collections;

public class PegueUmEquipamento
{
    public static EquipamentoBase SorteiaEquipamentoDefinitivo()
    {
        TiposDeEquipamento[] possibilidades = new TiposDeEquipamento[6] {
            TiposDeEquipamento.anelMaisMoeda,
            TiposDeEquipamento.anelEspecialMaisPotente,
            TiposDeEquipamento.anelMaisEstamina,
            TiposDeEquipamento.anelMaisCheckCombos,
            TiposDeEquipamento.anelMaisEsferas,
            TiposDeEquipamento.anelMenosCustoDeEsfera};

        return UmEquipamento(possibilidades[Random.Range(0,possibilidades.Length)]);
    }

    public static EquipamentoBase SorteiaEquipamento()
    {
        int deQual = Random.Range(0, 1);
        if (deQual == 0)
            return SorteiaEquipamentoDefinitivo();
        else
            return SorteiaEquipamentoDeUsoUnico();
    }

    public static EquipamentoBase SorteiaEquipamentoDeUsoUnico()
    {
        TiposDeEquipamento[] possibilidades = new TiposDeEquipamento[5] {
            TiposDeEquipamento.anelMaisAtaque,
            TiposDeEquipamento.anelMaisDefesa,
            TiposDeEquipamento.anelMagnetico,
            TiposDeEquipamento.anelMaisTempoDeCombo,
            TiposDeEquipamento.anelVidaExtra
        };
        return UmEquipamento(possibilidades[Random.Range(0, possibilidades.Length)]);
    }


    public static EquipamentoBase UmEquipamento(TiposDeEquipamento tipo,int nivel = 1)
    {
        EquipamentoBase retorno = new EquipamentoBase();
        switch (tipo)
        {
            case TiposDeEquipamento.anelEspecialMaisPotente:
                retorno = new AnelEspecialMaisPotente() {
                    NivelDoEquipamento = nivel,
                    NomeEquipamento = "Anel do especial mais potente",
                    TaxaDeModificacaoDoEquipamento = 0.11f
                };
            break;
            case TiposDeEquipamento.anelMaisMoeda:
                retorno = new AnelMaisMoedas()
                {
                    NivelDoEquipamento = nivel,
                    NomeEquipamento = "Anel mais moedas"
                };
            break;
                
            case TiposDeEquipamento.anelMaisEstamina:
                retorno = new AnelMaisEstamina()
                {
                    NivelDoEquipamento = nivel,
                    NomeEquipamento = "Anel mais estamina"
                };
            break; 
            case TiposDeEquipamento.anelMaisCheckCombos:
                retorno = new AnelMaisCheckCombos()
                {
                    NivelDoEquipamento = nivel,
                    NomeEquipamento = "Anel mais CheckCombo"
                };
            break; 
            case TiposDeEquipamento.anelMaisEsferas:
                retorno = new AnelMaisEsferas()
                {
                    NivelDoEquipamento = nivel,
                    NomeEquipamento = "Anel mais Esferas Especiais"
                };
            break;
            case TiposDeEquipamento.anelMenosCustoDeEsfera:
                retorno = new AnelMenosCustoDeEsfera()
                {
                    NivelDoEquipamento = nivel,
                    NomeEquipamento = "Anel do especial mais rápido"
                };
            break;
            case TiposDeEquipamento.anelMaisAtaque:
                retorno = new AnelMaisAtaque()
                {
                    NivelDoEquipamento = -1,
                    NomeEquipamento = "Anel de força"
                };
            break;
            case TiposDeEquipamento.anelMaisDefesa:
                retorno = new AnelMaisDefesa()
                {
                    NivelDoEquipamento = -1,
                    NomeEquipamento = "Anel de resistencia"
                };
                break;
            case TiposDeEquipamento.anelMagnetico:
                retorno = new AnelMagnetico()
                {
                    NivelDoEquipamento = -1,
                    NomeEquipamento = "Anel Magnetico"
                };
            break;
            case TiposDeEquipamento.anelMaisTempoDeCombo:
                retorno = new AnelMaisTempoDeCombo()
                {
                    NivelDoEquipamento = -1,
                    NomeEquipamento = "Anel do tempo de combo"
                };
            break;
            case TiposDeEquipamento.anelVidaExtra:
                retorno = new AnelVidaExtra()
                {
                    NivelDoEquipamento = -1,
                    NomeEquipamento = "Anel de vida extra"
                };
                break;

        }

        retorno.Tipo = tipo;
        return retorno;
    }

}
