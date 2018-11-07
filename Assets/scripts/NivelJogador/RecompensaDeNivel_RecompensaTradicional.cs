using UnityEngine;
using System.Collections;

public class RecompensaDeNivel_RecompensaTradicional : RecompensaDeNivel
{

    public override void AcaoDaRecompensa()
    {
        ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.Recompensas.Add(
            new RecompensaPorPassarDeNivel()
            );
    }
}

[System.Serializable]
public class RecompensaPorPassarDeNivel : Recompensa
{
    public RecompensaPorPassarDeNivel()
    {
            TextoDaRecompensa = "Você recebeu uma recompensa por passar de nível";
            Valores = new ValorDeRecompensa[2]
            { new ValorDeRecompensa() { Quantidade = 5,Tipo = tipoDeRecompensas.estrelasDeCristal},
            new ValorDeRecompensa() { Quantidade = 25,Tipo = tipoDeRecompensas.xp}
            };
            
    }
}
