using UnityEngine;
using System.Collections;

public class PrimeiroMultiplicadorGlobal : MaisMultiplicadorGlobal
{
    public override void MostrarAlgo(RecebiAlgo recebido, RecebiAlgo.acaoDesteBotao volta)
    {
        recebido.ConstroiObjeto(volta, RecebiAlgo.Estilo.primeiroMultiplicadorDePontos);
        tenhoAlgoParaMostrar = false;
    }
}
