using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelMaisEsferas : EquipamentoBase
{

    public override void EfeitoDoEquipamento()
    {
        ControladorDeJogo.c.SpawnItens.AlterarTaxaDeSpawndoItem(NomeItem.CristalDeEspecial, TaxaDeModificacaoDoEquipamento);
    }
}