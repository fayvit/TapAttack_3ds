using UnityEngine;
using System.Collections;
[System.Serializable]
public class MoedasEmUnicoJogo : Missoes
{
    public MoedasEmUnicoJogo(int nivel)
    {
        Tipo = TipoMissao.moedasEmUnicoJogo;
        Meta = MetaPeloLevel(35,nivel,0.1f);
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado = dados.Moedas;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return dados.Moedas;
    }
}
