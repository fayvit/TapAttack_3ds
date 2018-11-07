using UnityEngine;
using System.Collections;

public class Dinamite : MonoBehaviour
{
    [SerializeField]private float dano = 1.25f;
    [SerializeField]private float raioDeDano = 3;
    [SerializeField]private float tempoDeExplosao = 1.5f;
    [SerializeField]private float tempoParaPiscar = 4f;
    [SerializeField]private float distanciaDeAfastamento = 10;
    [SerializeField]private float tempoDeAfastamento = 0.75f;
    [SerializeField]private PiscarMaterial piscar;
    [SerializeField]private Material materialPiscante;

    private float tempoDecorrido = 0;
    private bool comecouPiscar = false;
    
    

    private float TempoParaPiscar
    {
        get { return Mathf.Min(tempoParaPiscar, 0.9f * tempoDeExplosao); }
    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido > tempoDeExplosao)
        {
            Explodir();
        }

        if ((tempoDeExplosao - tempoDecorrido) < TempoParaPiscar)
        {            
            if (!comecouPiscar)
                ComecePiscar();

            piscar.PiscarCor(materialPiscante);
        }
    }

    void ComecePiscar()
    {
        comecouPiscar = true;
        MeshRenderer MR = GetComponent<MeshRenderer>();
        materialPiscante = MaterialPiscante.InserirMaterialParaPiscar(MR,materialPiscante);
        //Material[] Ms = new Material[MR.materials.Length + 1];

        //for (int i = 0; i < MR.materials.Length; i++)
        //{
        //    Ms[i] = MR.materials[i];
        //}

        //materialPiscante = new Material(materialPiscante);
        //Ms[Ms.Length - 1] = materialPiscante;

        //MR.materials = Ms;

    }

    void ProcureObjetosPerto()
    {
        GameObject[] osPerto = InimigoBase.InimigosPerto(raioDeDano, transform.position);

        EstadoDePersonagem_Gerente T = GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>();
        DadosDoPersonagem dados = T.Dados;
        float distancia = Vector3.Distance(T.transform.position, transform.position);

        if (distancia < raioDeDano)
        {      
            T.AplicarDano(ValorDeDano(distancia, dados.G_XP.Nivel));
            AfastamentoDeExplosaoParaHeroi afH = T.gameObject.AddComponent<AfastamentoDeExplosaoParaHeroi>();
            afH.dirAfastamento = Vector3.ProjectOnPlane(T.transform.position - transform.position,Vector3.up).normalized;
        }

        foreach (GameObject I in osPerto)
        {
                AfastadorDeInimigoEmDano.InsereAfastamento(
                   Vector3.ProjectOnPlane( (I.transform.position - transform.position),Vector3.up).normalized, distanciaDeAfastamento, tempoDeAfastamento, I);
            I.GetComponent<InimigoBase>()
                .TomaDano(ValorDeDano(Vector3.Distance(I.transform.position,transform.position), dados.G_XP.Nivel),TipoDeDano.explosao);
        }
        
    }

    int ValorDeDano(float distancia,int nivel)
    {
        return  (distancia < Mathf.Max(1, raioDeDano / 3))
                ? (int)dano * nivel
                : (int)(dano * 1 / Mathf.Min(1, raioDeDano - distancia) * nivel);
    }  

    void Explodir()
    {        
        GameObject G = ControladorDeJogo.c.RetornaElemento(Elementos.explosao);
        Destroy(Instantiate(G, transform.position, G.transform.rotation),5f);
        ProcureObjetosPerto();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {        
        if (col.gameObject.tag == "Player")
        {
            Explodir();
        }
    }
}
