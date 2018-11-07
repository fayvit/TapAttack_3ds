using UnityEngine;

[System.Serializable]
public class AtaqueEspecial
{
    [SerializeField]private int numeroDeAtaques = 15;
    [SerializeField]private int danoPorAtaque = 2;
    [SerializeField]private float tempoDeEspecial = 1;
    [SerializeField]private bool iniciou = false;
    [SerializeField]private TipoDeDano tipo = TipoDeDano.magico;

    private int numeroDeDisparos = 0;
    private float tempoDecorrido = 0;        
    private float distanciaHorizontal = 0;
    private float distanciaVertical = 0;
    private DadosDoPersonagem dados;
    private Transform heroi;

    PosNoMapa DistanciaNoMapa(Vector3 final)
    {
        bool foi = true;
        PosNoMapa posM = MelhoraInstancia.TelaParaMundo3D(Vector3.zero);
        Vector3 inicial = posM.pos;
        foi &= posM.estaNoMapa;

        posM = MelhoraInstancia.TelaParaMundo3D(final);
        final = posM.pos;
        foi &= posM.estaNoMapa;

        return new PosNoMapa() { pos = final - inicial,estaNoMapa = foi };
    }
    float DistanciaHorizontal
    {
        get {
            if (distanciaHorizontal == 0)
            {
                PosNoMapa posM = DistanciaNoMapa(new Vector3(Screen.width,0,0));
                if (posM.estaNoMapa)
                    distanciaHorizontal = posM.pos.magnitude;
                else
                    distanciaHorizontal = 40;// inserido para evitar o bug do especial que não sai
            }
            return distanciaHorizontal;
        }
    }

    float DistanciaVertical
    {
        get {
            if (distanciaVertical == 0)
            {
                PosNoMapa posM = DistanciaNoMapa(new Vector3(0, Screen.height, 0 ));
                if (posM.estaNoMapa)
                    distanciaVertical = posM.pos.magnitude;
                else
                    distanciaVertical = 40;// inserido para evitar o bug do especial que não sai
            }
            return distanciaVertical;
        }
    }

    public int NumeroDeAtaques
    {
        get { return numeroDeAtaques; }
        set { numeroDeAtaques = value; }
    }

    public void Iniciar()
    {
        iniciou = true;
    }

    public void Update()
    {
        if (iniciou)
        {
            
            tempoDecorrido += Time.deltaTime;
            float x = 0;
            Transform alvo = null;

            for (int i = 0; i < NumeroDeAtaques; i++)
            {
                
                x = ((float)i / (NumeroDeAtaques) * tempoDeEspecial);
                
                if (tempoDecorrido > x && numeroDeDisparos < i)
                {
                    
                    alvo = SorteiaEle();
                    if(alvo!=null)
                        EspecialNele(alvo);
                    numeroDeDisparos++;
                }
            }

            if (tempoDecorrido >= tempoDeEspecial)
            {
                tempoDecorrido = 0;
                numeroDeDisparos = 0;
                iniciou = false;
            }
        }
    }

    void EspecialNele(Transform ele)
    {
        
        GameObject G = ControladorDeJogo.c.RetornaElemento(Elementos.parDanoEspecial);
        MonoBehaviour.Destroy(
        MonoBehaviour.Instantiate(G,ele.position,G.transform.rotation),0.5f);
        if (dados == null)
        {
            EstadoDePersonagem_Gerente gPersonagem = heroi.GetComponent<EstadoDePersonagem_Gerente>();
            dados = gPersonagem.Dados;            
        }

        //mov.PararMovimento();
        ele.GetComponent<InimigoBase>().TomaDano(danoPorAtaque*dados.Poder, tipo, heroi.gameObject);
    }

    Transform SorteiaEle()
    {
        if (heroi == null)
        {
            SetarHeroi();
        }

        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("inimigo");
        Debug.Log("aqui " + inimigos.Length);
        System.Collections.Generic.List<GameObject> inimigosPerto = new System.Collections.Generic.List<GameObject>();

        for (int i = 0; i < inimigos.Length; i++)
            if (Mathf.Abs(heroi.position.x - inimigos[i].transform.position.x) < DistanciaHorizontal
                    &&
                    Mathf.Abs(heroi.position.z - inimigos[i].transform.position.z) < DistanciaVertical
                    )
            {
                inimigosPerto.Add(inimigos[i]);
            }

        
        return inimigosPerto.Count>0
            ? inimigosPerto[Random.Range(0,inimigosPerto.Count)].transform
            : null;
    }

    void SetarHeroi()
    {
        if (!heroi)
            heroi = GameObject.FindWithTag("Player").transform;

        if (heroi == null)
            Debug.LogError("Heroi não foi setado corretamente");
    }
}

