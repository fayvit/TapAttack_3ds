using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AtualizadorDosTextosDeNivel : MonoBehaviour
{
    [SerializeField]private Text textoNumeroDeNivel;
    [SerializeField]private Text textoXP;
    [SerializeField]private Image imagemXP;
    [SerializeField]private Text textoDeInfo;
    [SerializeField]private Image imagemDeInfo;

    [SerializeField]private GameObject particulaXP;
    [SerializeField]private GameObject particulaTexto;

    public void AtualizaTextosDeNivel(int i)
    {
        textoNumeroDeNivel.text = "Nivel " + (i).ToString();
        if (RecompensaPorNivel.RecompensaDoNivel(i).textoParaPainel != "")
        {
            textoDeInfo.text = RecompensaPorNivel.RecompensaDoNivel(i).textoParaPainel;
        }
        else
        {
            imagemDeInfo.enabled = false;
            textoDeInfo.enabled = false;
        }

        if (ControladorGlobal.c != null)
        {
            int meuNivel = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.NivelJogador.Nivel;
            if (meuNivel >= i)            
            {
                textoDeInfo.color = new Color(0.34f, 0.93f, 1f);
                textoNumeroDeNivel.color = new Color(1, 0.93f, 0.44f);
            }

            if(meuNivel<=i)
            {
                imagemXP.enabled = false;
            }
            

            textoXP.text = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.NivelJogador.CalculaPassaNivelInicial(i, true).ToString();
        }
    }

    public void CorDoTexto(Color C)
    {
        textoDeInfo.color = C;
    }

    public void ParticulaXP()
    {
        imagemXP.enabled = true;
        GameObject G = Instantiate(particulaXP);
        G.transform.SetParent(imagemXP.transform);
        G.transform.localPosition = Vector3.zero;
    }

    public void ParticulaTexto()
    {
        GameObject G = Instantiate(particulaTexto);
        G.transform.SetParent(textoNumeroDeNivel.transform);
        G.transform.localPosition = Vector3.zero;
        CorDoTexto(Color.yellow);

        Destroy(G, 2);

        if (textoDeInfo.enabled)
        {
            G = Instantiate(particulaTexto);
            G.transform.SetParent(textoDeInfo.transform);
            G.transform.localPosition = Vector3.zero;
            CorDoTexto(Color.yellow);
            Destroy(G, 2);
        }
    }
}
