using UnityEngine;
using System.Collections;

public class AfastadorDeInimigoEmDano : MonoBehaviour
{
    public float tempoDeAfastamento;
    public float distanciaDeAfastamento;
    public Vector3 dirAfastamento;

    private float contadorDeTempo = 0;
    private Vector3 posAlvo;
    private Rigidbody R;

    public static void InsereAfastamento(
        Vector3 dirDeAfastamento,
        float distanciaDeAfastamento,
        float tempoDeAfastamento,
        GameObject oAfastado
        )
    {
        AfastadorDeInimigoEmDano af = oAfastado.AddComponent<AfastadorDeInimigoEmDano>();
        af.dirAfastamento = dirDeAfastamento;
        af.distanciaDeAfastamento = distanciaDeAfastamento;
        af.tempoDeAfastamento = tempoDeAfastamento;
    }
    
    // Use this for initialization
    void Start()
    {
        posAlvo = distanciaDeAfastamento * dirAfastamento + transform.position;
        posAlvo = MelhoraInstancia.PosEmparedado(posAlvo, transform.position);
        posAlvo = MelhoraInstancia.ProcuraPosNoMapa(posAlvo);
        R = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        contadorDeTempo += Time.deltaTime;
        dirAfastamento = Vector3.ProjectOnPlane(posAlvo- transform.position, Vector3.up);

        Debug.DrawRay(posAlvo, Vector3.up, Color.red, 10);
        if (Vector3.Distance(posAlvo, transform.position) > 0.1f &&contadorDeTempo<tempoDeAfastamento)
        {
            R.velocity = (dirAfastamento *distanciaDeAfastamento *  contadorDeTempo/ tempoDeAfastamento);
            if (gameObject.name == "Miinho")
                print("velocidade" + R.velocity);
        }
        else
        {            
            transform.position = posAlvo;
            Destroy(this);
        }
    }
}
