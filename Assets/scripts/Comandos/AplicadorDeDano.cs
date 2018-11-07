using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AplicadorDeDano : MonoBehaviour
{
    protected List<GameObject> acertados = new List<GameObject>();
    public GameObject dono;
    public int dano = 1;
    // Use this for initialization
    protected void Start()
    {
        Elementos e = Random.Range(0, 2) == 0 ? Elementos.somDoAtaque : Elementos.somDoAtaque2;
        Destroy(Instantiate(ControladorDeJogo.c.RetornaElemento(e)),5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void AplicaDanoEmInimigo(Collision col,
                    TipoDeDano tipo = TipoDeDano.fisico,
                    Elementos elemento = Elementos.parDano)
    {
        
            col.gameObject.GetComponent<InimigoBase>().TomaDano(dano, tipo, dono);

            Destroy(
                Instantiate(ControladorDeJogo.c.RetornaElemento(elemento),
                col.transform.position,
                ControladorDeJogo.c.RetornaElemento(elemento).transform.rotation), 1
                );

            acertados.Add(col.gameObject);
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "inimigo")
        {
            if (!acertados.Contains(col.gameObject))
            {
                AplicaDanoEmInimigo(col);
            }
        }
    }
}
