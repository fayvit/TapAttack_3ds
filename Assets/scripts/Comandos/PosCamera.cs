using UnityEngine;
using System.Collections;

public class PosCamera : MonoBehaviour {

    [SerializeField]private Transform alvo;
    [SerializeField]private float altura = 20;
    [SerializeField]private float distanciaHorizontal=20;
    [SerializeField]private float velocidadeDeCamera = 10;

    private Vector3 dirAlvo;
    private float velDeLerp = 1;
	// Use this for initialization
	void Start () {
        if (!alvo)
        {
            GameObject doAlvo = GameObject.FindGameObjectWithTag("Player");
            if (doAlvo)
                alvo = doAlvo.transform;
        }

        dirAlvo = alvo.position-distanciaHorizontal*Vector3.forward+altura*Vector3.up;
        transform.position = dirAlvo;
            transform.LookAt(alvo);
	}
	
	// Update is called once per frame
	void Update () {
        dirAlvo = alvo.position-distanciaHorizontal*Vector3.forward+altura*Vector3.up;

        velDeLerp = velocidadeDeCamera*Mathf.Max(1,
            Vector3.Distance(dirAlvo,transform.position)/Mathf.Sqrt(Mathf.Pow(altura,2) + Mathf.Pow(distanciaHorizontal,2)
            ));

        transform.position = Vector3.Lerp(transform.position,
            dirAlvo
            , velDeLerp * Time.deltaTime);

        TestePosRaio();
	}

    void TestePosRaio()
    {

    }
}
