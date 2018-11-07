using UnityEngine;
using System.Collections;

public class AvancaMagia : MonoBehaviour
{
    private Vector3 dir = Vector3.forward;
    private float velocidade = 25;
    private int dano = 1;

    private const float TEMPO_DE_DESTRUICAO_DESSA_MAGIA = 10;
    private const float TEMPO_DE_DESTRUICAO_DA_PARTICULA = 1;
    public Vector3 Dir
    {
        get { return dir; }
        set { dir = value; }
    }

    public float Velocidade
    {
        get { return velocidade; }
        set { velocidade = value; }
    }

    public int Dano
    {
        get { return dano; }
        set { dano = value; }
    }

    // Use this for initialization
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(Dir);
        Destroy(gameObject, TEMPO_DE_DESTRUICAO_DESSA_MAGIA);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Dir * Time.deltaTime * Velocidade;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<EstadoDePersonagem_Gerente>().AplicarDano(dano);
            GameObject G = ControladorDeJogo.c.RetornaElemento(Elementos.aMagiaAcertou);
            Destroy(Instantiate(G,transform.position,G.transform.rotation),TEMPO_DE_DESTRUICAO_DA_PARTICULA);
            Destroy(gameObject);
        }
    }
}
