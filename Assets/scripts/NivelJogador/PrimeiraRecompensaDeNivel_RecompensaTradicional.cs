using UnityEngine;
using System.Collections;

public class PrimeiraRecompensaDeNivel_RecompensaTradicional : RecompensaDeNivel_RecompensaTradicional
{

    public override void MostrarAlgo(RecebiAlgo recebido, RecebiAlgo.acaoDesteBotao volta)
    {
        tenhoAlgoParaMostrar = false;
        recebido.ConstroiObjeto(volta, RecebiAlgo.Estilo.primeiraRecompensa);
    }
}
