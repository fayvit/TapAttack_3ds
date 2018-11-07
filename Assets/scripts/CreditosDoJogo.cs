using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditosDoJogo : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SairDosCreditos()
    {
        SceneManager.LoadScene("novoTitulo");
    }

    public void JonasCamargo()
    {
        Application.OpenURL("https://www.facebook.com/jonasdc.art/?fref=ts");
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
    
}
