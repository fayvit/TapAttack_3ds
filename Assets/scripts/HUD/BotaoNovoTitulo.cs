using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class BotaoNovoTitulo : MonoBehaviour
{
    int w = 320;
    int h = 240;
    [SerializeField]private GUISkin skin;
    // Use this for initialization
    void Start()
    {
     

        //UnityEngine.N3DS.Keyboard.SetType(N3dsKeyboardType.Qwerty);
        /*AdsManager ads = FindObjectOfType<AdsManager>();
        
        if (ads)
        {
            ads.AbrirNative();
            ads.CarregarIntertialBanner();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BotaoJogar();
        }
    }

    /*
    private void OnGUI()
    {

        if (GUI.Button(new Rect(160 - 60, 240 - 32, 120, 30), "Show Keyboard"))
        {
            UnityEngine.N3DS.Keyboard.Show();
        }
    }*/

    public void BotaoJogar()
    {
        /*
        AdsManager ads = FindObjectOfType<AdsManager>();
        if (ads)
            ads.FecharAdNative();      
            */

        SceneManager.LoadScene("Perfil");
    }

    public void BotaoCreditos()
    {
        /*
        AdsManager ads = FindObjectOfType<AdsManager>();
        if (ads)
            ads.FecharAdNative();
            */

        SceneManager.LoadScene("Creditos");
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0.32f*w,0.78f*h,0.37f*w,0.14f*h), "Jogar",skin.button))
        {
            BotaoJogar();
        }
    }
}
