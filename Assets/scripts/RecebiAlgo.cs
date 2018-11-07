using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecebiAlgo : MonoBehaviour
{
    [Header("Relacionados as particulas")]
    [SerializeField]private GameObject particulaCoisaBoa;
    [SerializeField]private GameObject particulaUpei;

    [Header("Relacionados ao painel")]
    [SerializeField]private Text oQganhei;
    [SerializeField]private Text nomeDoQueGanhei;
    [SerializeField]private Text descricaoDoQueGanhei;
    [SerializeField]private Button btnFinalisar;

    [Header("Relacionados a Imagem da recompensa")]
    [SerializeField]private Image imagemDoQGanhei;
    [SerializeField]private Text txtNivel;
    [SerializeField]private Image imagemDoNivel;
    [SerializeField]private Text txtParaSempre;

    private acaoDesteBotao minhaAcao;

    public delegate void acaoDesteBotao();

    private const float INTERVALO_DE_TEMPO_DE_PARTICULAS = 0.25F;
    private const float DESLOCAMENTO_DAS_PARTICULAS = 100;
    private const float REESCALONAMENTO_DA_PARTICULA = 100;
    private const float TEMPO_DE_DESTOICAO_DA_PARTICULA = 2;

    public enum Estilo
    {
        liberaSlote,
        primeiroMultiplicadorDePontos,
        primeiraRecompensa
    }

    public void ConstroiObjeto(acaoDesteBotao acao, Estilo estilo)
    {
        Debug.Log(estilo);
        gameObject.SetActive(true);
        minhaAcao += acao;
        txtNivel.enabled = false;
        imagemDoNivel.enabled = false;

        switch (estilo)
        {
            case Estilo.liberaSlote: 
                oQganhei.text = "Você liberou seu primeiro slot de equipamentos";
                nomeDoQueGanhei.text = "Slot de Equipamento";
                descricaoDoQueGanhei.text = "Agora você pode utilizar equipamentos para ajudar nas suas batalhas";

                txtParaSempre.text = "Slot";
                

                imagemDoQGanhei.sprite = SpriteDeEquipamento.s.RetornaSprite("cadeado");
            break;

            case Estilo.primeiraRecompensa:

                Debug.Log("oQganhei: "+oQganhei);

                oQganhei.text = "Você recebeu uma recompensa por alcançar um nivel";
                nomeDoQueGanhei.text = "recompensa";
                descricaoDoQueGanhei.text = "Veja sua recompensa clicando no botão recompensas";

                txtParaSempre.text = "recompensa";
                

                imagemDoQGanhei.sprite = SpriteDeEquipamento.s.RetornaSprite("presenteIcone");
            break;
            case Estilo.primeiroMultiplicadorDePontos:

                oQganhei.text = "Você aumentou seu multiplicador de pontos Globais";
                nomeDoQueGanhei.text = "+ pontos";
                descricaoDoQueGanhei.text = "Você recebeu +0.5 no seu multiplicador de pontos globais";

                txtParaSempre.text = "multiplicador";


                imagemDoQGanhei.sprite = SpriteDeEquipamento.s.RetornaSprite("plus");

            break;
        }

        StartCoroutine(ParticulasMaisBotao());
    }

    public void ConstroiObjeto(acaoDesteBotao acao,EquipamentoBase equip)
    {

        Debug.Log(equip);
        gameObject.SetActive(true);
        minhaAcao += acao;

        oQganhei.text = "Você recebeu um Equipamento";
        nomeDoQueGanhei.text = equip.NomeEquipamento;
        
        descricaoDoQueGanhei.text = string.Format(BancoDeTextos.TextosDoIdioma("descricaoEquip" + equip.Tipo),
                                        equip.PercentagemDeMod);

        if (equip.NivelDoEquipamento >= 1)
        {
            txtNivel.text = "Nivel "+equip.NivelDoEquipamento.ToString();
            txtParaSempre.text = "Para Sempre";
        }
        else
        {
            txtParaSempre.text = "Uso Único";
            txtNivel.enabled = false;
            imagemDoNivel.enabled = false;
        }

        imagemDoQGanhei.sprite = SpriteDeEquipamento.s.RetornaSprite(equip.Tipo);

        StartCoroutine(ParticulasMaisBotao());
    }

    void OnDisable()
    {
        btnFinalisar.interactable = false;
    }

    void AdapteParticula(GameObject G,int multiplicador)
    {
        RectTransform rt2 = G.GetComponent<RectTransform>();
        rt2.sizeDelta = new Vector2(REESCALONAMENTO_DA_PARTICULA, REESCALONAMENTO_DA_PARTICULA);
        rt2.anchoredPosition += new Vector2(multiplicador*DESLOCAMENTO_DAS_PARTICULAS, 0);
        Destroy(G, TEMPO_DE_DESTOICAO_DA_PARTICULA);
    }

    IEnumerator ParticulasMaisBotao()
    {
        
        RectTransform rt = oQganhei.GetComponent<RectTransform>();
        GameObject G =  ParentearNaHUD.Parentear(particulaCoisaBoa, rt);
        AdapteParticula(G,-1);

        yield return new WaitForSeconds(INTERVALO_DE_TEMPO_DE_PARTICULAS);
        G = ParentearNaHUD.Parentear(particulaCoisaBoa, rt);
        AdapteParticula(G, 0);

        yield return new WaitForSeconds(INTERVALO_DE_TEMPO_DE_PARTICULAS);
        G = ParentearNaHUD.Parentear(particulaUpei, rt);
        AdapteParticula(G, 1);

        yield return new WaitForSeconds(INTERVALO_DE_TEMPO_DE_PARTICULAS);
        btnFinalisar.interactable = true;
    }

    // Use this for initialization
    void Start()
    {
        
        //ConstroiObjeto(Update, PegueUmEquipamento.UmEquipamento(TiposDeEquipamento.anelMaisMoeda));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BotaoEntendi()
    {
        if (minhaAcao != null)
            minhaAcao();

        minhaAcao = null;
        gameObject.SetActive(false);
    }
}
