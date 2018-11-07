using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class GerenciadorDoContainerDasMissoes
{
    [SerializeField]private Text tvd;
    [SerializeField]private Text tvm;

    private AnaliseDeVisibilidade analiseVerde;
    private AnaliseDeVisibilidade analiseVermelho;

    void SetarAnalises()
    {
        analiseVerde = new AnaliseDeVisibilidade(tvd.transform.parent.gameObject);
        analiseVermelho = new AnaliseDeVisibilidade(tvm.transform.parent.gameObject);

    }
    public void AtualizaMisoes()
    {
        if (tvd != null)
        {
            if (analiseVerde == null)
            SetarAnalises();

            AtualizeTextos(tvd,1);
            AtualizeTextos(tvm, 0);

       
            analiseVerde.Mostre();
            analiseVermelho.Mostre();
        }

        /*
        analiseVerde.VerifiqueMostraTexto();
        analiseVermelho.VerifiqueMostraTexto();
        */
    }

    public static void AtualizeTextos(Text tvd,int i)
    {
    Missoes[] minhasMissoes = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.GMissoes.MissoesAtuais;
        if (minhasMissoes != null)
        {
            if (minhasMissoes.Length > 0)
            {
                if (minhasMissoes[i].MostraSoma(ControladorGlobal.c.EmJogo) < minhasMissoes[i].Meta)
                    tvd.text = minhasMissoes[i].MostraSoma(ControladorGlobal.c.EmJogo) + "/" + minhasMissoes[i].Meta;
                else
                    tvd.text = "Concluído";
            }
            
        }
    }

    [System.Serializable]
    private class AnaliseDeVisibilidade
    {
        private string textoGuardado;
        private float tempoMostrando = 0;

        private const float TEMPO_PARA_ESCONDER = 2;

        private Text oTexto;
        private GameObject pai;
        public AnaliseDeVisibilidade(GameObject pai)
        {
            this.pai = pai;
            oTexto = pai.GetComponentInChildren<Text>();
        }

        public void VerifiqueMostraTexto()
        {
            string textoAnterior = oTexto.text;
            tempoMostrando += Time.deltaTime;
            if (textoAnterior != textoGuardado)
            {
                tempoMostrando = 0;
                textoGuardado = textoAnterior;
            }

            if (tempoMostrando < TEMPO_PARA_ESCONDER)
                Mostre();
            else
                Esconde();
        }

        public void Esconde()
        {
            if (pai.activeSelf)
            {
                pai.SetActive(false);
            }
        }

        public void Mostre()
        {
            if (!pai.activeSelf)
            {
                pai.SetActive(true);
            }
        }
    }
}

