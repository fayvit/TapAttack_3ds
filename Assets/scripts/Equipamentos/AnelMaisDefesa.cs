using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnelMaisDefesa : EquipamentoBase
{

    public override void EfeitoDoEquipamento()
    {
        NivelDoEquipamento = -1;
         GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().Dados.DobreVida = true;
        
    }
}



