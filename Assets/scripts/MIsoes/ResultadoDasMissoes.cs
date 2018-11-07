using UnityEngine;
using System.Collections;

public class ResultadoDasMissoes
{
    public static bool ExcedeuTentativasDeMissoes()
    {
        Perfil P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        GerenciadorDeMissoes gMissoes = P.GMissoes;
        Missoes[] Ms = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.GMissoes.MissoesAtuais;
        for (int i = 0; i < Ms.Length; i++)
        {
            if ((Ms[i].Tentativas == 20 || (Ms[i].Tentativas>20 && Ms[i].Tentativas % 10 == 0)) && !Ms[i].AlcancouAMeta())
            {
                return true;
            }
        }

        return false;
    }
    public static void AplicaResultadoDasMissoes()
    {
        Perfil P = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado;
        GerenciadorDeMissoes gMissoes = P.GMissoes;
        Missoes[] Ms = gMissoes.MissoesAtuais;
        for (int i = 0; i < Ms.Length; i++)
        {
            Ms[i].SomaAlcancado(ControladorGlobal.c.EmJogo);

            if (Ms[i].AlcancouAMeta())
            {
                P.Recompensas.Add(
                    new RecompensaPorMissao(
                        Ms[i].Tipo,
                        gMissoes.LevelDeEscolhaDeMissao(Ms[i].Tipo),
                        Ms[i].Meta
                        ));
                Ms[i].Tentativas = 0;
            }
            else
                Ms[i].Tentativas++;
        }
    }

    public static bool MissaoTeveResultado(int indiceDaMissao = -1)
    {
        Missoes[] Ms = ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.GMissoes.MissoesAtuais;
        if (indiceDaMissao < -1 || indiceDaMissao > Ms.Length-1)
        {
            Debug.Log("indice de missão fora do raio de duas missões");
            return false;
        }
        else if (indiceDaMissao > -1 && indiceDaMissao <Ms.Length-1)
        {
            
            if (Ms[indiceDaMissao].AlcancouAMeta())
                return true;
            else return false;
        }
        else
        {
            for (int i = 0; i < Ms.Length; i++)
            {
                if (Ms[i].AlcancouAMeta())
                    return true;
            }
            return false;
        }
    }
}
