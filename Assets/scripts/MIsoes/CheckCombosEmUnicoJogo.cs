using UnityEngine;
using System.Collections;
[System.Serializable]
public class CheckCombosEmUnicoJogo : Missoes
{
    public CheckCombosEmUnicoJogo(int nivel)
    {
        Meta = MetaPeloLevel(20, nivel, 0.1f);
        Tipo = TipoMissao.checkCombosEmUnicoJogo;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado = dados.Cubos;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return dados.Cubos;
    }
}
