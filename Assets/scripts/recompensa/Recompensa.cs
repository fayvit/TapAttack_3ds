using UnityEngine;
using System.Collections;

[System.Serializable]
public class Recompensa
{
    [SerializeField] private string textoDaRecompensa = "";
    [SerializeField] private ValorDeRecompensa[] valores;

    public ValorDeRecompensa[] Valores
    {
        get { return valores; }
        protected set { valores = value; }
    }

    public string TextoDaRecompensa
    {
        get { return textoDaRecompensa; }
        protected set { textoDaRecompensa = value; }
    }
}

[System.Serializable]
public struct ValorDeRecompensa
{
    [SerializeField]private tipoDeRecompensas tipo;
    [SerializeField]private int quantidade;

    public tipoDeRecompensas Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }

    public int Quantidade
    {
        get { return quantidade; }
        set { quantidade = value; }
    }
}


public enum tipoDeRecompensas
{
    moedas,
    estrelasDeCristal,
   // x2,
    //impulsoPoderoso,
    xp
}
