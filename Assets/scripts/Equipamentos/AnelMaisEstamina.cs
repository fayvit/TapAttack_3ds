using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelMaisEstamina : EquipamentoBase
{

    public override void EfeitoDoEquipamento()
    {
        ControladorDeJogo.c.SpawnItens.AlterarTaxaDeSpawndoItem(NomeItem.maisEstamina, TaxaDeModificacaoDoEquipamento);
    }
}
