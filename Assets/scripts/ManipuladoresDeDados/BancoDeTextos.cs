using UnityEngine;
using System.Collections.Generic;

public static class BancoDeTextos
{
    public static idioma idiomaAtual = idioma.pt_br;
    public static Dictionary<idioma, Dictionary<ChavesDeTexto, string>> textos
        = new Dictionary<idioma, Dictionary<ChavesDeTexto, string>>()
        {
            { idioma.pt_br, new Dictionary<ChavesDeTexto, string>()
            {
                { ChavesDeTexto.nulo,  "A chave de texto foi considerada nula" },
                { ChavesDeTexto.checkComboEspecial, "Especial transforma inimigos derrotados em Check Combos"},
                { ChavesDeTexto.esferasDeFogo,
                    "Possui um ataque especial com lâminas de fogo"
                } ,
                { ChavesDeTexto.maisAtaque,
                    "Tem um ataque mais potente"
                } ,
                { ChavesDeTexto.moedasEspecial,
                    "Especial transforma inimigos derrotados em Moedas"
                 },
                { ChavesDeTexto.naoPerdeCheckCombo,
                    "Não perde o combo quando toma dano"
                } ,
                { ChavesDeTexto.vidaExtra,
                    "Recebe uma vida extra"
                },
                { ChavesDeTexto.tempoDeCombo,
                    "Tempo de Combo dura {0}% a mais"
                } ,
                { ChavesDeTexto.pontoPorInimigo,
                    "Recebe {0}% a mais de pontos por inimigo"
                } ,
                { ChavesDeTexto.pontoDeCombo,
                    "Recebe {0}% a mais de pontos por combo"
                 },
                { ChavesDeTexto.MelhorarComDim,
                    "Melhorar de {0}% para {1}% por {2} Moedas"
                } ,
                { ChavesDeTexto.MelhorarComEstrela,
                    "Melhorar de {0}% para {1}% por {2} estrelas"
                } ,
                { ChavesDeTexto.botaoComprarPersonagem,
                    "Habilitar esse personagem por {0} estrelas"
                },
                { ChavesDeTexto.nomeDoPerfilNulo,
                    "O nome do perfil não pode ser vazio"
                },
                { ChavesDeTexto.deletouOPerfil,
                    "O Perfil {0} foi deletado com sucesso"
                },
                { ChavesDeTexto.DropZerado,
                    "Criar um Perfil"
                },
                { ChavesDeTexto.nomesIguais,
                    "O nome escolhido é igual ao nome do perfil"
                },
                { ChavesDeTexto.nomeTrocado,
                    "O nome do perfil foi trocado com sucesso"
                },
                { ChavesDeTexto.temCertezaQueQuerTrocarNome,
                    "Você tem certeza que quer trocar o nome do perfil \"{0}\" para \"{1}\""
                },
                { ChavesDeTexto.perfilCriado,
                    "O perfil {0} foi criado com sucesso"
                },
                { ChavesDeTexto.certezaDeReiniciarJogo,
                    "O progresso atual será perdido. Tem certeza que deseja reiniciar o jogo?"
                },
                { ChavesDeTexto.certezaVoltarAoTitulo,
                    "O progresso atual será perdido. Tem certeza que deseja ao titulo?"
                },
                { ChavesDeTexto.indicativoDaMissaoalcanceCombo,
                    "Alcance {0} no marcador de Combos no jogo atual"
                },
                { ChavesDeTexto.indicativoDaMissaoalcanceNivel,
                    "Alcance o nivel {0} no jogo atual"
                },
                { ChavesDeTexto.indicativoDaMissaocheckCombosEmUnicoJogo,
                    "Colete {0} CheckCombos no jogo atual"
                },
                { ChavesDeTexto.indicativoDaMissaocoleteCheckCombos,
                    "Colete {0} CheckCombos"
                },
                { ChavesDeTexto.indicativoDaMissaocoleteEsferas,
                    "Colete {0} esferas de especial"
                },
                { ChavesDeTexto.indicativoDaMissaocoleteEstaminas,
                    "Colete {0} moedas de estamina"
                },
                { ChavesDeTexto.indicativoDaMissaocoleteMoedas,
                    "Colete {0} moedas douradas"
                },
                { ChavesDeTexto.indicativoDaMissaoderroteInimigos,
                    "Derrote um total de {0} inimigos"
                },
                { ChavesDeTexto.indicativoDaMissaoesferasEmUnicoJogo,
                    "Colete {0} esferas no jogo atual"
                },
                { ChavesDeTexto.indicativoDaMissaoestaminasEmUnicoJogo,
                    "Colete {0} moedas de estamina no jogo atual"
                },
                { ChavesDeTexto.indicativoDaMissaoinimigosEmUnicoJogo,
                    "Derrote {0} inimigos no jogo atual"
                },
                { ChavesDeTexto.indicativoDaMissaomoedasEmUnicoJogo,
                    "Colete {0} moedas douradas no jogo atual"
                },
                { ChavesDeTexto.indicativoDaMissaopasseDeNivel,
                    "Passe de nivel {0} vezes"
                },
                { ChavesDeTexto.indicativoDaMissaopontuacaoEmUnicoJogo,
                    "Alcance {0} pontos no jogo atual"
                },
                { ChavesDeTexto.indicativoDaMissaosomePontuacao,
                    "Consiga {0} pontos"
                },
                { ChavesDeTexto.missaoCumprida,
                    "Concluir a missão \n {0}"
                },
                { ChavesDeTexto.descricaoEquipanelEspecialMaisPotente,
                    "Seu especial é {0}% mais potente"
                },
                { ChavesDeTexto.descricaoEquipanelMaisMoeda,
                    "As moedas douradas aparecem {0}% mais"
                },
                { ChavesDeTexto.descricaoEquipanelMaisEstamina,
                    "As moedas de estamina aparecem {0}% mais"
                },
                { ChavesDeTexto.descricaoEquipanelMaisCheckCombos,
                    "Os CheckCombos aparecem {0}% mais"
                },
                { ChavesDeTexto.descricaoEquipanelMaisEsferas,
                    "As esferas de especial aparecem {0}% mais"
                },
                { ChavesDeTexto.descricaoEquipanelMenosCustoDeEsfera,
                    "A barra de especial enche {0}% mais rápido"
                },
                { ChavesDeTexto.MelhorarEquipComDim,
                    "Melhorar de {0}% para {1}% por {2} Moedas"
                },
                { ChavesDeTexto.MelhorarEquipComEstrelas,
                    "Melhorar de {0}% para {1}% por {2} Estrelas"
                },
                { ChavesDeTexto.descricaoEquipanelMaisDefesa,
                    "Te da o dobro de pontos de vida"
                },
                { ChavesDeTexto.descricaoEquipanelMaisAtaque,
                    "Te da o dobro de pontos de ataque"
                },
                { ChavesDeTexto.descricaoEquipanelMagnetico,
                    "Esse anel atrai as moedas douradas quando você se aproxima"
                },
                { ChavesDeTexto.descricaoEquipanelMaisTempoDeCombo,
                    "Dobra o tempo até a barra de combo esvaziar"
                },
                { ChavesDeTexto.descricaoEquipanelVidaExtra,
                    "Recebe uma vida extra"
                }
            }
            }
        };

