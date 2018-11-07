using UnityEngine;
using System.Collections;

public class movimentacaoPointinTouch {

    private Animator animator;
    private UnityEngine.AI.NavMeshAgent nav;
    private bool parandoMovimento = false;

	// Use this for initialization
	public movimentacaoPointinTouch (GameObject G) {
        nav = G.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = G.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void Update () {
        
            
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(raio, out hit))
        {

            nav.destination = hit.point;
            
        }

        if (parandoMovimento)
        {
            nav.destination = nav.transform.position;
            parandoMovimento = false;
            ControladorDeJogo.c.mostradorDeAlvo.SetActive(false);
        }

        
    }

    public void AnimaMove()
    {        
        animator.SetFloat("velocidade", Vector3.ProjectOnPlane(nav.velocity, Vector3.up).magnitude);
        

        if (Vector3.Distance(nav.transform.position, nav.destination) < 2f)
        {
            ControladorDeJogo.c.mostradorDeAlvo.SetActive(false);
            
        }
        else
        {
            ControladorDeJogo.c.mostradorDeAlvo.transform.position = nav.destination + 0.25f * Vector3.up;
            ControladorDeJogo.c.mostradorDeAlvo.SetActive(true);
        }
    }

    public void AnimaDerrotado()
    {
        animator.SetBool("morto", true);
    }


    public void AnimaTomeiDano()
    {
        GameObject obj = ControladorDeJogo.c.RetornaElemento(Elementos.parTomeiDano);
        MonoBehaviour. Destroy(
            MonoBehaviour.Instantiate(obj, nav.transform.position + Vector3.up, obj.transform.rotation),
            0.5f
            );

        animator.Play("tomouDano");
    }

    public void PararMovimento()
    {
        nav.destination = nav.transform.position;
        parandoMovimento = true;
        
    }

    public void DesativaMorto()
    {
        PararMovimento();
        animator.SetBool("morto", false);
    }
}
