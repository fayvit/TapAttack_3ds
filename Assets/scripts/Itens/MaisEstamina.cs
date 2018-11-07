using UnityEngine;
class MaisEstamina:Coletavel
{
    protected override GameObject ParticulaDeColetavelEAcaoColetavel(DadosDoPersonagem dados)
    {
        dados.AdicionaEstamina(valor);
        ControladorGlobal.c.EmJogo.Estaminas++;
        return ControladorDeJogo.c.RetornaElemento(Elementos.parMoeda);
    }
}

