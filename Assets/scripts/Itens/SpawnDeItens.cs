using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnDeItens
{
    [SerializeField] private ItemSpawnado[] itens;
    [SerializeField] private float distanciaMin = 40;
    [SerializeField] private float distanciaMax = 150;

    private Transform heroi;
    private bool enabled = true;

    public void AlterarTaxaDeSpawndoItem(NomeItem nome,float mod)
    {
        int indice = 0;
        for (int i = 0; i < itens.Length; i++)
        {
            if (itens[i].Nome == nome)
                indice = i;
        }

        itens[indice].Taxa += mod;
    }

    // Use this for initialization
    public void Start()
    {
        heroi = GameObject.FindWithTag("Player").transform;

        if (!heroi)
        {
            Debug.LogWarning("Heroi não setado corretamente no spawn de itens");
            enabled = false;
        }
    }

    // Update is called once per frame

    public void Update()
    {
        if (enabled)
            bUpdate();
    }

    void bUpdate()
    {
        bool foi = false;
        float distanciaAlvo;
        Vector3 dir = Vector3.zero;
        PosNoMapa pos = new PosNoMapa();

        for (int i = 0; i < itens.Length; i++)
        {
            foi = false;
            distanciaAlvo = Random.Range(distanciaMin, distanciaMax);
            if (itens[i].VerificaNovoSpawn())
            {
                int cont = 0;
                while (!foi && cont<100)
                {
                    cont++;
                    dir = Vector3.ProjectOnPlane(Random.insideUnitSphere, Vector3.up).normalized;
                    dir *= distanciaAlvo;

                    dir = MelhoraInstancia.PosEmparedado(heroi.position + dir, heroi.position);
                    pos = MelhoraInstancia.EstaNoMapa(dir);
                    if(Vector3.Distance(heroi.position,pos.pos)>distanciaAlvo-1)
                        foi = pos.estaNoMapa;
                }

                if (foi)
                {
                    GameObject G = ControladorDeJogo.c.RetornaElemento(
                        (Elementos)System.Enum.Parse(typeof(Elementos),itens[i].Nome.ToString())
                        );
                    G = (GameObject)MonoBehaviour.Instantiate(G,pos.pos+1.5f*Vector3.up,G.transform.rotation);

                    if (itens[i].Nome == NomeItem.estrelaDeCristal)
                        EstrelaDeCristal.EstrelasEmCampo.Add(G);
                }
            }
        }
    }
}

[System.Serializable]
public struct ItemSpawnado
{
    [SerializeField] private NomeItem nome;
    [SerializeField] private float taxa;
    [SerializeField] private float intervaloMinDeSpawn;
    [SerializeField] private float intervaloMaxDeSpawn;
    [SerializeField] private float contadorDeTempo;
    [SerializeField] private float tempoParaProximoSpawn;

    public NomeItem Nome
    {
        get { return nome; }
    }

    public float Taxa
    {
        get { return taxa; }
        set{ taxa = value; }
    }

    public bool VerificaNovoSpawn()
    {
        bool retorno = false;
        contadorDeTempo += Time.deltaTime;
        if (contadorDeTempo > tempoParaProximoSpawn && VerificaEstrela())
        {
            contadorDeTempo = 0;
            retorno = true;
            tempoParaProximoSpawn = Random.Range((1/Taxa)*intervaloMinDeSpawn,(1/Taxa)*intervaloMaxDeSpawn);
        }
        return retorno;
    }

    bool VerificaEstrela()
    {
        bool retorno = true;
        if (nome == NomeItem.estrelaDeCristal)
        {
            retorno = EstrelaDeCristal.PodeSpawnarMaisEstrela();
        }

        return retorno;
    }
}

public enum NomeItem
{
    Moeda,
    CristalDeEspecial,
    checkCombo,
    maisEstamina,
    TNT,
    estrelaDeCristal
}
