using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InimigosPorNivel
{
    private static List<List<encontravel>> listaEncontravel = new List<List<encontravel>>()
    {
        new List<encontravel>() { new encontravel(Inimigos.sapinho,1)},// nivel 1
        new List<encontravel>() { new encontravel(Inimigos.sapinho,0.8f),
            new encontravel(Inimigos.sapinhoDeCerco, 0.2f)
        },// nivel 2
        new List<encontravel>() { new encontravel(Inimigos.sapinho,0.7f),
            new encontravel(Inimigos.sapinhoDeCerco, 0.3f)
        },// nivel 3
        new List<encontravel>() { new encontravel(Inimigos.sapinho,0.6f),
            new encontravel(Inimigos.sapinhoDeCerco, 0.1f),
            new encontravel(Inimigos.sapinhoDeCanto, 0.3f)//nivel 4
        },
        new List<encontravel>() { new encontravel(Inimigos.sapinho,0.5f),
            new encontravel(Inimigos.sapinhoDeCerco, 0.2f),
            new encontravel(Inimigos.sapinhoDeCanto, 0.26f),
            new encontravel(Inimigos.jacare,0.04f)//nivel 5
        },
        new List<encontravel>() { new encontravel(Inimigos.sapinho,0.5f),
            new encontravel(Inimigos.sapinhoDeCerco, 0.2f),
            new encontravel(Inimigos.sapinhoDeCanto, 0.25f),
            new encontravel(Inimigos.jacare,0.04f)//nivel 6
        },
        new List<encontravel>() { new encontravel(Inimigos.sapinho,0.36f),
            new encontravel(Inimigos.sapinhoDeCerco, 0.25f),
            new encontravel(Inimigos.sapinhoDeCanto, 0.35f),
            new encontravel(Inimigos.jacare,0.03f),
            new encontravel(Inimigos.mago,0.01f)//nivel 7
        },
    };
    private enum Inimigos
    {
        sapinho =0,
        sapinhoDeCerco =1,
        sapinhoDeCanto=2,        
        jacare=3,
        mago = 4,
    }

    private struct encontravel
    {
        private Inimigos oInimigo;
        private float taxa;

        public encontravel(Inimigos oInimigo, float taxa)
        {
            this.oInimigo = oInimigo;
            this.taxa = taxa;
        }
        public float Taxa
        {
            get { return taxa; }
            set { taxa = value; }
        }

        public Inimigos OInimigo
        {
            get { return oInimigo; }            
        }
    }

    public static int EncontravelDoNivel(int nivel)
    {
        
        int retorno = 0;
        if (nivel <= 7 && nivel != 1)
            retorno = SorteiaInimigo(listaEncontravel[nivel-1]);
        else if (nivel > 7)
            retorno = SorteiaInimigo(ListaUpada(nivel));

        return retorno;
    }

    static List<encontravel> ListaUpada(int nivel)
    {
        return new List<encontravel>() { new encontravel(Inimigos.sapinho,0.1f*nivel*0.9f),
            new encontravel(Inimigos.sapinhoDeCerco, 0.2f*nivel*0.7f),
            new encontravel(Inimigos.sapinhoDeCanto, 0.3f*nivel),
            new encontravel(Inimigos.jacare,0.3f*nivel*1.2f),
            new encontravel(Inimigos.mago,0.1f*nivel*1.1f) };        
    }

    static int SorteiaInimigo(List<encontravel> encontraveis)
    {
        int retorno = -1;
        float sum = 0;
        int i;

        for ( i = 0; i < encontraveis.Count; i++)
            sum += encontraveis[i].Taxa;

        
        float roleta = Random.Range(0, sum);
        
        sum = 0;
        for (i = 0; i < encontraveis.Count; i++)
        {
            sum += encontraveis[i].Taxa;
            
            if (roleta <= sum && retorno == -1)
            {
                
                retorno = (int)encontraveis[i].OInimigo;
            }
        }

        return retorno;
    }

}
