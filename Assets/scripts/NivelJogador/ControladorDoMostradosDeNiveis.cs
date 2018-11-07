using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorDoMostradosDeNiveis : MonoBehaviour
{
    [SerializeField]private ScrollRect mascaraDoContainer;
    [SerializeField]private RectTransform containerDeTamanhoVariavel;
    [SerializeField]private GameObject itemDoContainer;
    [SerializeField]private Text mostradorDaPontuacao;
    [SerializeField]private int numeroDeNiveisMostraveis = 60;

    private IGerenciadorDeExperiencia gXP;
    // Use this for initialization
    void Start()
    {
        RecalculaTamanhoDoContainer();

        for (int i = numeroDeNiveisMostraveis; i >0; i--)
        {
            ParentearNaHUD.Parentear(itemDoContainer,containerDeTamanhoVariavel).GetComponent<AtualizadorDosTextosDeNivel>().AtualizaTextosDeNivel(i);
        }

        itemDoContainer.SetActive(false);
        gXP = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.NivelJogador;
        
        mostradorDaPontuacao.text = gXP.XP + " / " + gXP.ParaProxNivel;
        Invoke("MeLeveParaMinhaAltura", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MeLeveParaMinhaAltura()
    {
        mascaraDoContainer.verticalNormalizedPosition = (float)(Mathf.Max(gXP.Nivel-3,0))/(numeroDeNiveisMostraveis-3);
    }

    void RecalculaTamanhoDoContainer()
    {
        containerDeTamanhoVariavel.sizeDelta
                    = new Vector2(0, numeroDeNiveisMostraveis * itemDoContainer.GetComponent<LayoutElement>().preferredHeight);
    }

   

    public void NiveisParaGanharAgora(int inicio,int fim)
    {
        for (int i = inicio; i < fim; i++)
        {
            containerDeTamanhoVariavel.GetChild(numeroDeNiveisMostraveis - i)
                .GetComponent<AtualizadorDosTextosDeNivel>().CorDoTexto(Color.black);
        }
    }

    public void ParticulaDaBarraDeXP(int nivel)
    {
        containerDeTamanhoVariavel.GetChild(numeroDeNiveisMostraveis - nivel)
                .GetComponent<AtualizadorDosTextosDeNivel>().ParticulaXP();
    }

    public void ParticulaDoTextoDeNivel(int nivel)
    {
        containerDeTamanhoVariavel.GetChild(numeroDeNiveisMostraveis - nivel)
                .GetComponent<AtualizadorDosTextosDeNivel>().ParticulaTexto();
    }

    public void BotaoContinuar()
    {
        ControladorGlobal.c.Estado = EstadoDoSoftware.telaTitulo;
        SceneManager.LoadScene("PreJogo");
    }
}
