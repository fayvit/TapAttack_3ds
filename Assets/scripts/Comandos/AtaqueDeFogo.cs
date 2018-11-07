using UnityEngine;
using System.Collections;

public class AtaqueDeFogo : AplicadorDeDano
{
    public Material materialPiscante;

    new void Start()
    {
        Elementos e = Elementos.somDoAtaqueDeFogo;
        Destroy(Instantiate(ControladorDeJogo.c.RetornaElemento(e)), 5);
    }
    protected override void AplicaDanoEmInimigo(Collision col,
                    TipoDeDano tipo = TipoDeDano.fisico,
                    Elementos elemento = Elementos.parDano)
    {
        
        base.AplicaDanoEmInimigo(col, TipoDeDano.fogo, Elementos.danoDeFogo);
        if (!col.gameObject.GetComponent<MaterialPiscante>())
        {
            MaterialPiscante M = col.gameObject.AddComponent<MaterialPiscante>();
            M.materialPiscante = materialPiscante;
        }
    }
}
