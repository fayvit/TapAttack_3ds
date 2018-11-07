using UnityEngine;
using System.Collections;

public class EstouSpawnando : MonoBehaviour
{
    [SerializeField]private GameObject oSpawnado;
    [SerializeField]private float tempoMinDeVortice = 3;
    [SerializeField]private float tempoMaxDeVortice = 5;

    private float tempoDecorrido = 0;
    private float tempoAtualDeSpawn = 3;

    public GameObject OSpawnado
    {
        get { return oSpawnado; }
        set { oSpawnado = value; }
    }

    // Use this for initialization
    void Start()
    {
        tempoAtualDeSpawn = Random.Range(tempoMinDeVortice, tempoMaxDeVortice);
    }

    // Update is called once per frame
    void Update()
    {
        tempoDecorrido+= Time.deltaTime;
        if (tempoDecorrido >= tempoAtualDeSpawn)
        {
            
            ControladorDeJogo.c.Spawn.InimigoAdicionadoAoCampo(
                (GameObject)Instantiate(OSpawnado, transform.position, OSpawnado.transform.rotation)
                );
            Destroy(gameObject);
        }
    }
}
