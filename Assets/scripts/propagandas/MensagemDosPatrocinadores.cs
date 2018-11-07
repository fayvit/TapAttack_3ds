using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MensagemDosPatrocinadores : MonoBehaviour
{
    [SerializeField]private GameObject containerDoEntendi;
    [SerializeField]private GameObject loadBar;
    [SerializeField]private PainelUmaMensagem umaMensagem;

    //private AdsManager ads;
    // Use this for initialization
    void Start()
    {
        //ads = FindObjectOfType<AdsManager>();
       // ads.PreparaVideo(AoCarregar,AoFalhar,AoFechar,AoSairDaAplicacao);
        //print("aqui");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    void AoFechar(object sender, System.EventArgs args)
    {
      //  umaMensagem.ConstroiPainelUmaMensagem(r, "Fechou");
    }

    void AoSairDaAplicacao(object sender, System.EventArgs args)
    {
      //  umaMensagem.ConstroiPainelUmaMensagem(r, "Saiu da aplicação");
    }

    void AoFalhar(object sender, GoogleMobileAds.Api.AdFailedToLoadEventArgs args)
    {
     //   umaMensagem.ConstroiPainelUmaMensagem(r, "Falhou");
    }*/

    void AoCarregar(object sender, System.EventArgs args)
    {
        /*
        containerDoEntendi.SetActive(true);
        loadBar.SetActive(false);
        umaMensagem.ConstroiPainelUmaMensagem(r, "Propaganda carregada");
        Debug.Log("O que isso faz??");*/
    }

    void r() { }

    public void BotaoEntendi_MostrarVideo()
    {
        //ads.ShowInterstitialVideo();
        SceneManager.LoadScene("contadoraDePontos_plus");

    }
}
