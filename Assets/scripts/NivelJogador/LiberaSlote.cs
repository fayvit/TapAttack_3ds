using UnityEngine;
using System.Collections;

public class LiberaSlote : RecompensaDeNivel
{
    private EquipamentoBase equip;
    private RecebiAlgo.acaoDesteBotao volta;
    private RecebiAlgo recebido;
    public enum QualSlote
    {
        primeiro,
        segundo,
        terceiro
    }

    private QualSlote qual;
    public LiberaSlote(QualSlote qual)
    {
        this.qual = qual;
    }

    public override void AcaoDaRecompensa()
    {
        Perfil P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        P.Slotes[(int)qual].Desbloqueado = true;

        /*Modificar escolha de equipamento*/
        equip = PegueUmEquipamento.SorteiaEquipamentoDefinitivo();
        P.AdicionaEquipamentoNaLita(equip);
    }

    public override void MostrarAlgo(RecebiAlgo recebido, RecebiAlgo.acaoDesteBotao volta)
    {
        recebido.gameObject.SetActive(true);
        if (qual == QualSlote.primeiro)
        {
            recebido.ConstroiObjeto(MaisUmaCoisa, RecebiAlgo.Estilo.liberaSlote);
            this.recebido = recebido;
            this.volta += volta;
        }
        else
            recebido.ConstroiObjeto(volta, equip);

        tenhoAlgoParaMostrar = false;
    }

    void MaisUmaCoisa()
    {
        GameObject.FindObjectOfType<ControladorDoMostradosDeNiveis>().StartCoroutine(
        MaisUmaCoisaComTempo());
    }

    IEnumerator MaisUmaCoisaComTempo()
    {
        yield return new WaitForSeconds(0.25f);
        recebido.ConstroiObjeto(volta, equip);
    }
}
