using UnityEngine;
using System.Collections;

public class MensagemVidaExtra : MonoBehaviour
{
    [SerializeField]private PainelUmaMensagem umaMensagem;

    private tipoDeVidaExtra tipo = tipoDeVidaExtra.bonusPersonagem;

    public enum tipoDeVidaExtra
    {
        bonusPersonagem,
        anel,
        vidaEstrela
    }

    public tipoDeVidaExtra Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }

    // Use this for initialization
    void Start()
    {
        if (tipo != tipoDeVidaExtra.vidaEstrela)
        {
            Invoke("PararTempo", 1.5f);
            umaMensagem.retornar += RetornaMensagem;
        }
        else
        {
            umaMensagem.ConstroiPainelUmaMensagem(MaisVidaParaOJogo, "Seu personagem recebeu uma vida extra");
        }
    }

    void PararTempo()
    {
        Time.timeScale = 0;
    }

    void RetornaMensagem()
    {
        
        switch (tipo)
        {
            case tipoDeVidaExtra.anel:
                StartCoroutine(MensagemDeAnel());
            break;
            case tipoDeVidaExtra.bonusPersonagem:
                //MensagemDeBonus();
                StartCoroutine(MensagemDeBonus());
            break;
        }
    }

    IEnumerator MensagemDeAnel()
    {
        
        yield return new WaitForSecondsRealtime(0.5f);
        
        umaMensagem.ConstroiPainelUmaMensagem(MaisVidaParaOJogo,
            "O anel de vida extra começa a brilhar até o personagem acordar pronto para um novo desafio");
    }

    IEnumerator MensagemDeBonus()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        umaMensagem.ConstroiPainelUmaMensagem(MaisVidaParaOJogo,
            "O bonus do personagem lhe dá direito a mais uma vida");
    }

    void MaisVidaParaOJogo()
    {
        Time.timeScale = 1;
        GameObject G = ControladorDeJogo.c.RetornaElemento(Elementos.reviviParticulas);
        Vector3 posHeroi = GameObject.FindWithTag("Player").transform.position;
        Instantiate(G,posHeroi,G.transform.rotation);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
