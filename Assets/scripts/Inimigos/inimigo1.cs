using UnityEngine;
using System.Collections;

public class inimigo1 : InimigoBase {

    public float impulso = 350;
    public float distanciaDePerseguicao = 100;

    protected Transform tHeroi;
    protected Animator animator;
    private AnimatorStateInfo aInfo;
    private Rigidbody rigid;
    private bool estavaAndando;
    private bool iniciou = false;

	// Use this for initialization
	 new void Start () {
        GameObject G = GameObject.FindWithTag("Player");
        if (G)
        {
            base.Start();
            tHeroi = G.transform;
            animator = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();
            iniciou = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (iniciou)
        {
            seMovaInimigo();
            SomDoInimigo();
        }
        else
            Start();
        
	}
    protected virtual void SomDoInimigo()
    {

    }

    protected virtual Vector3 PontoAlvo()
    {
        Vector3 normalizado = (tHeroi.position - transform.position);

        normalizado = new Vector3(normalizado.x, 0, normalizado.z);

        return normalizado.normalized;
    }

    protected virtual float DistanciaDePerseguicao
    {
        get { return distanciaDePerseguicao; }
    }

    void seMovaInimigo()
    {

        aInfo = animator.GetCurrentAnimatorStateInfo(0);
        Vector3 normalizado = PontoAlvo();
        
        if (Vector3.Distance(tHeroi.position, transform.position) < DistanciaDePerseguicao)
        {
            if (Vector3.Dot(transform.forward, normalizado) > 0.9f)
            {
                ParaAnimacaoDeGiro();
                if (aInfo.IsName("padrao") || aInfo.IsName("proLado")) 
                {
                    estavaAndando = false;
                    rigid.velocity = Vector3.zero;
                    animator.SetTrigger("avante");
                }
                else if (aInfo.IsName("andando"))
                {
                    if (!estavaAndando)
                    {

                        rigid.AddForce(normalizado * impulso * Velocidade + 100 * Vector3.up);
                        estavaAndando = true;
                    }
                }
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(normalizado), 30 * Time.deltaTime);
                AnimaGiro();
            }
        }
    }

    protected virtual void AnimaGiro()
    { }

    protected virtual void ParaAnimacaoDeGiro()
    { }  


}
