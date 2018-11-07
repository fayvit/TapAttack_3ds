using UnityEngine;
using System.Collections;
[System.Serializable]
public class GerenciadorDeCombos
{
    [SerializeField]private float tempoParaFimDoCombo = 5;
    [SerializeField]private float tempoDecorrido = 0;
    [SerializeField]private int contadorDoCombo = 0;
    [SerializeField]private int pontosAcumulados = 0;
    [SerializeField]private int modDoTempoDeCombo = 0;
    [SerializeField]private int modDoGanhoDePontos = 0;
    [SerializeField]private int modDosPontosPorCombo = 0;
    [SerializeField]private float modDoMultiplicadorDePontos = 1;

    public bool dobraTempoDeCombo = false;

    public float PercentagemDeTempoParaFimDoCombo
    {
        get { return contadorDoCombo==0?0: (1-tempoDecorrido / (tempoParaFimDoCombo*(1+(float)modDoTempoDeCombo/100))); }
    }
    public int ContadorDoCombo
    {
        get { return contadorDoCombo; }
    }

    public int PontosPorAdicionar
    {
        get {return (int)((1 + (contadorDoCombo-1)*(0.1f+ ((float)modDosPontosPorCombo / 100))) * pontosAcumulados); }
    }

    public float ModDoMultiplicadorDePontos
    {
        get { return modDoMultiplicadorDePontos; }
        set { modDoMultiplicadorDePontos = value; }
    }

    public void SetaMods()
    {
        Perfil p = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        Personagem P = p.PersonagemAtualSelecionado;
        ModDoMultiplicadorDePontos = p.MultiplicadorGlobalDePontos;
        modDoTempoDeCombo = P.ModDoTempoDeCombo;
        modDosPontosPorCombo = P.ModDosPontosPorCombo;
        modDoGanhoDePontos = P.ModDoGanhoDePontosPorInimigo;

        if (dobraTempoDeCombo)
            modDoTempoDeCombo *= 2;
    }

    public void AdicionaCombo(int pontos,int quanto = 1)
    {
        pontosAcumulados += (int)((1+(float)modDoGanhoDePontos/100)*pontos);   
        tempoDecorrido = 0;
        contadorDoCombo += quanto;
        ControladorGlobal.c.EmJogo.ComboMaximoAlcancado 
            = Mathf.Max(contadorDoCombo, ControladorGlobal.c.EmJogo.ComboMaximoAlcancado);
    }

    public void ZerarCombo()
    {
        ControladorDeJogo.c.G_Pontos.AdicionaPontos((int)(PontosPorAdicionar*modDoMultiplicadorDePontos));
        contadorDoCombo = 0;
        pontosAcumulados = 0;
                
    }

    // Update is called once per frame
    public void Update()
    {
        tempoDecorrido += Time.deltaTime;
        if (tempoDecorrido > (tempoParaFimDoCombo*(1+(float)modDoTempoDeCombo/100)))
        {
            ZerarCombo();
        }
    }
}
