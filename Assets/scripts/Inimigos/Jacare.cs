using UnityEngine;
using System.Collections;

public class Jacare : InimigoBase
{
    [SerializeField]private Rigidbody rigid;
    [SerializeField]private Animator animator;
    [SerializeField]private AudioClip[] sons;
    [SerializeField]private AudioSource audioX;

    private bool iniciou = false;
    private float tempoDecorrido = 0;
    private Transform tHeroi;
    private Vector3 pontoDeSpawn = Vector3.zero;
    private Vector3 pontoParaDeslocamento = Vector3.zero;
    private FaseDoJacare fase = FaseDoJacare.andandoDeBoa;

    private const float DISTANCIA_PARA_VER = 25;
    private const float DISTANCIA_DO_PONTO_FOCO = 15;
    private const float TEMPO_ANIMA_VENDO = 1.5F;
    private const float VELOCIDADE_DE_GIRO = 250;
    private const float VELOCIDADE_ANDANDO_BASE = 2;
    private const float VELOCIDADE_CORRENDO_BASE = 3;

    private enum FaseDoJacare
    {
        andandoDeBoa,
        Viu,
        perseguindo
    }
    // Use this for initialization
    new void Start()
    {
        GameObject G = GameObject.FindWithTag("Player");
        if (G)
        {
            base.Start();
            tHeroi = G.transform;
            pontoDeSpawn = transform.position;
            pontoParaDeslocamento = NovoPontoParaDeslocamento();
            iniciou = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciou)
            ComportamentoDoJacare();
        else
            Start();
    }

    void ComportamentoDoJacare()
    {
        switch (fase)
        {
            case FaseDoJacare.andandoDeBoa:
                if (Vector3.Distance(transform.position, pontoParaDeslocamento) > 0.5f)
                {
                    transform.rotation = Quaternion.RotateTowards(
                        transform.rotation,
                        Quaternion.LookRotation(pontoParaDeslocamento - transform.position),
                        Time.deltaTime * VELOCIDADE_DE_GIRO
                        );

                    rigid.velocity = (pontoParaDeslocamento - transform.position).normalized * VELOCIDADE_ANDANDO_BASE * Velocidade;
                }
                else
                {
                    rigid.velocity = Vector3.zero;
                    pontoParaDeslocamento = NovoPontoParaDeslocamento();
                }

                if (Vector3.Distance(transform.position, tHeroi.position) < DISTANCIA_PARA_VER)
                {
                    fase = FaseDoJacare.Viu;
                    audioX.clip = sons[Random.Range(0, sons.Length)];
                    audioX.Play();
                    animator.SetTrigger("Achou");
                    transform.rotation = Quaternion.LookRotation(tHeroi.position - transform.position);
                }
            break;
            case FaseDoJacare.Viu:
                rigid.velocity = Vector3.zero;
                tempoDecorrido += Time.deltaTime;
                if (tempoDecorrido > TEMPO_ANIMA_VENDO)
                    fase = FaseDoJacare.perseguindo;
            break;
            case FaseDoJacare.perseguindo:
                rigid.velocity = (tHeroi.position - transform.position).normalized * VELOCIDADE_CORRENDO_BASE * Velocidade;
                transform.rotation = Quaternion.RotateTowards(
                        transform.rotation,
                        Quaternion.LookRotation(tHeroi.position - transform.position),
                        Time.deltaTime * VELOCIDADE_DE_GIRO
                        );
                break;
        }
    }

    Vector3 NovoPontoParaDeslocamento()
    {
        Vector3 retorno = pontoDeSpawn;
        retorno += Vector3.ProjectOnPlane(Random.onUnitSphere, Vector3.up).normalized*DISTANCIA_DO_PONTO_FOCO;
        return retorno;
    }
}
