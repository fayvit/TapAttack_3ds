using UnityEngine;
using System.Collections;
[System.Serializable]
public class ColeteMoedas : Missoes
{
    public ColeteMoedas(int nivel = 1)
    {
        Tipo = TipoMissao.coleteMoedas;
        Meta = MetaPeloLevel(115,nivel,0.1f);
    }
    public override void SomaAlcancado(ContainerDosDadosEmJogo dados)
    {
        alcancado += dados.Moedas;
    }

    public override int MostraSoma(ContainerDosDadosEmJogo dados)
    {
        return alcancado + dados.Moedas;
    }
}
