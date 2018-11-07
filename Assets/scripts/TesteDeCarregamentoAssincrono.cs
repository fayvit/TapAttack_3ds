using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TesteDeCarregamentoAssincrono : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        print("trigger");
        if (col.tag == "Player")
        {
            print("aqui");
            SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
        }
    }
}
