using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelMaisTempoDeCombo : EquipamentoBase
{

    public override void EfeitoDoEquipamento()
    {
        ControladorDeJogo.c.G_Combos.dobraTempoDeCombo = true;
    }
}



