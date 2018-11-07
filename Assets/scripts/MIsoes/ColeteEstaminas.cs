using UnityEngine;
using System.Collections;
[System.Serializable]
public class ColeteEstaminas:Missoes
{ 

    public ColeteEstaminas(int nivel)
    {
        Tipo = TipoMissao.coleteEstaminas;
        Meta = MetaPeloLevel(35, nivel, 0.05f);
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado += dados.Estaminas;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return alcancado + dados.Estaminas;
    }
}
