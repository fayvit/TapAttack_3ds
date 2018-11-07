using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpawnerDeInimigos
{
    [SerializeField] private GameObject vorticeDeSpawn;
    [SerializeField] private GameObject[] inimigos;
    [SerializeField] private float tempoDeSpawn = 7f;
    [SerializeField] private int maxInimigosDeSpawn = 15;
    [SerializeField] private int numeroInicialDeInimigos = 10;
    [SerializeField] private float distanciaMinSpawn = 10;
    [SerializeField] private float distanciaMaxSpawn = 50;

    private Transform heroi;
    private DadosDoPersonagem dados;
    private List<GameObject> inimigosEmCampo = new List<GameObject>();
    private float contadorDeTempo = 0;
    private bool enabled = true;

    // Use this for initialization
    public void Start()
    {
        GameObject G = GameObject.FindWithTag("Player");
        if (G)
        {
            heroi = G.transform;
            dados = G.GetComponent<EstadoDePersonagem_Gerente>().Dados;
        }
        else
        {
            Debug.LogError("Heroi não encontrado");
            enabled = false;
        }

        for (int i = 0; i < numeroInicialDeInimigos; i++)
            SpawnarInimigo();
    }

    // Update is called once per frame
    public void Update()
    {
        if (enabled)
            bUpdate();
    }
    void bUpdate()
    {
        contadorDeTempo += Time.deltaTime;
        if (contadorDeTempo > tempoDeSpawn)
        {
            VerificaInimigosDerrotados();
            if (inimigosEmCampo.Count < maxInimigosDeSpawn)
            {
                SpawnarInimigo();

                if (inimigosEmCampo.Count < 6)
                {
                    for (int i = 0; i < maxInimigosDeSpawn-10; i++)
                        SpawnarInimigo();
                }
            }

            contadorDeTempo = 0;
        }
    }

    void VerificaInimigosDerrotados()
    {
        List<int> retirar = new List<int>();
        for (int i=0;i<inimigosEmCampo.Count;i++)
        {
            if (inimigosEmCampo[i] == null)
                retirar.Add(i);
        }

        for(int i = retirar.Count-1;i>=0;i--)
            inimigosEmCampo.RemoveAt(retirar[i]);
    }

    void SpawnarInimigo()
    {

        Vector3 onde = Vector3.zero;
        PosNoMapa p;
        bool noMapa = false;
        int cont = 0;

        while (!noMapa )
        {
            onde = Vector3.ProjectOnPlane(Random.insideUnitSphere, Vector3.up).normalized;            
            onde = Random.Range(distanciaMinSpawn, distanciaMaxSpawn) * onde + heroi.position;            
            cont++;            
            p = MelhoraInstancia.EstaNoMapa(onde);
            noMapa = p.estaNoMapa;
            onde = p.pos;
        }

       GameObject G = MonoBehaviour.Instantiate(vorticeDeSpawn, onde, vorticeDeSpawn.transform.rotation);
        SelecionarInimigoSpawnado(G);
        inimigosEmCampo.Add(G);
    }

    void SelecionarInimigoSpawnado(GameObject G)
    {
        EstouSpawnando estou = G.GetComponent<EstouSpawnando>();
        int esse = InimigosPorNivel.EncontravelDoNivel(dados.NivelParaMostrador);
        estou.OSpawnado = inimigos[esse];
    }

    public void InimigoAdicionadoAoCampo(GameObject oInimigo)
    {
        inimigosEmCampo.Add(oInimigo);
    }
}
