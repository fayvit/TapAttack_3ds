using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorDeRecompensas : MonoBehaviour
{
    [SerializeField] private GameObject painelDeMissaoCumprida;
    [SerializeField] private GameObject containerDaMissaoCumprida;
    [SerializeField] private Text textoNaoTemRecompensa;

    [SerializeField] private bool InserirRecompensaDeTeste = false;

    private bool passouDeNivel = false;

    // Use this for initialization
    void Start()
    {


        //Missoes[] Ms = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.GMissoes.MissoesAtuais;
        if(InserirRecompensaDeTeste)
            ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado
                .Recompensas.Add(new RecompensaPorMissao(TipoMissao.alcanceCombo,1,10));

        Recompensa[] Rs= ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.Recompensas.ToArray();

        containerDaMissaoCumprida.GetComponent<RectTransform>().sizeDelta
                    = new Vector2(0, Rs.Length * painelDeMissaoCumprida.GetComponent<LayoutElement>().preferredHeight);

        for (int i = 0; i < Rs.Length; i++)
        {
            //if (ResultadoDasMissoes.MissaoTeveResultado(i))
            {
                GameObject G = Instantiate(painelDeMissaoCumprida);
                G.GetComponent<MostrarRecompensa>().SetarRecompensa(Rs[i]);
                RectTransform T = G.GetComponent<RectTransform>();
                T.SetParent(containerDaMissaoCumprida.transform);
                T.localScale = new Vector3(1, 1, 1);

                T.offsetMax = Vector2.zero;
                T.offsetMin = Vector2.zero;   
            }
        }

        if (Rs.Length > 0)
            textoNaoTemRecompensa.enabled = false;

        painelDeMissaoCumprida.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PassouDeNivel()
    {
        passouDeNivel = true;
        FindObjectOfType<BotaoNivelDoJogadorNoPerfil>().PassouDeNivel();
        FindObjectOfType<BotaoRecompensaDoPerfil>().GetComponent<Button>().interactable = false;
        
    }

    public void BotaoContinuar()
    {
        if (passouDeNivel)
        {
            SceneManager.LoadScene("niveis_plus");
        }
        else
        {
            ControladorGlobal.c.Estado = EstadoDoSoftware.retornandoParaPerfilDoTitulo;
            SceneManager.LoadScene("PreJogo");
        }
    }
}
