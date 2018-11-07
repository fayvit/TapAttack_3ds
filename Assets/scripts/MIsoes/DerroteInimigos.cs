using UnityEngine;
using System.Collections;
[System.Serializable]
public class DerroteInimigos : Missoes
{
    public DerroteInimigos(int nivel)
    {
        Meta = MetaPeloLevel(310, nivel, 0.1f);
        Tipo = TipoMissao.derroteInimigos;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado += dados.Inimigos;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return alcancado + dados.Inimigos;
    }
}
