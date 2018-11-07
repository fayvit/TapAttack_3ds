using UnityEngine;
using System.Collections;
[System.Serializable]
public class InimigosEmUnicoJogo : Missoes
{
    public InimigosEmUnicoJogo(int nivel)
    {
        Meta = MetaPeloLevel(75, nivel, 0.03f);
        Tipo = TipoMissao.inimigosEmUnicoJogo;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado = dados.Inimigos;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return dados.Inimigos;
    }
}
