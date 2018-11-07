using UnityEngine;
using System.Collections;

public class TriggerMudaFase : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("inicial");
            ControladorGlobal.c.DadosGlobais.fizTutorial = true;
        }
    }
}