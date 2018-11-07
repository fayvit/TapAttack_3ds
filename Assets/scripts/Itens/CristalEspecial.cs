using UnityEngine;
using System.Collections;

public class CristalEspecial : Coletavel
{
    protected override GameObject ParticulaDeColetavelEAcaoColetavel(DadosDoPersonagem dados)
    {
        dados.AdicionaCristais(valor);
        ControladorGlobal.c.EmJogo.Esferas++;
        return ControladorDeJogo.c.RetornaElemento(Elementos.pegueiCristal);
    }
}
