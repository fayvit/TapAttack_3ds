using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PiscarBarra
{
    private RawImage faltouEstamina;
    private bool piscar = false;
    private bool opacidadeCrescente = true;
    private float opacidadeAtual = 0;
    private float tempoPiscando = 1;
    private float tempoDecorrido = 0;

    private float velocidadeDePisca = 10;

    public PiscarBarra(RawImage raw,float velocidade = 10)
    {
        faltouEstamina = raw;
        velocidadeDePisca = velocidade;
    }
    
    public void AcionarPiscaEstamina()
    {
        piscar = true;
        tempoDecorrido = 0;
    }

    // Update is called once per frame
    public void PiscarComTempo()
    {
        Color C = faltouEstamina.color;
        if (piscar)
        {
            tempoDecorrido += Time.deltaTime;
            
            
            if (tempoDecorrido < tempoPiscando)
            {

                Piscador();
                
            }
            else
            {
                opacidadeAtual = 0;
                piscar = false;
            }
            
           
        }
        faltouEstamina.color = new Color(C.r, C.g, C.b, opacidadeAtual);
    }

    public void PiscarSemTempo()
    {
        Color C = faltouEstamina.color;
        Piscador();
        faltouEstamina.color = new Color(C.r, C.g, C.b, opacidadeAtual);        
    }

    public void Piscador()
    {
        if (opacidadeCrescente)
        {
            opacidadeAtual = Mathf.Lerp(opacidadeAtual, 1, velocidadeDePisca * Time.deltaTime);
            if (opacidadeAtual > 0.99f)
            {
                opacidadeAtual = 1;
                opacidadeCrescente = false;
            }

        }
        else
        {
            opacidadeAtual = Mathf.Lerp(opacidadeAtual, 0, velocidadeDePisca * Time.deltaTime);
            if (opacidadeAtual < 0.01f)
            {
                opacidadeAtual = 0;
                opacidadeCrescente = true;
            }
        }
    }
}
