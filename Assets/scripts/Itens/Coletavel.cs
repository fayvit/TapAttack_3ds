using UnityEngine;
using System.Collections;

public class Coletavel : MonoBehaviour
{
    [SerializeField]private bool rotacionar = false;
    [SerializeField]private float tempoParaDesaparecer = 60;
    [SerializeField]private float tempoParaPiscar = 4;
    [SerializeField]private PiscarMaterial piscar;

    [SerializeField]protected int valor = 1;    

    private float tempoDecorrido = 0;
    private Material materialDePiscar;

    private float TempoParaPiscar
    {
        get { return Mathf.Min(tempoParaPiscar, 0.95f * tempoParaDesaparecer); }
    }

    void Start()
    {
        materialDePiscar = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    protected void Update()
    {
        tempoDecorrido += Time.deltaTime;
        if (tempoDecorrido > tempoParaDesaparecer)
            Destroy(gameObject);

        if ((tempoParaDesaparecer - tempoDecorrido) < TempoParaPiscar)
            piscar.PiscarFloat(materialDePiscar,"_Metallic");

        if(rotacionar)
            transform.Rotate(Vector3.forward, 30 * Time.deltaTime);
    }

    protected virtual GameObject ParticulaDeColetavelEAcaoColetavel(DadosDoPersonagem dados)
    {
        return ControladorDeJogo.c.RetornaElemento(Elementos.parMoeda);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject G = ParticulaDeColetavelEAcaoColetavel(col.GetComponent<EstadoDePersonagem_Gerente>().Dados);

            Destroy(Instantiate(G, transform.position, G.transform.rotation), 2.75f);
            Destroy(gameObject);
        }
    }
}
