using UnityEngine;
using System.Collections;
[System.Serializable]
public class AlcanceCombo : Missoes
{
    public AlcanceCombo(int nivel)
    {
        Meta = MetaPeloLevel(50, nivel, 0.1f);
        Tipo = TipoMissao.alcanceCombo;
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado = dados.ComboMaximoAlcancado;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return dados.ComboMaximoAlcancado;
    }
}
