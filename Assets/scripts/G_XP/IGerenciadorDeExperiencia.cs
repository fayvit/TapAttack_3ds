﻿
public interface IGerenciadorDeExperiencia
{
    int XP { get; set; }
    int Nivel { get; set; }
    int ParaProxNivel { get; set; }
    float ModNIvel { get; set; }
    int UltimoPassaNivel { get; }

    int CalculaPassaNivelInicial(int N, bool tudo = false);
    bool VerificaPassaNivel();
    void AplicaPassaNivel(/*IAtributos A*/);
    void SimulaPassaNivel(/*IAtributos A, */int ateONivel = -1);
    int CalculaPassaNivelAtual();
}
