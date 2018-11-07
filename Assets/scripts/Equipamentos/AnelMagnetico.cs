using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelMagnetico : EquipamentoBase
{

    public override void EfeitoDoEquipamento()
    {
        ControladorDeJogo.c.MoedasMagneticas = true;
    }
}
