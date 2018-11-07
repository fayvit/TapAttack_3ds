using UnityEngine;
using System.Collections;

[System.Serializable]
public class RecompensaPorMissao:Recompensa
{
    public RecompensaPorMissao(TipoMissao tipo,int nivel,int meta)
    {
        TextoDaRecompensa = string.Format(
            BancoDeTextos.TextosDoIdioma(ChavesDeTexto.missaoCumprida),
            string.Format(
                BancoDeTextos.TextosDoIdioma("indicativoDaMissao"+tipo.ToString()),meta)
            );

        Valores = CalculeOsValores(nivel);
    }

    ValorDeRecompensa[] CalculeOsValores(int nivel)
    {
        Perfil P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;

        ValorDeRecompensa[] retorno = new ValorDeRecompensa[2]
            { new ValorDeRecompensa() { Quantidade = 15 +3*(nivel-1),Tipo = tipoDeRecompensas.moedas},
            new ValorDeRecompensa() { Quantidade = 25+5*(nivel-1),Tipo = tipoDeRecompensas.xp}
            };

        if (P.numeroDeJogosHoje() > 1 && P.numeroDeJogosHoje() % 10 == 0)
        {
            retorno[0] = new ValorDeRecompensa() { Quantidade = nivel * (1 + Random.Range(0, 2)), Tipo = tipoDeRecompensas.estrelasDeCristal };
        }else
        if (P.JogosSeguidos > 10)
        {
            retorno[0].Quantidade = 20 + 5 * (nivel - 1);
            retorno[1].Quantidade = 30 + 7 * (nivel - 1);
        }

        return retorno;
    }
}