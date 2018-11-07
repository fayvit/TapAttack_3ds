using UnityEngine;
using System.Collections;
[System.Serializable]
public class ColeteEsferas : Missoes
{

    public ColeteEsferas(int nivel)
    {
        Meta = MetaPeloLevel(75, nivel, 0.1f);
        Tipo = TipoMissao.coleteEsferas;
        
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado += dados.Esferas;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return alcancado + dados.Esferas;
    }
}
