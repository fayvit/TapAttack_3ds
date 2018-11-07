using UnityEngine;
using System.Collections;

public class MaterialPiscante : MonoBehaviour
{
    public Material materialPiscante;
    [SerializeField]private PiscarMaterial[] piscar;

    private GameObject particulaDoFogo;
    private GameObject heroi;
    private Material[] materiaisPiscantes;
    private InimigoBase doMeuInimigo;
    private float tempoDecorrido = 0;
    private int danosAplicados = 0;

    private const float TEMPO_QUEIMANDO = 3;
    private const int DANO_POR_SEGUNDO =1;


    // Use this for initialization
    void Start()
    {
        heroi = GameObject.FindWithTag("Player");
        doMeuInimigo = GetComponent<InimigoBase>();
        //comecouPiscar = true;
        Renderer[] MR = GetComponentsInChildren<Renderer>();
        
        piscar = new PiscarMaterial[MR.Length];
        materiaisPiscantes = new Material[MR.Length];
        for (int j = 0; j < MR.Length; j++)
        {
           materiaisPiscantes[j] =  InserirMaterialParaPiscar(MR[j],materialPiscante);
            piscar[j] = new PiscarMaterial();
        }

        InserirParticulaEstouQueimando();
        
    }

    void InserirParticulaEstouQueimando()
    {
        GameObject G = ControladorDeJogo.c.RetornaElemento(Elementos.estouQueimando);
        particulaDoFogo = (GameObject)Instantiate(G, transform.position, G.transform.rotation, transform);
    }

    public static void RemoverMaterialParaPiscar(Renderer MRj)
    {
        Material[] Ms = new Material[MRj.materials.Length -1 ];

        for (int i = 0; i < MRj.materials.Length-1; i++)
        {
            Ms[i] = MRj.materials[i];
        }

        MRj.materials = Ms;
    }

    public static Material InserirMaterialParaPiscar(Renderer MRj, Material materialPiscante)
    {
        Material[] Ms = new Material[MRj.materials.Length + 1];

        for (int i = 0; i < MRj.materials.Length; i++)
        {
            Ms[i] = MRj.materials[i];
        }

        materialPiscante = new Material(materialPiscante);
        Ms[Ms.Length - 1] = materialPiscante;

        MRj.materials = Ms;
        return materialPiscante;
    }

    void TerminaQueimar()
    {
        Renderer[] MR = GetComponentsInChildren<Renderer>();
        for (int j = 0; j < MR.Length; j++)
        {
            RemoverMaterialParaPiscar(MR[j]);
        }

        Destroy(particulaDoFogo);

        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        tempoDecorrido += Time.deltaTime;

        for(int i=0;i<piscar.Length;i++)
            piscar[i].PiscarCor(materiaisPiscantes[i]);

        
        if (tempoDecorrido > danosAplicados+1)
        {
            doMeuInimigo.TomaDano(DANO_POR_SEGUNDO, TipoDeDano.fogo,heroi);
            danosAplicados++;
        }

        
        if (tempoDecorrido > TEMPO_QUEIMANDO)
        {
            TerminaQueimar();
        }
    }
}
