using UnityEngine;
using System.Collections;

public class AplicadorDeVidaExtra : MonoBehaviour
{
    private float tempoDecorrido = 0;
    private bool jaInstanciei = false;
    private Rigidbody[] rDosInimigos;
    private Transform tHeroi;

    private const float VELOCIDADE_DE_AFASTAMENTO = 10;
    private const float TEMPO_PARA_PARTICULA2 = 1.5F;
    private const float TEMPO_PARA_DESTROIUR = 2.2F;
    // Use this for initialization
    void Start()
    {
        tHeroi = GameObject.FindWithTag("Player").transform;
        GameObject[] Gs =  GameObject.FindGameObjectsWithTag("inimigo");
        DestruaOsEstouSpawnando();
        rDosInimigos = new Rigidbody[Gs.Length];
        for (int i = 0; i < Gs.Length; i++)
        {
            rDosInimigos[i] = Gs[i].GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        tempoDecorrido += Time.deltaTime;
        for (int i = 0; i < rDosInimigos.Length; i++)
        {
            rDosInimigos[i].velocity = 
                (rDosInimigos[i].transform.position - tHeroi.position).normalized * VELOCIDADE_DE_AFASTAMENTO;
        }

        if (tempoDecorrido > TEMPO_PARA_PARTICULA2 && !jaInstanciei)
        {
            GameObject G = ControladorDeJogo.c.RetornaElemento(Elementos.reviviLevantei);
            Destroy(Instantiate(G, tHeroi.position, G.transform.rotation),3);
            tHeroi.GetComponent<EstadoDePersonagem_Gerente>().ApComandos.Mov.DesativaMorto();
            jaInstanciei = true;
        }

        if (tempoDecorrido > TEMPO_PARA_DESTROIUR)
        {
            tHeroi.GetComponent<EstadoDePersonagem_Gerente>().Revivi();
            Destroy(gameObject);
        }
        
    }

    void DestruaOsEstouSpawnando()
    {
        EstouSpawnando[] estou = FindObjectsOfType<EstouSpawnando>();
        foreach (EstouSpawnando e in estou)
            Destroy(e.gameObject);
    }
}
