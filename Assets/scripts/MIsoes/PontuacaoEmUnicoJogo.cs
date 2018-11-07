using UnityEngine;
using System.Collections;
[System.Serializable]
public class PontuacaoEmUnicoJogo : Missoes
{
    public PontuacaoEmUnicoJogo(int nivel)
    {
        Meta = MetaPeloLevel(50000, nivel, 0.1f);
        Tipo = TipoMissao.pontuacaoEmUnicoJogo;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado = dados.Pontuacao;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return dados.Pontuacao;
    }
}
