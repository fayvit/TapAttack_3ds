using UnityEngine;
using System.Collections.Generic;

public class RecompensaPorNivel
{
    public static List<RecompensaDeNivel> listaDeRecompensas = new List<RecompensaDeNivel>()
    {
        /*nivel 1*/ new RecompensaDeNivel() { textoParaPainel = "Ganhe Xp por completar missões em jogo"},
        /*nivel 2*/ new RecompensaDeNivel(),
        /*nivel 3*/ new LiberaSlote(LiberaSlote.QualSlote.primeiro)
                    { textoParaPainel = "Primeiro slot de equipamentos liberado + 1 equipamento",
                        tenhoAlgoParaMostrar = true
                    },
        /*nivel 4*/ new PrimeiroMultiplicadorGlobal() { textoParaPainel = "Seu multiplicador Global recebe +0.5",
                        tenhoAlgoParaMostrar = true
                    },
        /*nivel 5*/ new RecompensaDeNivel(),
        /*nivel 6*/ new LiberaSlote(LiberaSlote.QualSlote.segundo)
                    { textoParaPainel = "Segundo slot de equipamentos liberado + 1 equipamento",
                        tenhoAlgoParaMostrar = true
                    },
        /*nivel 7*/ new RecompensaDeNivel(),
        /*nivel 8*/ new EquipamentoPorNivel() { textoParaPainel = "Equipamento",
                        tenhoAlgoParaMostrar = true
                    },
        /*nivel 9*/ new RecompensaDeNivel(),
        /*nivel 10*/ new PrimeiraRecompensaDeNivel_RecompensaTradicional() { textoParaPainel = "Recompensa de ouro",
                         tenhoAlgoParaMostrar = true
                     },
        /*nivel 11*/ new LiberaSlote(LiberaSlote.QualSlote.terceiro)
                     { textoParaPainel = "Terceiro slot de equipamentos liberado + 1 equipamento",
                         tenhoAlgoParaMostrar = true
                     },
        /*nivel 12*/ new RecompensaDeNivel(),
        /*nivel 13*/ new EquipamentoPorNivel() { textoParaPainel="Equipamento",
                        tenhoAlgoParaMostrar = true
                    },
        /*nivel 14*/ new MaisMultiplicadorGlobal() { textoParaPainel = "Seu multiplicador Global recebe +0.5"},
        /*nivel 15*/ new RecompensaDeNivel(),
        /*nivel 16*/ new RecompensaDeNivel_RecompensaTradicional() { textoParaPainel="Recompensa de platina"},
        /*nivel 17*/  new EquipamentoPorNivel() { textoParaPainel="Equipamento",
                        tenhoAlgoParaMostrar = true
                    },
        /*nivel 18*/ new RecompensaDeNivel(),
        /*nivel 19*/ new RecompensaDeNivel(),
        /*nivel 20*/ new MaisMultiplicadorGlobal() { textoParaPainel = "Seu multiplicador Global recebe +0.5"},
    };

    public static List<RecompensaDeNivel> listaDeRecompensasParaNivelAlto = new List<RecompensaDeNivel>()
    {
        /*modulo 0*/new RecompensaDeNivel_RecompensaTradicional() { textoParaPainel="Recompensa de platina"},
        /*modulo 1*/new EquipamentoPorNivel() { textoParaPainel="Equipamento",
                        tenhoAlgoParaMostrar = true
                    },
        /*modulo 2*/new RecompensaDeNivel_RecompensaTradicional() { textoParaPainel="Recompensa de platina"},
        /*modulo 3*/new MaisMultiplicadorGlobal() { textoParaPainel="Seu multiplicador Global recebe +0.5"},
        /*modulo 4*/new RecompensaDeNivel_RecompensaTradicional() { textoParaPainel="Recompensa de platina"},
    };

    public static RecompensaDeNivel RecompensaDoNivel(int nivel)
    {
        if (nivel <= 20)
            return listaDeRecompensas[nivel - 1];
        else
            return listaDeRecompensasParaNivelAlto[nivel % 5];
    }
}

public class RecompensaDeNivel
{
    public string textoParaPainel = "";
    public float valorEventual = 0;
    public bool tenhoAlgoParaMostrar = false;
    
    public virtual void AcaoDaRecompensa()
    {

    }

    public virtual void MostrarAlgo(RecebiAlgo recebido, RecebiAlgo.acaoDesteBotao volta)
    {
        
    }
}
