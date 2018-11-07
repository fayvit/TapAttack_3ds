using UnityEngine;
using System.Collections;

public class EquipamentoPorNivel : RecompensaDeNivel
{
    private EquipamentoBase equip;
    public override void AcaoDaRecompensa()
    {
        equip = PegueUmEquipamento.SorteiaEquipamento();
        ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado
        .AdicionaEquipamentoNaLita(equip);
    }

    public override void MostrarAlgo(RecebiAlgo recebido, RecebiAlgo.acaoDesteBotao volta)
    {
        recebido.ConstroiObjeto(volta, equip);
        tenhoAlgoParaMostrar = false;
    }
}
