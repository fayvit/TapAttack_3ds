using UnityEngine;
using System.Collections;

public class MelhoraInstancia
{
    public static Vector3 PosParaDeslocamento(Vector3 pontoAlvo, Vector3 posAtual)
    {
        pontoAlvo = ProcuraPosNoMapa(pontoAlvo);
        pontoAlvo = PosEmparedado(pontoAlvo, posAtual);
        return pontoAlvo;
    }

    public static Vector3 PosEmparedado(Vector3 pontoAlvo, Vector3 posAtual)
    {
        Vector3 retorno = pontoAlvo;
        pontoAlvo += Vector3.up;
        posAtual += Vector3.up;
        RaycastHit hit;
        if (Physics.Linecast(posAtual, pontoAlvo, out hit))
        {
            if (Vector3.Angle(hit.normal, Vector3.ProjectOnPlane(hit.normal, Vector3.up)) < 5)
            {
                retorno = ProcuraPosNoMapa(hit.point + hit.normal);
             /*   Debug.LogWarning("[melhoraPos] angulo Menor que 10 " + hit.collider.name);*/
            }

            //Debug.Log(hit.collider.gameObject+" o angulo e "+Vector3.Angle(hit.normal,oQProcura-oParado));
        }

        return retorno;
    }

    public static Vector3 ProcuraPosNoMapa(Vector3 pontoAlvo)
    {
        
        return EstaNoMapa(pontoAlvo).pos;
    }

    public static PosNoMapa EstaNoMapa(Vector3 pontoAlvo)
    {
        Vector3 retorno = pontoAlvo;
        bool noMapa = false;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(pontoAlvo, Vector3.up, out hit))
        //if (hit.transform.name == terra)
        {
            retorno = hit.point;
            noMapa = true;

        }

        if (Physics.Raycast(pontoAlvo + 0.1f * Vector3.up, Vector3.down, out hit))
        // if (hit.transform.name == terra)
        {
            retorno = hit.point;
            noMapa = true;
        }

        return new PosNoMapa() { estaNoMapa = noMapa, pos = retorno };

    }

    public static PosNoMapa TelaParaMundo3D(Vector3 posInicial)
    {
        Ray raio = Camera.main.ScreenPointToRay(posInicial);
        RaycastHit hit = new RaycastHit();
        bool foi = false;
        if (Physics.Raycast(raio, out hit))
        {
            foi = true;
            posInicial = hit.point;
        }
        return new PosNoMapa() { estaNoMapa = foi,pos = posInicial};
        
    }
}

public struct PosNoMapa
{
    public bool estaNoMapa;
    public Vector3 pos;
}
