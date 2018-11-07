using UnityEngine;
using System.Collections;

public static class ListaDePersonagens
{
    public static Personagem[] Lista()
    {
        return new Personagem[6]
            {
                new Personagem() {
                    NomeDoPersonagem="Ink Anchor",
                    Bonus = BonusDePersonagem.moedasEspecial,
                    Habilidade = HabilidadePersonagem.tempoDeCombo,
                    Bloqueado = false
                },
                new Personagem() {
                    NomeDoPersonagem="Danielly Gold",
                    Bonus = BonusDePersonagem.checkComboEspecial,
                    Habilidade = HabilidadePersonagem.pontoDeCombo,
                    CustoDeDesbloqueio = 30
                },
                new Personagem() {
                    NomeDoPersonagem="Grey Knight",
                    Bonus = BonusDePersonagem.maisAtaque,
                    Habilidade = HabilidadePersonagem.pontoPorInimigo,
                    CustoDeDesbloqueio=30
                },
                new Personagem() {
                    NomeDoPersonagem="Charlie Heart",
                    Bonus = BonusDePersonagem.naoPerdeCheckCombo,
                    Habilidade = HabilidadePersonagem.pontoDeCombo,
                    CustoDeDesbloqueio=50
                }
                ,
                new Personagem() {
                    NomeDoPersonagem="Akina Portale",
                    Bonus = BonusDePersonagem.vidaExtra,
                    Habilidade = HabilidadePersonagem.tempoDeCombo,
                    CustoDeDesbloqueio=70
                },
                new Personagem() {
                    NomeDoPersonagem="Billy Frame",
                    Bonus = BonusDePersonagem.esferasDeFogo,
                    Habilidade = HabilidadePersonagem.pontoPorInimigo,
                    CustoDeDesbloqueio=120
                }
            };
    }
    
}
