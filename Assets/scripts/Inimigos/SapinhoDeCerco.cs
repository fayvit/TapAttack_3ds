using UnityEngine;
using System.Collections;

public class SapinhoDeCerco : Sapinho
{

    private int qual = 0;
    protected override Vector3 PontoAlvo()
    {
        if (qual == 0)
        {
            qual = Random.Range(-1, 2);
            
        }

        Vector3 retorno = tHeroi.position + tHeroi.forward + tHeroi.right*qual;
        //retorno = MelhoraInstancia.PosEmparedado(retorno, transform.position);
        //retorno = MelhoraInstancia.ProcuraPosNoMapa(retorno);

        retorno -= transform.position;
        retorno = new Vector3(retorno.x, 0, retorno.z);
        retorno = retorno.normalized;
        Debug.DrawRay(transform.position, retorno, Color.red, 1);
        return retorno;
    }
}
