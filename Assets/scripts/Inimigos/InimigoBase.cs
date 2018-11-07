using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InimigoBase : MonoBehaviour
{
    [SerializeField]private int vida = 10;
    [SerializeField]private int vidaMax = 10;
    [SerializeField]private int ataque = 1;
    [SerializeField]private float velocidade = 1;
    [SerializeField]private Defesas defesa;

    private DadosDoPersonagem dados;

    public float Velocidade
    {
        get { return velocidade; }
    }

    public int Ataque
    {
        get{ return ataque; }
    }

    // Use this for initialization
    protected void Start()
    {
        dados = GameObject.FindWithTag("Player").GetComponent<EstadoDePersonagem_Gerente>().Dados;
        int nivelFinal = dados.G_XP.Nivel + 1;
        int nivelInicial = Mathf.Max(1, nivelFinal - 4);
        AplicaNivel(
            Random.Range(nivelInicial, nivelFinal)
            );
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        /*
		if (col.gameObject.name != "Terrain")
			print(col.gameObject);
			*/
        if (col.gameObject.tag == "Player")
        {
            bool bateu = false;
            foreach (ContactPoint c in col.contacts)
                if (Vector3.Dot(c.normal, Vector3.up) > 0.9f)
                    bateu = true;

            if (bateu)
            {
                print("bateu");
            }
            else
            {
                col.gameObject.GetComponent<EstadoDePersonagem_Gerente>().AplicarDano(
                    GetComponent<InimigoBase>().Ataque
                    );
            }
        }
    }

    public virtual void AfastaInimigoDeDano(Vector3 dir, float distancia, float tempo)
    {
        Debug.LogWarning("Deve sofrer sobrecarga da classe concreta do inimigo");
    }

    public void AplicaNivel(int nivel)
    {        
        vida    = 6 + nivel * 5;
        vidaMax = 6 + nivel * 5;
        ataque  = nivel;
        velocidade = Mathf.Max(0.2f,1+(dados.NivelParaMostrador-1)*0.25f+Mathf.Max(1,nivel)*0.05f);
        defesa.contra = new Dictionary<TipoDeDano, int>() {
            { TipoDeDano.fisico,Mathf.Max(1,(int)(nivel/3))},
            { TipoDeDano.fogo,Mathf.Max(1, (int)(nivel / 3))},
            { TipoDeDano.gelo,Mathf.Max(1, (int)(nivel / 3))},
            { TipoDeDano.raio,Mathf.Max(1, (int)(nivel / 3))},
            { TipoDeDano.magico,Mathf.Max(1, (int)(nivel / 3))}};

    }

    public void TomaDano(int valorDeDano,TipoDeDano tipo,GameObject atacante = null)
    {
        vida -= Mathf.Max(1, valorDeDano - defesa.contra[tipo]);

        if (vida <= 0)
        {
            if (atacante)
            {
                atacante.GetComponent<EstadoDePersonagem_Gerente>().Dados.AplicaXP((int)((vidaMax + ataque + velocidade) / 2));
                ControladorDeJogo.c.G_Combos.AdicionaCombo(100);
                ControladorGlobal.c.EmJogo.Inimigos++;

                if (tipo == TipoDeDano.magico)
                    VerificaAcoesEspeciais.VerificaAcaoDeBonus(transform.position,
                        ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.PersonagemAtualSelecionado.Bonus
                        );
            }

            Destroy(gameObject);
            
        }
    }

    public static GameObject[] InimigosPerto(float distancia,Vector3 deOnde)
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("inimigo");        
        List<GameObject> osPerto = new List<GameObject>();
        foreach (GameObject I in inimigos)
        {
            if (Vector3.Distance(I.transform.position, deOnde) < distancia)
            {
                osPerto.Add(I);
            }
        }

        return osPerto.ToArray();
    }
}

public enum TipoDeDano
{
    fisico,
    fogo,
    raio,
    gelo,
    magico,
    explosao
}

[System.Serializable]
public class Defesas
{
    [SerializeField]private int defesaFisica = 0;
    [SerializeField]private int defesaFogo = 0;
    [SerializeField]private int defesaGelo = 0;
    [SerializeField]private int defesaRaio = 0;
    [SerializeField]private int defesaMagica = 0;
    [SerializeField]private int defesaExplosiva= 0;

    public Dictionary<TipoDeDano, int> contra
    {
        get {
            return new Dictionary<TipoDeDano, int>()
        {
            { TipoDeDano.fisico,defesaFisica},
            { TipoDeDano.fogo,defesaFogo},
            { TipoDeDano.gelo,defesaGelo},
            { TipoDeDano.raio,defesaRaio},
            { TipoDeDano.magico,defesaMagica},
            { TipoDeDano.explosao,defesaExplosiva}
        };
                }

        set {
            defesaFisica = value[TipoDeDano.fisico];
            defesaFogo   = value[TipoDeDano.fogo];
            defesaGelo   = value[TipoDeDano.gelo];
            defesaRaio   = value[TipoDeDano.raio];
            defesaMagica = value[TipoDeDano.magico];
        }
    }
}
