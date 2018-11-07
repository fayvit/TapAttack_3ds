using UnityEngine;
using System.Collections;
[System.Serializable]
public class SomePontuacao : Missoes
{
    public SomePontuacao(int nivel)
    {
        Meta = MetaPeloLevel(295000, nivel, 0.1f);
        Tipo = TipoMissao.somePontuacao;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado += dados.Pontuacao;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return alcancado + dados.Pontuacao;
    }
}
