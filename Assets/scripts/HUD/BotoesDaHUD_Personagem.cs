using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class BotoesDaHUD_Personagem
{
    [SerializeField] private Button[] botoesDessaHUD;
    [SerializeField] private GameObject botoesDeComprarPersonagem;


    public void HabilitarCompra()
    {
        HabilitarBtnsDeCompra();
        DesabilitarBtnsPrincipais();
    }

    public void DesabilitarCompra()
    {
        DesabilitarBtnsDeCompra();
        HabilitarBtnsPrincipais();
    }
    void HabilitarBtnsDeCompra()
    {
        botoesDeComprarPersonagem.SetActive(true);
    }
    public void DesabilitarBtnsDeCompra()
    {
        if(botoesDeComprarPersonagem!=null)
            botoesDeComprarPersonagem.SetActive(false);
    }
    public void DesabilitarBtnsPrincipais()
    {
        foreach (Button B in botoesDessaHUD)
            B.interactable = false;
    }

    void HabilitarBtnsPrincipais()
    {
        if(botoesDessaHUD!=null)
            if(botoesDessaHUD[0]!=null)
        foreach (Button B in botoesDessaHUD)
            B.interactable = true;
    }
}
