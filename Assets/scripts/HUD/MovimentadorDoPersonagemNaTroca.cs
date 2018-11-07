using UnityEngine;
using System.Collections;

[System.Serializable]
public class MovimentadorDoPersonagemNaTroca
{
    [SerializeField]private int quem = 0;
    [SerializeField]private float tempoParaTroca = 1;
    [SerializeField]private Transform[] personagens;
    [SerializeField]private Transform posAlvo;
    [SerializeField]private Transform tEscondido;//Atras da Camera

    private float tempoDecorrido = 0;
    private bool trocando = false;
    private Vector3 posEscondido = Vector3.zero;

    // Use this for initialization
    public void Start()
    {
        posEscondido = tEscondido.position - tEscondido.forward * 2;
    }

    // Update is called once per frame
    public void Update()
    {
        if (trocando)
        {
            tempoDecorrido += Time.deltaTime;
            if (Vector3.Distance(personagens[quem].position,posAlvo.position)>0.1f)
            {
                personagens[quem].position = Vector3.Lerp(personagens[quem].position,posAlvo.position,tempoDecorrido/tempoParaTroca);
            }

            for (int i = 0; i < personagens.Length; i++)
            {
                if (quem != i && Vector3.Distance(personagens[i].position, posEscondido) > 0.1f)
                {
                    personagens[i].position = Vector3.Lerp(personagens[i].position, posEscondido, tempoDecorrido / tempoParaTroca);
                }
            }

            if (tempoDecorrido > tempoParaTroca)
                trocando = false;
        }
        
    }

    public void DisparaTroca(int quem)
    {
        this.quem = quem;
        tempoDecorrido = 0;
        trocando = true;
    }
}
