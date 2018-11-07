using UnityEngine;
using System.Collections;
[System.Serializable]
public class EsferasEmUnicoJogo : Missoes
{

    public EsferasEmUnicoJogo(int nivel)
    {
        Meta = MetaPeloLevel(25, nivel, 0.05f);
        Tipo = TipoMissao.esferasEmUnicoJogo;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado = dados.Esferas;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return dados.Esferas;
    }
}
