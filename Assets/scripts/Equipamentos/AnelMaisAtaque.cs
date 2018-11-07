using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelMaisAtaque : EquipamentoBase
{

    public override void EfeitoDoEquipamento()
    {
        GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().Dados.DobreForca = true;        
    }
}



