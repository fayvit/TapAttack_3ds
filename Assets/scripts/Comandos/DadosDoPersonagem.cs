using UnityEngine;
using System.Collections;

[System.Serializable]
public class DadosDoPersonagem
{
    [SerializeField] private int nivelInicial = 1;
    [SerializeField] private int vidaCorrente = 14;
    [SerializeField] private int vidaMax = 14;
    [SerializeField] private int resistencia = 3;
    [SerializeField] private int estaminaCorrente = 3;
    [SerializeField] private int estaminaMax = 3;
    [SerializeField] private int dinheiro = 0;
    [SerializeField] private int cristaisEspeciais = 0;
    [SerializeField] private int cristaisParaAtivar = 50;
    [SerializeField] private int forca = 1;
    [SerializeField] private int poder = 3;
    [SerializeField] private GerenciadorDeExperiencia mNivel;// = new GerenciadorDeExperiencia();
    [SerializeField] private float tempoDeRegeneracao = 1.5f;
    [SerializeField] private float tempoRegenerando = 0;

    private bool dobreVida = false;
    private bool dobreForca = false;

    private bool umPontoDeVida = false;

    public bool DobreForca
    {
        set { dobreForca = value; }
    }


    public bool DobreVida
    {
        get { return dobreVida; }
        set { dobreVida = value; }
    }
    public int Dinheiro
    {
        get { return dinheiro; }
    }

    public int CristaisEspeciais
    {
        get { return cristaisEspeciais; }
    }

    public int CristaisParaAtivar
    {
        set { cristaisParaAtivar = value; }
        get { return cristaisParaAtivar; }
    }

    public IGerenciadorDeExperiencia G_XP
    {
        get { return mNivel; }
    }

    public int NivelParaMostrador
    {
        get { return mNivel.Nivel - NivelInicial + 1; }
    }

    public int Forca
    {
        set { forca = value; }
        get{ return forca; }
    }

    public int Poder
    {
        get { return poder; }
        set { poder = value; }
    }

    public int EstaminaMax
    {
        get { return estaminaMax; }
    }

    public int EstaminaCorrente
    {
        get { return estaminaCorrente; }
    }

    public int VidaCorrente
    {
        set { vidaCorrente = value; }
        get { return vidaCorrente; }
    }

    public int VidaMax
    {
        get { return vidaMax; }
        set { vidaMax = value; }
    }

    public int NivelInicial
    {
        get { return nivelInicial; }
    }

    public bool UmPontoDeVida
    {
        get { return umPontoDeVida; }
        set { umPontoDeVida = value; }
    }

    public void UtilizarEstamina(int gasto)
    {
        estaminaCorrente-=gasto;
        tempoRegenerando = 0;
    }

    public void AdicionaEstamina(int valor)
    {
        estaminaCorrente = Mathf.Min(estaminaCorrente+valor,estaminaMax);
    }

    public void ZeraCristais()
    {
        cristaisEspeciais = 0;
    }

    public void AdicionaDinheiro(int tanto)
    {
        dinheiro += tanto;
    }

    public void AdicionaCristais(int tanto)
    {
        if (cristaisEspeciais + tanto <= cristaisParaAtivar)
            cristaisEspeciais += tanto;
        else
        {
            cristaisEspeciais = cristaisParaAtivar;
            // Ativa Especial
        }
    }

    /// <summary>
    /// Função responsável por aumentar o XP e verificar o passa nivel
    /// </summary>
    /// <param name="XP"> numero pontos de experiencia obtido</param>
    public void AplicaXP(int XP)
    {
        mNivel.XP += XP;
        if (mNivel.VerificaPassaNivel())
        {
            mNivel.AplicaPassaNivel();
            MaisNivel();
        }
    }

    void MaisNivel()
    {
        resistencia++;
        estaminaMax++;
        forca++;
        poder++;

        vidaMax = 4 * resistencia + 2;
        estaminaCorrente = estaminaMax;
        vidaCorrente = Mathf.Min(vidaMax,vidaCorrente+4);
       // vidaCorrente = 1;
    }

    public float EstaminaPeloTempo()
    {
        if (estaminaCorrente < estaminaMax)
        {
            return tempoRegenerando / tempoDeRegeneracao;
        }
        else
            return 0;
    }

    // Use this for initialization
    public void Start()
    {
        if (NivelInicial > 1)
            AplicaNivelInicial();
    }

    void AplicaNivelInicial()
    {
        for (int i = 1; i < NivelInicial; i++)
            MaisNivel();

        if (dobreVida)
        {
            VidaMax *= 2;
            VidaCorrente *= 2;
        }

        if (dobreForca)
            forca *= 2;

        mNivel.ParaProxNivel = mNivel.CalculaPassaNivelInicial(NivelInicial);
        mNivel.Nivel = NivelInicial;

        if (umPontoDeVida)
            VidaCorrente = 1;
    }

    // Update is called once per frame

    public void RegeneracaoDeEstamina()
    {
        tempoRegenerando += Time.deltaTime;
        if (tempoRegenerando > tempoDeRegeneracao)
        {
            estaminaCorrente = Mathf.Min(estaminaCorrente + 1, estaminaMax);
            tempoRegenerando = 0;
        }
    }

    public void AplicarDano(int dano)
    {
        if (vidaCorrente - dano > 0)
            vidaCorrente -=dano;
        else
        {
            vidaCorrente = 0;
        }

        

        //Procedimento de dano ou morte
    }
}
