using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeDigaMinhaMissao : MonoBehaviour
{
    [SerializeField]private Text textoDaMissaoVerde;
    [SerializeField]private Text textoDaMissaoVermelha;
    [SerializeField]private Text contadorDaMissaoVerde;
    [SerializeField]private Text contadorDaMissaoVermelha;
    [SerializeField] private bool esconder = true;

    private bool iniciou;    
    private float tempoDecorrido = 0;
    private const float TEMPO_PARA_MESAGEM_DESAPARECER = 6;

    void OnEnable()
    {
        if (contadorDaMissaoVerde != null)
        {
            GerenciadorDoContainerDasMissoes.AtualizeTextos(contadorDaMissaoVerde, 0);
            GerenciadorDoContainerDasMissoes.AtualizeTextos(contadorDaMissaoVermelha, 1);
        }
    }
    public static void TextosDeMissao(Text Tvd,Text Tvm,Missoes[] Ms)
    {
        Tvd.text = string.Format(BancoDeTextos.TextosDoIdioma(
                    (ChavesDeTexto)System.Enum.Parse(typeof(ChavesDeTexto), "indicativoDaMissao" + Ms[0].Tipo.ToString())),
                    Ms[0].Meta.ToString()
                    );
        Tvm.text = string.Format(BancoDeTextos.TextosDoIdioma(
            (ChavesDeTexto)System.Enum.Parse(typeof(ChavesDeTexto), "indicativoDaMissao" + Ms[1].Tipo.ToString())),
            Ms[1].Meta.ToString()
            );
    }
    // Use this for initialization
    void Start()
    {
        if (ControladorGlobal.c != null)
        {
            Missoes[] Ms = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.GMissoes.MissoesAtuais;
            
            if (Ms != null)
                if(Ms.Length>0)
                {
                    
                    TextosDeMissao(textoDaMissaoVermelha, textoDaMissaoVerde, Ms);
                    iniciou = true;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (esconder)
        {
            tempoDecorrido += Time.deltaTime;
            if (tempoDecorrido > TEMPO_PARA_MESAGEM_DESAPARECER)
                Destroy(gameObject);
        }

        if (!iniciou)
            Start();
    }
}
