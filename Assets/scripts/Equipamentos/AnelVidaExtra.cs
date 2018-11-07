using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelVidaExtra : EquipamentoBase
{
    public override void EfeitoDoEquipamento()
    {
        ControladorDeJogo.c.VidaExtra = true;
    }
}
