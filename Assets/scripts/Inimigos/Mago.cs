using UnityEngine;
using System.Collections;

public class Mago : InimigoBase
{
    [SerializeField]private UnityEngine.AI.NavMeshAgent nav;
    [SerializeField]private Animator animator;
    [SerializeField]private float tempoBaseDeAtaque = 7;
    [SerializeField]private AudioClip[] risadas;
    [SerializeField]private AudioSource audioX;

    
    private float tempoDecorrido = 0;
    private bool iniciou = false;
    private Transform tHeroi;
    private Vector3 pontoDeGuarda;
    private faseDoMago fase = faseDoMago.rirDoHeroi;

    private const float DISTANCIA_PARA_RIR = 30;
    private const float TEMPO_RINDO = 1.5F;
    private const float VELOCIDADE_PARA_VOLTAR_AO_PAI = 2.5F;
    private enum faseDoMago
    {
        rirDoHeroi,
        rindoDoHeroi,
        ocupandoPosicao,
        emPOsicaoDeAtaque
    }
    // Use this for initialization
    new void Start()
    {
        GameObject G = GameObject.FindWithTag("Player");
        if (G)
        {
            base.Start();
            tHeroi = G.transform;
            tempoBaseDeAtaque *= (float)100 / (100 + Ataque);
            
            pontoDeGuarda = PontoDeGuardaDoMago();
            iniciou = true;
        }
    }

    Vector3 PontoDeGuardaDoMago()
    {
        return ControladorDeJogo.c.CantosDoMapa[Random.Range(0,4)].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciou)
            ComportamentoDoMago();
        else
            Start();
    }

    void ComportamentoDoMago()
    {
        switch (fase)
        {
            case faseDoMago.rirDoHeroi:
                ProcureRisada();
            break;
            case faseDoMago.rindoDoHeroi:
                tempoDecorrido += Time.deltaTime;
                if (tempoDecorrido > TEMPO_RINDO)                
                    OcupandoPosicao();
                
            break;
            case faseDoMago.ocupandoPosicao:
                if (Vector3.Distance(transform.position, pontoDeGuarda) < 3f)
                    fase = faseDoMago.emPOsicaoDeAtaque;
            break;
            case faseDoMago.emPOsicaoDeAtaque:
                RotinaDeAtaque();
            break;
        }

        animator.SetFloat("velocidade", nav.velocity.magnitude);
        transform.LookAt(tHeroi);

        transform.position =
            Vector3.Lerp(transform.position, transform.parent.position, Time.deltaTime * VELOCIDADE_PARA_VOLTAR_AO_PAI);
    }

    void RotinaDeAtaque()
    {
        tempoDecorrido += Time.deltaTime;
        if (tempoDecorrido > tempoBaseDeAtaque)
        {
            DisparaAtaque();
            tempoDecorrido = 0;
        }
    }

    void DisparaAtaque()
    {
        Invoke("SaiMagia", 0.5f);
        animator.Play("atacar");
    }

    void SaiMagia()
    {

        GameObject G = (GameObject)Instantiate(
                ControladorDeJogo.c.RetornaElemento(Elementos.magiaDoMaguinho),
                transform.position + 3 * transform.forward + Vector3.up, Quaternion.LookRotation(transform.forward)
                );

        AvancaMagia ava = G.GetComponent<AvancaMagia>();
        ava.Dir = transform.forward;
        ava.Velocidade = 25 * (1 + Velocidade / 100);
        ava.Dano = Ataque;
    }

    void OcupandoPosicao()
    {
        fase = faseDoMago.ocupandoPosicao;
        nav.destination = pontoDeGuarda;
    }
    void ProcureRisada()
    {
        if (Vector3.Distance(transform.position, tHeroi.position) > DISTANCIA_PARA_RIR)
        {
            nav.destination = tHeroi.position;
        }
        else {
            nav.destination = transform.position;
            animator.SetTrigger("darRisada");
            audioX.clip = risadas[Mathf.Clamp(Random.Range(-2, 2), 0, 1)];
            audioX.Play();
            fase = faseDoMago.rindoDoHeroi;
            tempoDecorrido = 0;
        }
    }

    void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
