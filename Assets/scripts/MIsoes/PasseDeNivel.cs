using UnityEngine;
using System.Collections;
[System.Serializable]
public class PasseDeNivel : Missoes
{
    public PasseDeNivel(int nivel)
    {
        Meta = MetaPeloLevel(14, nivel, 0.05f);
        Tipo = TipoMissao.passeDeNivel;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado += (dados.Nivel-1);
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return alcancado + (dados.Nivel-1);
    }
}
