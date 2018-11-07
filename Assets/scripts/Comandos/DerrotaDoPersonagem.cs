using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class DerrotaDoPersonagem:MonoBehaviour
{
    [SerializeField]private Text textoEstrelas;
    [SerializeField]private GameObject botaoVidaPorEstrela;
    [SerializeField]private GameObject botaoVidaPorPropaganda;

    void Start()
    {
        Invoke("PararTempo", 1.5f);
        textoEstrelas.text = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.EstrelasDeCristal.ToString();

        botaoVidaPorEstrela.SetActive(!ControladorDeJogo.c.UsouVidaPorEstrela);
       // botaoVidaPorPropaganda.SetActive(!ControladorDeJogo.c.UsouVidaPorPropaganda);
    }

    void PararTempo()
    {
        Time.timeScale = 0;
    }

    public void FinalizaJogo()
    {
//        AdsManager ads = FindObjectOfType<AdsManager>();
        bool temPropaganda = false;
  //      if (ads)
    //        temPropaganda = ads.IntertitialVideoCarregado;

        Time.timeScale = 1;

        if (temPropaganda)
            SceneManager.LoadScene("nossosPatrocinadores");
        else
            SceneManager.LoadScene("contadoraDePontos_plus");
    }

    public void BotaoVidaPorEstrela()
    {
        if (ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.EstrelasDeCristal > 0)
        {
            ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.EstrelasDeCristal--;
            FindObjectOfType<EstadoDePersonagem_Gerente>().InserirVidaExtra(MensagemVidaExtra.tipoDeVidaExtra.vidaEstrela);
            ControladorDeJogo.c.UsouVidaPorEstrela = true;
            Destroy(gameObject);
        }

    }

    public void BotaoVidaPorPropaganda()
    {


    }
}
