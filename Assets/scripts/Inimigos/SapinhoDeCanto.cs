using UnityEngine;
using System.Collections;

public class SapinhoDeCanto : Sapinho
{
    private Vector3 meuCanto = Vector3.zero;

    new void Start()
    {
        GameObject G = GameObject.FindWithTag("Player");
        if (G)
        {
            base.Start();
            meuCanto = ControladorDeJogo.c.CantosDoMapa[Random.Range(0, 4)].position;
        }
        
    }

    protected override float DistanciaDePerseguicao
    {
        get { return 10000; }
    }
    protected override Vector3 PontoAlvo()
    {       
    
        if (Vector3.Distance(tHeroi.position, transform.position) < distanciaDePerseguicao)
            return base.PontoAlvo();
        else
        {
            return (meuCanto-transform.position).normalized;
        }
    }

}
