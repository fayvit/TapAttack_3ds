using UnityEngine;
using System.Collections;

public class AfastamentoDeExplosaoParaHeroi : MonoBehaviour
{
    
    public float tempoAfastando = 0.25f;
    public float distanciaAfastando = 10;
    public Vector3 dirAfastamento = Vector3.zero;

    private float tempoDecorrido = 0;
    private Vector3 posInicial;

    // Use this for initialization
    void Start()
    {
        GetComponent<Animator>().Play("tomouDano");
       
        posInicial = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        tempoDecorrido += Time.deltaTime;
        transform.position += dirAfastamento * distanciaAfastando * Time.deltaTime / tempoAfastando;
        if (tempoDecorrido > tempoAfastando)
        {
            transform.position = MelhoraInstancia.PosEmparedado(posInicial + dirAfastamento * distanciaAfastando, posInicial);
            GetComponent<EstadoDePersonagem_Gerente>().ApComandos.Mov.PararMovimento();
            Destroy(this);
        }
    }
}