    public static string TextosDoIdioma(ChavesDeTexto chave)
    {
        return textos[idiomaAtual][chave];
    }

    public static string TextosDoIdioma(string chave)
    {
        return TextosDoIdioma(StringParaChaveDeTexto(chave));
    }

    public static ChavesDeTexto StringParaChaveDeTexto(string s)
    {
        ChavesDeTexto C = AuxiliarDeEnum.StringParaEnum<ChavesDeTexto>(s);
        if (C == ChavesDeTexto.nulo)
        {
            Debug.LogError("A chave de textofoi considerada nula");
        }
        
        return C;
    }
}

public enum idioma
{
    pt_br
}

public enum ChavesDeTexto
{
    nulo = 0,
    checkComboEspecial,
    esferasDeFogo,
    maisAtaque,
    moedasEspecial,
    naoPerdeCheckCombo,
    vidaExtra,
    pontoDeCombo,
    pontoPorInimigo,
    tempoDeCombo,
    MelhorarComDim,
    MelhorarComEstrela,
    botaoComprarPersonagem,
    nomeDoPerfilNulo,
    deletouOPerfil,
    DropZerado,
    nomesIguais,
    nomeTrocado,
    temCertezaQueQuerTrocarNome,
    perfilCriado,
    certezaDeReiniciarJogo,
    certezaVoltarAoTitulo,
    indicativoDaMissaocoleteMoedas,
    indicativoDaMissaomoedasEmUnicoJogo,
    indicativoDaMissaocoleteEstaminas,
    indicativoDaMissaoestaminasEmUnicoJogo,
    indicativoDaMissaocoleteEsferas,
    indicativoDaMissaoesferasEmUnicoJogo,
    indicativoDaMissaocoleteCheckCombos,
    indicativoDaMissaocheckCombosEmUnicoJogo,
    indicativoDaMissaoderroteInimigos,
    indicativoDaMissaoinimigosEmUnicoJogo,
    indicativoDaMissaosomePontuacao,
    indicativoDaMissaopontuacaoEmUnicoJogo,
    indicativoDaMissaopasseDeNivel,
    indicativoDaMissaoalcanceCombo,
    indicativoDaMissaoalcanceNivel,
    missaoCumprida,
    descricaoEquipanelMaisMoeda,
    descricaoEquipanelMaisEstamina,
    descricaoEquipanelMaisCheckCombos,
    descricaoEquipanelMaisEsferas,
    descricaoEquipanelEspecialMaisPotente,
    descricaoEquipanelMenosCustoDeEsfera,
    descricaoEquipanelMaisAtaque,
    descricaoEquipanelMaisDefesa,
    descricaoEquipanelMaisTempoDeCombo,
    descricaoEquipanelMagnetico,
    descricaoEquipanelVidaExtra,
    MelhorarEquipComDim,
    MelhorarEquipComEstrelas
}
