using UnityEngine;
using System.Collections;

public class MaisMultiplicadorGlobal : RecompensaDeNivel
{
    public float valor = 0.5f;
    public override void AcaoDaRecompensa()
    {
        ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.SomaMultiplicadorGlobal(valor);
    }
}
