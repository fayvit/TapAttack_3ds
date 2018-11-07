using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Sobre : MonoBehaviour
{
    [System.Serializable]
    private struct TextsDoPainelDoPerfil
    {
        [Header("Dados Principais")]
        public Text nomeDoPerfil;
        public Text quantidadeDeDinheiro;
        public Text quantidadeDeEstrelas;

        [Space(1)][Header("Estatisticas")]
        public Text quantidadeDaPontuacao;
        public Text numeroDeCombos;
        public Text numMaxMoedas;
        public Text numMaxCubos;
        public Text numMaxEsferas;
        public Text numMaxEstaminas;
        public Text nivelMaxAlcancado;
        public Text numInimigosDerrotados;
    }

    [SerializeField]private TextsDoPainelDoPerfil txt;

    private bool foi = false;


    void Start()
    {
        if (ControladorGlobal.c != null && !foi)
        {
            foi = true;

            Perfil P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
            if (txt.nomeDoPerfil != null)
            {
                txt.nomeDoPerfil.text = P.NomeDoPerfil;
                txt.quantidadeDeDinheiro.text = P.Dinheiro.ToString();
                txt.quantidadeDeEstrelas.text = P.EstrelasDeCristal.ToString();
            }

            if (txt.numeroDeCombos != null)
            {
                txt.numeroDeCombos.text = P.ComboMaximoAlcancado.ToString();
                txt.numInimigosDerrotados.text = P.NumeroMaxInimigosDerrotadosEmunicoJogo.ToString();
                txt.numMaxCubos.text = P.NumeroMaximoDeCheckCombosEmUnicoJogo.ToString();
                txt.numMaxEsferas.text = P.NumeroMaximoDeEsferasEmUnicoJogo.ToString();
                txt.numMaxEstaminas.text = P.NumeroMaximoDeEstaminasEmUnicoJogo.ToString();
                txt.numMaxMoedas.text = P.NumeroMaximoDeMoedasEmUnicoJogo.ToString();
                txt.nivelMaxAlcancado.text = P.NivelMaximoAlcancado.ToString();
                txt.quantidadeDaPontuacao.text = P.MaiorPontuacao.ToString();
            }
        }
    }

    void Update()
    {
        Start();
    }

    public void BotaoVoltar()
    {
        SceneManager.LoadScene("Perfil");
    }

    private void OnGUI()
    {
        if (txt.numeroDeCombos != null)
        {
            int w = 320;
            int h = 240;
            if (GUI.Button(new Rect(0.85f * w, 0.03f * h, 0.12f * w, 0.15f * h), "X", ((GUISkin)Resources.Load("meuSkin")).button))
            {
                BotaoVoltar();
            }
        }
    }
}
