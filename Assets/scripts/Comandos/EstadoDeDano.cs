using UnityEngine;
using System.Collections;

public class EstadoDeDano
{
    private float tempoDecorrido = 0;
    private float tempoBaseDeDano = 0.5f;
    private float tempoNesseDano = 0.5f;
    private bool estouEmDano = false;

    public void DisparaDano(float tempoDeDano = 0)
    {
        if (tempoDeDano != 0)
            tempoNesseDano = tempoDeDano;
        else
            tempoNesseDano = tempoBaseDeDano;

        tempoDecorrido = 0;

        estouEmDano = true;
    }
    // Update is called once per frame
    public bool Update()
    {
        if (estouEmDano)
        {
            tempoDecorrido += Time.deltaTime;
            if (tempoDecorrido > tempoNesseDano)
                estouEmDano = false;
        }

        return estouEmDano;
    }
}
