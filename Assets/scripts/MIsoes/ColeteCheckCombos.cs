using UnityEngine;
using System.Collections;
[System.Serializable]
public class ColeteCheckCombos : Missoes
{
    public ColeteCheckCombos(int nivel)
    {
        Meta = MetaPeloLevel(75, nivel, 0.1f);
        Tipo = TipoMissao.coleteCheckCombos;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado += dados.Cubos;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return alcancado + dados.Cubos;
    }
}