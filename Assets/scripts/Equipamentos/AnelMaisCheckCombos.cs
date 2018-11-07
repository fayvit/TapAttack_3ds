using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelMaisCheckCombos : EquipamentoBase
{
    public override void EfeitoDoEquipamento()
    {
        ControladorDeJogo.c.SpawnItens.AlterarTaxaDeSpawndoItem(NomeItem.checkCombo, TaxaDeModificacaoDoEquipamento);
    }
}
