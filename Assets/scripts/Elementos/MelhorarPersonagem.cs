using UnityEngine;
using System.Collections;

public class MelhorarPersonagem
{
    public static string TextoDeMelhora(Personagem P)
    {
        string retorno= "";
        if (P.NivelDaHabilidade % 5 != 0)
        {
            retorno = string.Format(
                BancoDeTextos.TextosDoIdioma(ChavesDeTexto.MelhorarComDim),
                P.ValorCorrenteDaHabilidade,
                P.ProximoValorParaHabilidade,
                P.CustoCorrenteDaHabilidade);
        }
        else {
            retorno = string.Format(
                            BancoDeTextos.TextosDoIdioma(ChavesDeTexto.MelhorarComEstrela),
                            P.ValorCorrenteDaHabilidade,
                            P.ProximoValorParaHabilidade,
                            P.NivelDaHabilidade/5*16);
        }
        return retorno;
    }
    public static void Melhorar(PainelUmaMensagem p, BotoesDaHUD_Personagem btns, PainelUmaMensagem.RetornarParaAntecessor r)
    {
        Perfil perfil = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        Personagem P = perfil.PersonagemAtualSelecionado;
        if (P.NivelDaHabilidade % 5 != 0)
        {
            if (P.CustoCorrenteDaHabilidade <= perfil.Dinheiro)
            {
                perfil.Dinheiro -= P.CustoCorrenteDaHabilidade;
                RenovaValorECusto(P);
            }
            else
            {
                btns.DesabilitarBtnsPrincipais();
                p.ConstroiPainelUmaMensagem(r, "Você ainda não tem as moedas necessárias");
                
            }
        }
        else
        {
            if (P.NivelDaHabilidade/5*16 <= perfil.EstrelasDeCristal)
            {
                perfil.EstrelasDeCristal -= P.NivelDaHabilidade / 5 * 16;
                RenovaValorECusto(P);
                ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
            }
            else
            {
                btns.DesabilitarBtnsPrincipais();
                p.ConstroiPainelUmaMensagem(r, "Sem estrelas ´para melhorar");
                Debug.Log("Sem estrelas ´para melhorar");
            }
        }
    }

    static void RenovaValorECusto(Personagem P)
    {
        P.ValorCorrenteDaHabilidade = (int)(Mathf.Pow(P.TaxaDaEvolucaoDaHabilidade, P.NivelDaHabilidade) * P.ValorInicialDaHabilidade);
        P.CustoCorrenteDaHabilidade = (int)(Mathf.Pow(P.TaxaDaEvolucaoDoCustoDaHabilidade, P.NivelDaHabilidade) * P.CustoInicialDaHabilidade);
        P.NivelDaHabilidade++;
        ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
    }
}
