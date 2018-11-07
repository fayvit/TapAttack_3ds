using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelMaisMoedas : EquipamentoBase
{

    public override void EfeitoDoEquipamento()
    {
        ControladorDeJogo.c.SpawnItens.AlterarTaxaDeSpawndoItem(NomeItem.Moeda,TaxaDeModificacaoDoEquipamento);
    }
}
