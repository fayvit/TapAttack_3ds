using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelMenosCustoDeEsfera : EquipamentoBase
{
    public override void EfeitoDoEquipamento()
    {
        DadosDoPersonagem dados =
        GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().Dados;
        dados.CristaisParaAtivar = (int)(1 / (1 + TaxaDeModificacaoDoEquipamento) * dados.CristaisParaAtivar);
    }
}