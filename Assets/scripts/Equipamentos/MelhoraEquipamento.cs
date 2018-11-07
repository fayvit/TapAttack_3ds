using UnityEngine;
using System.Collections;

public class MelhoraEquipamento
{
    public static string TextoDeMelhora(EquipamentoBase P)
    {
        string retorno = "";
        if (P.NivelDoEquipamento % 5 != 0)
        {
            retorno = string.Format(
                BancoDeTextos.TextosDoIdioma(ChavesDeTexto.MelhorarEquipComDim),
                P.PercentagemDeMod,
                P.ProximoValorDeModificacao,
                P.CustoParaNivel);
        }
        else
        {
            retorno = string.Format(
                BancoDeTextos.TextosDoIdioma(ChavesDeTexto.MelhorarEquipComEstrelas),
                P.PercentagemDeMod,
                P.ProximoValorDeModificacao,
                P.NivelDoEquipamento/5*2);
        }
        return retorno;
    }

    public static bool Melhorar(EquipamentoBase equip,PainelUmaMensagem m,GameObject paiDosDesligaveis,PainelUmaMensagem.RetornarParaAntecessor r)
    {
        bool melhorou = false;
        Perfil P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        if (equip.NivelDoEquipamento % 5 != 0)
        {
            if (equip.CustoParaNivel <= P.Dinheiro)
            {
                P.Dinheiro -= equip.CustoParaNivel;
                RenovaValorECusto(equip);
                melhorou = true;
            }
            else
            {
                ModificadorDoContainerPrincipal.DesligarBotoes(paiDosDesligaveis);
                m.ConstroiPainelUmaMensagem(r, "Você ainda não tem as moedas necessárias");
                melhorou = false;
            }
        }
        else
        {
            if (equip.NivelDoEquipamento / 5 * 2 <= P.EstrelasDeCristal)
            {
                P.EstrelasDeCristal -= equip.NivelDoEquipamento / 5 * 2;
                RenovaValorECusto(equip);
                melhorou = true;
            }
            else
            {
                ModificadorDoContainerPrincipal.DesligarBotoes(paiDosDesligaveis);
                m.ConstroiPainelUmaMensagem(r, "Você ainda não tem as estrelas necessárias");
                melhorou = false;
            }
        }

        return melhorou;
    }

    static void RenovaValorECusto(EquipamentoBase equip)
    {
        equip.TaxaDeModificacaoDoEquipamento *= (1 + equip.TaxaDeEvolucaoDaModificacao);
        equip.NivelDoEquipamento++;
        ControladorGlobal.c.DadosGlobais.SalvarSeNaoForTesteDeCena();
    }

    /*
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
            if (P.NivelDaHabilidade / 5 * 16 <= perfil.EstrelasDeCristal)
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
    */
}
