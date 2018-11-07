using UnityEngine;
using System.Collections;
[System.Serializable]
public class AlcanceNivel : Missoes
{
    public AlcanceNivel(int nivel)
    {
        Meta = MetaPeloLevel(7, nivel, 0.2f);
        Tipo = TipoMissao.alcanceNivel;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado = dados.Nivel;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return dados.Nivel;
    }
}
