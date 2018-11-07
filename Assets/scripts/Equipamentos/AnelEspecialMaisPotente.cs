using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelEspecialMaisPotente : EquipamentoBase
{
    
    public override void EfeitoDoEquipamento()
    {
        ControladorDeJogo.c.AtkE.NumeroDeAtaques
            = (int)(ControladorDeJogo.c.AtkE.NumeroDeAtaques * (1 + TaxaDeModificacaoDoEquipamento));
    }
}
