using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class CompraDeNovosPersonagens
{
    [SerializeField]private Scrollbar sBar;
    [SerializeField]private Text botaoComprar;
    [SerializeField] private RawImage imgRender;
    [SerializeField] private RawImage imgRender2;

    private Perfil P;
    private Personagem p;
    private int indice = 0;

    // Use this for initialization
    public void Start()
    {
        if(sBar!=null)
            sBar.value = 1;

        P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        p = P.PersonagemAtualSelecionado;
        indice = P.IndiceDoPersonagemSelecionado;
    }

    // Update is called once per frame
    public void Update()
    {
        if (botaoComprar != null)
        {
            botaoComprar.text = string.Format(
                BancoDeTextos.TextosDoIdioma(ChavesDeTexto.botaoComprarPersonagem),
                p.CustoDeDesbloqueio
                );
        }
    }

    public void MudouPersonagem(int indice,Image img)
    {
        this.p = P.MeusPersonagens[indice];
        this.indice = indice;
        imgRender.texture = img.mainTexture;
        imgRender2.texture = img.mainTexture;
    }

    public bool TentarComprar()
    {
        if (P.EstrelasDeCristal >= p.CustoDeDesbloqueio)
        {
            p.Bloqueado = false;
            P.EstrelasDeCristal -= p.CustoDeDesbloqueio;
            P.IndiceDoPersonagemSelecionado = indice;
            ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
            return true;
        }
        else
        {
            Debug.Log("Sem estrelas para comprar");
            return false;
        }
    }
}
