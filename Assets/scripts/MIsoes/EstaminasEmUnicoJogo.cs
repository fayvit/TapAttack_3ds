using UnityEngine;
using System.Collections;
[System.Serializable]
public class EstaminasEmUnicoJogo : Missoes
{

    public EstaminasEmUnicoJogo(int nivel)
    {
        Tipo = TipoMissao.estaminasEmUnicoJogo;
        Meta = MetaPeloLevel(10,nivel,0.1f);
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado = dados.Estaminas;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return dados.Estaminas;
    }
}
