using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Perfil
{
    [Header("dados de perfil")]
    #region variaveis com dados de perfil
    [SerializeField]private string nomeDoPerfil = "meu nominho";
    [SerializeField]private int dinheiro = 0;
    [SerializeField]private int estrelasDeCristal = 0;
    [SerializeField]private int maiorPontuacao = 0;
    [SerializeField]private int comboMaximoAlcancado = 0;
    [SerializeField]private int numeroMaximoDeEsferasEmUnicoJogo = 0;
    [SerializeField]private int numeroMaximoDeCheckCombosEmUnicoJogo = 0;
    [SerializeField]private int numeroMaximoDeMoedasEmUnicoJogo = 0;
    [SerializeField]private int numeroMaximoDeEstaminasEmUnicoJogo = 0;
    [SerializeField]private int numeroMaxInimigosDerrotadosEmunicoJogo = 0;
    [SerializeField]private int nivelMaximoAlcancado = 0;
    [SerializeField]private float multiplicadorDePontos = 1;
//    [SerializeField]private float modMultiplicadorDePontos = 0; seria utilizado para um impulso poderoso
    private IGerenciadorDeExperiencia nivelJogador = new GerenciadorDeExperiencia() { ModNIvel = 1.11f};
    #endregion

    [Header("fidelidade do jogador")]
    #region variaveis relacionadas a fidelidade do jogador
    [SerializeField]private int jogosSeguidos = 0;
    [SerializeField]private int totalDeJogosTerminados = 0;

    private Dictionary<string, int> estrelasPorDia = new Dictionary<string, int>();
    private Dictionary<string, int> jogosHoje = new Dictionary<string, int>();    
    #endregion

    [Header("personagens")]
    #region variaveis de personagens
    [SerializeField]private int indiceDoPersonagemSelecionado = 0;
    [SerializeField]private Personagem[] personagens = ListaDePersonagens.Lista();
    #endregion

    #region variaveis de Equipamento
    [Header("equipamentos")]
    [SerializeField]private List<EquipamentoBase> meusEquipamentos = new List<EquipamentoBase>();
    [SerializeField]private SloteDeEquipamento[] slotes;
    #endregion

    [Header("missões")]
    [SerializeField]private GerenciadorDeMissoes gMissoes;
    [SerializeField]private List<Recompensa> recompensas = new List<Recompensa>();

    public Perfil()
    {
        SetarSlotes();
    }

    void SetarSlotes()
    {
        slotes = new SloteDeEquipamento[3]
        {
             new SloteDeEquipamento() { Desbloqueado = false,Preenchido = false,EquipamentoNoSlote = new AnelMaisMoedas()} ,
             new SloteDeEquipamento() { Desbloqueado = false,Preenchido = false} ,
             new SloteDeEquipamento() { Desbloqueado = false,Preenchido = false}
        };
    }

    public SloteDeEquipamento[] Slotes
    {
        get {
            if (slotes == null)
            {
                SetarSlotes();
                Debug.LogError("Os slotes nulos foram setados mas não salvos");
            }
            return slotes;
        }
    }
    public Personagem[] MeusPersonagens
    {
        get { return personagens; }
    }

    public Personagem PersonagemAtualSelecionado
    {
        get { return personagens[indiceDoPersonagemSelecionado]; }
    }

    public int IndiceDoPersonagemSelecionado
    {
        get { return indiceDoPersonagemSelecionado; }
        set { indiceDoPersonagemSelecionado = Mathf.Clamp(value,0, personagens.Length-1); }
    }
    public string NomeDoPerfil
    {
        get { return nomeDoPerfil; }
        set { nomeDoPerfil = value; }
    }

    public int Dinheiro
    {
        get { return dinheiro; }
        set { dinheiro = value; }
    }

    public int EstrelasDeCristal
    {
        get { return estrelasDeCristal; }
        set { estrelasDeCristal = value; }
    }

    public int MaiorPontuacao
    {
        get { return maiorPontuacao; }
        set { maiorPontuacao = value; }
    }

    public int ComboMaximoAlcancado
    {
        get { return comboMaximoAlcancado; }
        set { comboMaximoAlcancado = value; }
    }

    public int NumeroMaximoDeEsferasEmUnicoJogo
    {
        get { return numeroMaximoDeEsferasEmUnicoJogo; }
        set { numeroMaximoDeEsferasEmUnicoJogo = value; }
    }

    public int NumeroMaximoDeCheckCombosEmUnicoJogo
    {
        get { return numeroMaximoDeCheckCombosEmUnicoJogo; }
        set { numeroMaximoDeCheckCombosEmUnicoJogo = value; }
    }

    public int NumeroMaximoDeMoedasEmUnicoJogo
    {
        get { return numeroMaximoDeMoedasEmUnicoJogo; }
        set { numeroMaximoDeMoedasEmUnicoJogo = value; }
    }

    public int NumeroMaximoDeEstaminasEmUnicoJogo
    {
        get { return numeroMaximoDeEstaminasEmUnicoJogo; }
        set { numeroMaximoDeEstaminasEmUnicoJogo = value; }
    }

    public int NivelMaximoAlcancado
    {
        get { return nivelMaximoAlcancado; }
        set { nivelMaximoAlcancado = value; }
    }

    public Dictionary<string, int> EstrelasPorDia
    {
        get { return estrelasPorDia; }
        set { estrelasPorDia = value; }
    }

    public int NumeroMaxInimigosDerrotadosEmunicoJogo
    {
        get { return numeroMaxInimigosDerrotadosEmunicoJogo; }
        set { numeroMaxInimigosDerrotadosEmunicoJogo = value; }
    }    

    public Dictionary<PropPerfil, int> PropriedadesDePerfil
    {
        get {
            return new Dictionary<PropPerfil, int>()
            {
                { PropPerfil.pontos,maiorPontuacao },
                { PropPerfil.nivel,nivelMaximoAlcancado },
                { PropPerfil.combo,comboMaximoAlcancado},
                { PropPerfil.moedas,numeroMaximoDeMoedasEmUnicoJogo},
                { PropPerfil.checkCombo,numeroMaximoDeCheckCombosEmUnicoJogo},
                { PropPerfil.esfera,numeroMaximoDeEsferasEmUnicoJogo},
                { PropPerfil.estamina,numeroMaximoDeEstaminasEmUnicoJogo},
                { PropPerfil.inimigos,numeroMaxInimigosDerrotadosEmunicoJogo}
            };
        }

        set {
            maiorPontuacao = value[PropPerfil.pontos];
            nivelMaximoAlcancado = value[PropPerfil.nivel];
            comboMaximoAlcancado = value[PropPerfil.combo];
            numeroMaximoDeMoedasEmUnicoJogo = value[PropPerfil.moedas];
            numeroMaximoDeCheckCombosEmUnicoJogo = value[PropPerfil.checkCombo];
            numeroMaximoDeEsferasEmUnicoJogo = value[PropPerfil.esfera];
            numeroMaximoDeEstaminasEmUnicoJogo = value[PropPerfil.estamina];
            numeroMaxInimigosDerrotadosEmunicoJogo = value[PropPerfil.inimigos];
        }
    }

    public GerenciadorDeMissoes GMissoes
    {
        get { if (gMissoes == null)
                gMissoes = new GerenciadorDeMissoes();
            return gMissoes; }
    }

    public int JogosSeguidos
    {
        get { return jogosSeguidos; }
        set { jogosSeguidos = value; }
    }

    public int TotalDeJogosTerminados
    {
        get { return totalDeJogosTerminados; }
        set { totalDeJogosTerminados = value; }
    }

    public List<Recompensa> Recompensas
    {
        get { return recompensas; }
        set { recompensas = value; }
    }

    public List<EquipamentoBase> MeusEquipamentos
    {
        get { return meusEquipamentos; }
    }

    public IGerenciadorDeExperiencia NivelJogador
    {
        get { return nivelJogador; }
        set { nivelJogador = value; }
    }

    public float MultiplicadorGlobalDePontos
    {
        get {
            if (multiplicadorDePontos == 0)
                multiplicadorDePontos = 1;
            return multiplicadorDePontos; }
    }

    public void VerificaEquipamentosQuebrados()
    {
        for (int i = 0; i < slotes.Length; i++)
        {
            if(slotes[i].EquipamentoNoSlote!=null)
                if (slotes[i].EquipamentoNoSlote.NivelDoEquipamento <= 0)
                {
                    MeusEquipamentos.Remove(slotes[i].EquipamentoNoSlote);
                    slotes[i].RemoveEquipamento();
                }
        }
    }

    public void RemoveUmEquipamento(int numSlote)
    {
        MeusEquipamentos.Remove(slotes[numSlote].EquipamentoNoSlote);
        slotes[numSlote].RemoveEquipamento();
    }

    public void AdicionaJogoHoje(int quantidade = 1)
    {
        string hoje = System.DateTime.Now.ToString("dd/MM/yyyy");
        if (jogosHoje.ContainsKey(hoje))
            jogosHoje[hoje] += quantidade;
        else
            jogosHoje[hoje] = quantidade;
    }

    public int numeroDeJogosHoje()
    {
        string hoje = System.DateTime.Now.ToString("dd/MM/yyyy");
        if (jogosHoje.ContainsKey(hoje))
            return jogosHoje[hoje];
        else
            return 0;
    }

    public void AtualizaPerfil(Perfil P, ControladorDoResultado.ContainerDaHUD_Recordes recordesHUD)
    {
        dinheiro = P.Dinheiro;

        VerificaNovosRecordes(P, recordesHUD);
    }

    public void VerificaNovosRecordes(Perfil P, ControladorDoResultado.ContainerDaHUD_Recordes recordesHUD)
    {

        Dictionary<PropPerfil, int> temp;
        foreach (PropPerfil prop in PropriedadesDePerfil.Keys)
        {
            if (PropriedadesDePerfil[prop] < P.PropriedadesDePerfil[prop])
            {
                if (PropriedadesDePerfil[prop] != 0)
                {
                    recordesHUD.GameObjectRecords[prop].SetActive(true);
                    GameObject G = MonoBehaviour.Instantiate(recordesHUD.particulaDeCoisasBoas);
                    G.transform.SetParent(recordesHUD.GameObjectRecords[prop].transform);
                    G.transform.localPosition = Vector3.zero;
                    MonoBehaviour.Destroy(G, 1.6f);
                }

                temp = PropriedadesDePerfil;
                temp[prop] = P.PropriedadesDePerfil[prop];
                PropriedadesDePerfil = temp;
            }
        }
    }

    public void AplicarEfeitosDeEquipamento()
    {
        for (int i = 0; i < Slotes.Length; i++)
        {
            if (Slotes[i].Preenchido)
                Slotes[i].EquipamentoNoSlote.EfeitoDoEquipamento();
        }
    }

    public void AdicionaEquipamentoNaLita(EquipamentoBase equip)
    {
        if (meusEquipamentos == null)
            meusEquipamentos = new List<EquipamentoBase>();
        meusEquipamentos.Add(equip);
    }

    public void SomaMultiplicadorGlobal(float quanto)
    {
        multiplicadorDePontos += quanto;
        Debug.Log(multiplicadorDePontos);
    }
}

public enum PropPerfil
{
    pontos,
    nivel,
    combo,
    moedas,
    checkCombo,
    esfera,
    estamina,
    inimigos
}
