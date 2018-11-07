using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class ControladorDoResultado : MonoBehaviour
{
    [System.Serializable]
    public class ContainerDaHUD_Recordes
    {
        [SerializeField] public GameObject pontos;
        [SerializeField] public GameObject nivel;
        [SerializeField] public GameObject numCombo;
        [SerializeField] public GameObject moedas;
        [SerializeField] public GameObject checkCombo;
        [SerializeField] public GameObject esferas;
        [SerializeField] public GameObject estaminas;
        [SerializeField] public GameObject inimigos;

        [SerializeField]public GameObject particulaDeCoisasBoas;

        public Dictionary<PropPerfil, GameObject> GameObjectRecords
        {
            get {
                return new Dictionary<PropPerfil, GameObject>()
            {
                { PropPerfil.pontos,pontos },
                { PropPerfil.nivel,nivel },
                { PropPerfil.combo,numCombo},
                { PropPerfil.moedas,moedas},
                { PropPerfil.checkCombo,checkCombo},
                { PropPerfil.esfera,esferas},
                { PropPerfil.estamina,estaminas},
                { PropPerfil.inimigos,inimigos}
                };
            }
        }
            
    }

    [SerializeField] private ContainerDaHUD_Recordes recordesHUD;
    [SerializeField] private Text estrelasHoje;
    [SerializeField]private TentativasExcedidas tentaticasExcedidas;

    [SerializeField] private float contadorDeTempo = 0;
    [SerializeField] private float tempoParaIniciar = 0.25f;
    [SerializeField] private float tempoDeInterpolacao = 3;
    [SerializeField] private float tempoDeSomarDadosPrincipais = 1;

    private bool jaSalvouOResultado = false;
    private Perfil perfilAtual;
    private ContainerDosDadosEmJogo doJogo;

    private EstadoDoContador estado = EstadoDoContador.iniciando;

    [Header("Variaveis de testes")]
    [SerializeField]private bool inserirMissaoVencida = false;

    private enum EstadoDoContador
    {
        iniciando,
        interpolandoPontos,
        aplicaPontos,
        interpolaPontosAplicados,
        finalizado
    }

    // Use this for initialization
    void Start()
    {
        
        perfilAtual = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        doJogo = ControladorGlobal.c.EmJogo;

        if (doJogo == null)
            doJogo = new ContainerDosDadosEmJogo();

        if (estrelasHoje != null)
        {
            estrelasHoje.text = EstrelaDeCristal.NumeroDeEstrelasHoje.ToString() + "/5";
        }
        PainelDoPerfil.t.ZeraResultados();
        PainelDoPerfil.t.ExibeDadosPrincipais(perfilAtual);

        if (inserirMissaoVencida)
            perfilAtual.GMissoes.InserirMissaoVencida();
    }

    // Update is called once per frame
    void Update()
    {
        contadorDeTempo += Time.deltaTime;
        switch (estado)
        {
            case EstadoDoContador.iniciando:
                if (contadorDeTempo > tempoParaIniciar)
                {
                    estado = EstadoDoContador.interpolandoPontos;
                    contadorDeTempo = 0;
                }
            break;
            case EstadoDoContador.interpolandoPontos:
                 InterpolaPontosParaPainel();
                if (contadorDeTempo > tempoDeInterpolacao)
                {
                    contadorDeTempo = 0;
                    estado = EstadoDoContador.aplicaPontos;
                }
            break;
            case EstadoDoContador.aplicaPontos:
                if (!jaSalvouOResultado)
                    SalvarREsultado();
            break;
            case EstadoDoContador.interpolaPontosAplicados:
                InterpolaDinheiro();
                if (contadorDeTempo > tempoDeSomarDadosPrincipais)
                    estado = EstadoDoContador.finalizado;
            break;
        }
    }

    void SalvarREsultado()
    {
        Perfil PP = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        PP.AtualizaPerfil(
            new Perfil()
            {
                ComboMaximoAlcancado = doJogo.ComboMaximoAlcancado,
                Dinheiro = perfilAtual.Dinheiro + doJogo.Moedas,
                MaiorPontuacao = doJogo.Pontuacao,
                NivelMaximoAlcancado = doJogo.Nivel,
                NumeroMaximoDeCheckCombosEmUnicoJogo = doJogo.Cubos,
                NumeroMaximoDeEsferasEmUnicoJogo = doJogo.Esferas,
                NumeroMaximoDeEstaminasEmUnicoJogo = doJogo.Estaminas,
                NumeroMaximoDeMoedasEmUnicoJogo = doJogo.Moedas,
                NumeroMaxInimigosDerrotadosEmunicoJogo = doJogo.Inimigos
            }, recordesHUD
            );
        estado = EstadoDoContador.interpolaPontosAplicados;
        PP.AdicionaJogoHoje();
        PP.JogosSeguidos++;
        PP.TotalDeJogosTerminados++;

        ResultadoDasMissoes.AplicaResultadoDasMissoes();
        ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.VerificaEquipamentosQuebrados();

        jaSalvouOResultado = true;
        ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
        
    }

    void InterpolaDinheiro()
    {
        Perfil P = new Perfil()
        {
            NomeDoPerfil = perfilAtual.NomeDoPerfil,
            Dinheiro = ValorDaInterpolacao(perfilAtual.Dinheiro,tempoDeSomarDadosPrincipais),
            EstrelasDeCristal = ValorDaInterpolacao(perfilAtual.EstrelasDeCristal,tempoDeSomarDadosPrincipais),
            ComboMaximoAlcancado = doJogo.ComboMaximoAlcancado,
            NumeroMaximoDeCheckCombosEmUnicoJogo = doJogo.Cubos,
            NivelMaximoAlcancado = doJogo.Nivel,
            NumeroMaximoDeEsferasEmUnicoJogo = doJogo.Esferas,
            MaiorPontuacao = doJogo.Pontuacao,
            NumeroMaximoDeEstaminasEmUnicoJogo = doJogo.Estaminas,
            NumeroMaximoDeMoedasEmUnicoJogo = doJogo.Moedas,
            NumeroMaxInimigosDerrotadosEmunicoJogo = doJogo.Inimigos
        };

        PainelDoPerfil.t.AtualizaExibicao(P);
    }

    void InterpolaPontosParaPainel()
    {
        Perfil P = new Perfil()
        {
            NomeDoPerfil = perfilAtual.NomeDoPerfil,
            Dinheiro = perfilAtual.Dinheiro,
            EstrelasDeCristal = perfilAtual.EstrelasDeCristal,
            ComboMaximoAlcancado = ValorDaInterpolacao(doJogo.ComboMaximoAlcancado, tempoDeInterpolacao),
            NumeroMaximoDeCheckCombosEmUnicoJogo = ValorDaInterpolacao(doJogo.Cubos, tempoDeInterpolacao),
            NivelMaximoAlcancado = ValorDaInterpolacao(doJogo.Nivel, tempoDeInterpolacao),
            NumeroMaximoDeEsferasEmUnicoJogo = ValorDaInterpolacao(doJogo.Esferas, tempoDeInterpolacao),
            MaiorPontuacao = ValorDaInterpolacao(doJogo.Pontuacao, tempoDeInterpolacao),
            NumeroMaximoDeEstaminasEmUnicoJogo = ValorDaInterpolacao(doJogo.Estaminas, tempoDeInterpolacao),
            NumeroMaximoDeMoedasEmUnicoJogo = ValorDaInterpolacao(doJogo.Moedas, tempoDeInterpolacao),
            NumeroMaxInimigosDerrotadosEmunicoJogo = ValorDaInterpolacao(doJogo.Inimigos,tempoDeInterpolacao)
        };

        PainelDoPerfil.t.AtualizaExibicao(P);
    }

    int ValorDaInterpolacao(float valorAlvo,float qualTempo)
    {
        return (int)Mathf.Lerp(0, valorAlvo, contadorDeTempo / qualTempo);
    }

    void FinalisarMostradorDeResultados()
    {
        if (!ResultadoDasMissoes.MissaoTeveResultado())
        {
            ControladorGlobal.c.Estado = EstadoDoSoftware.retornandoParaPerfilDoTitulo;
            SceneManager.LoadScene("PreJogo");
        }
        else
        {
            SceneManager.LoadScene("recompensas_plus");
        }
    }

    public void BotaoContinuar()
    {
        if (!jaSalvouOResultado)
            SalvarREsultado();

        if (!ResultadoDasMissoes.ExcedeuTentativasDeMissoes())
        {
            FinalisarMostradorDeResultados();
        }
        else
            tentaticasExcedidas.MostrarTentativasExcedidas(gameObject, FinalisarMostradorDeResultados);
    }
}
