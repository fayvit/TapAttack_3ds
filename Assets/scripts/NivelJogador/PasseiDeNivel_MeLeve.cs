using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PasseiDeNivel_MeLeve : MonoBehaviour
{
    public int nivelDeSaida = 0;
    public int nivelAlvo = 0;
    private float contadorDeTempo = 0;
    private bool invocou = false;
    private bool estouMostrandoAlgo = false;
    public RecebiAlgo recebido;
    private FasesDoPassaNivel fase;

    private const float TEMPO_PARA_INICIAR = 0.5F;
    private const float TEMPO_MAX_PARA_MOSTRAR_NIVEIS = 1.5F;
    private const float TEMPO_DE_INTERVALO_ENTRE_MOSTRA_NIVEIS = 0.25F;
    private enum FasesDoPassaNivel
    {
        customizarMostrador,
        mostrarNiveisGanhos,
        mostrarOsAlgoMais,
        meDestroir
    }
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switch (fase)
        {
            case FasesDoPassaNivel.customizarMostrador:
                
                if (SceneManager.GetActiveScene().name == "niveis_plus" || SceneManager.GetActiveScene().name == "niveis")
                {
                    ModificadorDoContainerPrincipal.DesligarBotoes(FindObjectOfType<Canvas>().gameObject);
                    CustomizeMostradorDeNiveis();
                }
            break;
            case FasesDoPassaNivel.mostrarNiveisGanhos:
                contadorDeTempo += Time.deltaTime;
                if (contadorDeTempo > TEMPO_PARA_INICIAR && !invocou)
                {
                    for (int i = nivelDeSaida; i < nivelAlvo; i++)
                    {
                        if ((nivelAlvo - nivelDeSaida) * TEMPO_DE_INTERVALO_ENTRE_MOSTRA_NIVEIS < TEMPO_MAX_PARA_MOSTRAR_NIVEIS)
                        {
                            StartCoroutine(MostrarNivelGanho(i, (i - nivelDeSaida) * TEMPO_DE_INTERVALO_ENTRE_MOSTRA_NIVEIS));
                            //Invoke("MostrarNivelGanho", i * TEMPO_DE_INTERVALO_ENTRE_MOSTRA_NIVEIS);
                        }
                        else
                        {
                            StartCoroutine(MostrarNivelGanho(i, (TEMPO_MAX_PARA_MOSTRAR_NIVEIS / (nivelAlvo - nivelDeSaida))));
                        }
                        RecompensaPorNivel.RecompensaDoNivel(i+1).AcaoDaRecompensa();
                        ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
                    }

                    invocou = true;
                }

                if (contadorDeTempo > TEMPO_MAX_PARA_MOSTRAR_NIVEIS)
                {
                    bool continua = true;
                    for (int i = nivelDeSaida; i < nivelAlvo; i++)
                    {
                        continua &= !RecompensaPorNivel.RecompensaDoNivel(i + 1).tenhoAlgoParaMostrar;
                    }

                    if (continua)
                        fase = FasesDoPassaNivel.meDestroir;
                    else
                        fase = FasesDoPassaNivel.mostrarOsAlgoMais;
                }
            break;
            case FasesDoPassaNivel.mostrarOsAlgoMais:
                bool seguir = true;
                for (int i = nivelDeSaida; i < nivelAlvo; i++)
                {
                    if (RecompensaPorNivel.RecompensaDoNivel(i + 1).tenhoAlgoParaMostrar && !estouMostrandoAlgo)
                    {
                        seguir = false;
                        estouMostrandoAlgo = true;
                        RecompensaPorNivel.RecompensaDoNivel(i + 1).MostrarAlgo(recebido, VoltarDoMostrador);
                        enabled = false;
                    }
                }

                if (seguir)
                    fase = FasesDoPassaNivel.meDestroir;
            break;
            case FasesDoPassaNivel.meDestroir:
                ModificadorDoContainerPrincipal.ReligarBotoes(FindObjectOfType<Canvas>().gameObject);
                Destroy(gameObject);
            break;
        }
        
    }

    void VoltarDoMostrador()
    {
        estouMostrandoAlgo = false;
        enabled = true;
    }

    IEnumerator MostrarNivelGanho(int nivel,float tempo)
    {
        yield return new WaitForSeconds(tempo);
        ControladorDoMostradosDeNiveis contN = FindObjectOfType<ControladorDoMostradosDeNiveis>();
        contN.ParticulaDaBarraDeXP(nivel-1);
        yield return new WaitForSeconds(0.25f);
        contN.ParticulaDoTextoDeNivel(nivel);

        
    }

    

    void CustomizeMostradorDeNiveis()
    {
        for (int i = nivelDeSaida; i <= nivelAlvo; i++)
        {
            FindObjectOfType<ControladorDoMostradosDeNiveis>().NiveisParaGanharAgora(nivelDeSaida,nivelAlvo);
        }

        fase = FasesDoPassaNivel.mostrarNiveisGanhos;
    }
}
