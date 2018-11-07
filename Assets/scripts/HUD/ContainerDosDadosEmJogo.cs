using UnityEngine;
[System.Serializable]
public class ContainerDosDadosEmJogo
{
    
    [SerializeField]private int pontuacao = 0;
    [SerializeField]private int comboMaximoAlcancado = 0;
    [SerializeField]private int esferas = 0;
    [SerializeField]private int cubos = 0;
    [SerializeField]private int moedas = 0;
    [SerializeField]private int estaminas = 0;
    [SerializeField]private int nivel = 0;
    [SerializeField]private int inimigos = 0;

    public int Pontuacao
    {
        get { return pontuacao; }
        set { pontuacao = value; }
    }

    public int ComboMaximoAlcancado
    {
        get { return comboMaximoAlcancado; }
        set { comboMaximoAlcancado = value; }
    }

    public int Esferas
    {
        get { return esferas; }
        set { esferas = value; }
    }

    public int Cubos
    {
        get { return cubos; }
        set { cubos = value; }
    }

    public int Moedas
    {
        get { return moedas; }
        set { moedas = value; }
    }

    public int Estaminas
    {
        get { return estaminas; }
        set { estaminas = value; }
    }

    public int Nivel
    {
        get { return nivel; }
        set { nivel = value; }
    }

    public int Inimigos
    {
        get { return inimigos; }
        set { inimigos = value; }
    }
}

