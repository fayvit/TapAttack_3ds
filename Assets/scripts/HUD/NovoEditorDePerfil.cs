using UnityEngine;
using System.Collections;

public class NovoEditorDePerfil : EditarPerfil_Base
{
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override int IndiceDoPerfilSelecionado()
    {
        return dados.IndiceDoPerfilSelecionado;
    }

    protected override void AtualizacoesEspecificasDaTrocaDeNome(int esse)
    {
        FindObjectOfType<NovoPerfil>().TemPerfilInicializado();
    }

    protected override void AlteracoesEspecificasAoDeletar()
    {
        if (dados.Perfis.Count > 0)
            FindObjectOfType<NovoPerfil>().TemPerfilInicializado();
        else
        {

        }
        BotaoSair();
    }

    public void BotaoExcluir()
    {
        BotaoDeletar();
    }

    public void BotaoSair()
    {
        FindObjectOfType<NovoPerfil>().ReligarComponentesEspecificos();
        gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        if (!painelMensagemConfirmacao.gameObject.activeSelf &&!painelUmaMensagem.gameObject.activeSelf)
        {
            int w = 320;
            int h = 240;

            if (GUI.Button(new Rect(0.06f * w, 0.1f * h, 0.22f * w, 0.1f * h), "Sair", ((GUISkin)Resources.Load("meuSkin")).button))
            {
                BotaoSair();
            }

            if (GUI.Button(new Rect(0.5f * w, 0.62f * h, 0.32f * w, 0.15f * h), "Criar", ((GUISkin)Resources.Load("meuSkin 1")).button))
            {
                BotaoAlterarPerfil();
            }

            if (GUI.Button(new Rect(0.06f * w, 0.62f * h, 0.32f * w, 0.15f * h), "Excluir", ((GUISkin)Resources.Load("meuSkin 2")).button))
            {
                BotaoExcluir();
            }

            input.text = GUI.TextField(new Rect(0.1f * w, 0.43f * h, 0.7f * w, 0.1f * h), "", ((GUISkin)Resources.Load("meuSkin")).textField);
            N3dsKeyboardResult result = UnityEngine.N3DS.Keyboard.GetResult();
            input.text = result.ToString();
        }
    }
}
