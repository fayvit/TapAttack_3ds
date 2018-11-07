using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MostrarRecompensa : MonoBehaviour
{
    [SerializeField]private GameObject botaoDoPainel;
    [SerializeField]private RecebiAlgo recebido;
    [SerializeField]private Text textoDoMotivoDaRecompensa;
    [SerializeField]private Text[] numRecompensa;
    [SerializeField]private Text[] textoRecompensa;
    [SerializeField]private GameObject containerDosPaineisRecompensa;
    [SerializeField]private GameObject particulaDeCoisasBoas;
    [SerializeField]private GameObject particulaUpeiDeNivel;

    private Recompensa R;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetarRecompensa(Recompensa R)
    {
        this.R = R;
        textoDoMotivoDaRecompensa.text = R.TextoDaRecompensa;

        for (int i = 0; i < R.Valores.Length; i++)
        {
            numRecompensa[i].text = R.Valores[i].Quantidade.ToString();
            textoRecompensa[i].text = R.Valores[i].Tipo.ToString();

            if (R.Valores[i].Tipo == tipoDeRecompensas.xp)
            {
                textoRecompensa[i].fontSize = 18;
            }
            else
                textoRecompensa[i].fontSize = 14;

            if (R.Valores[i].Quantidade < 1000)
                numRecompensa[i].fontSize = 20;
        }

    }

    public void BotaoMostrarRecompensa()
    {
        botaoDoPainel.SetActive(false);
        containerDosPaineisRecompensa.SetActive(true);
        AplicaParticulas();
        AplicarRecompensa();
    }

    void AplicaParticulas()
    {
        for (int i = 0; i < containerDosPaineisRecompensa.transform.childCount; i++)
        {
            StartCoroutine(InstanciaEssaPArticula(i*0.25f,i));
            
        }
    }

    IEnumerator InstanciaEssaPArticula(float tempo,int i)
    {
        yield return new WaitForSeconds(tempo);
        GameObject G = Instantiate(particulaDeCoisasBoas);
        G.transform.SetParent(containerDosPaineisRecompensa.transform.GetChild(i));
        G.transform.localPosition = Vector3.zero;
        Destroy(G, 1.6f);
    }

    void AplicarRecompensa()
    {
        Perfil PP = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        for (int i = 0; i < R.Valores.Length; i++)
        {
            switch (R.Valores[i].Tipo)
            {
                case tipoDeRecompensas.estrelasDeCristal:
                    PP.EstrelasDeCristal+=R.Valores[i].Quantidade;
                break;
                    /*
                case tipoDeRecompensas.impulsoPoderoso:
                    // a fazer...
                break;
                */
                case tipoDeRecompensas.moedas:
                    PP.Dinheiro += R.Valores[i].Quantidade;
                break;
                    /*
                case tipoDeRecompensas.x2:
                    // a fazer...
                break;*/
                case tipoDeRecompensas.xp:
                    PP.NivelJogador.XP += R.Valores[i].Quantidade;
                    if (PP.NivelJogador.VerificaPassaNivel())
                    {
                        PasseiDeNivel_MeLeve passei = FindObjectOfType<PasseiDeNivel_MeLeve>();
                        if (passei==null)                        
                        {
                            GameObject G = Instantiate(new GameObject());
                            passei =  G.AddComponent<PasseiDeNivel_MeLeve>();
                            recebido.transform.SetParent(G.transform);
                            passei.recebido = recebido;
                            passei.nivelDeSaida = PP.NivelJogador.Nivel;

                            FindObjectOfType<ControladorDeRecompensas>().PassouDeNivel();
                            
                        }

                        PP.NivelJogador.AplicaPassaNivel();
                        passei.nivelAlvo = PP.NivelJogador.Nivel;

                        Invoke("ParticulaComAtraso", 0.6f);
                    }
                break;
            }
        }

        PP.Recompensas.Remove(R);
        ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
    }

    void ParticulaComAtraso()
    {
        GameObject G = Instantiate(particulaUpeiDeNivel);
        Transform G2 = FindObjectOfType<BotaoNivelDoJogadorNoPerfil>().transform;
        G.transform.SetParent(G2);
        G.transform.localPosition = Vector3.zero;
        Destroy(G, 1.6f);
    }
}
