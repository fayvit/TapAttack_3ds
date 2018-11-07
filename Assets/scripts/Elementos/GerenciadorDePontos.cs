using UnityEngine;
using System.Collections;
[System.Serializable]
public class GerenciadorDePontos
{
    [SerializeField]private int pontosTotais = 0;

    public int PontosTotais
    {
        get { return pontosTotais; }
    }

    public void AdicionaPontos(int tanto)
    {
        pontosTotais += tanto;
    }
}
