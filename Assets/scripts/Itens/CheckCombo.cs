using UnityEngine;

public class CheckCombo : Coletavel
{
    protected override GameObject ParticulaDeColetavelEAcaoColetavel(DadosDoPersonagem dados)
    {
        ControladorDeJogo.c.G_Combos.AdicionaCombo(50);
        ControladorGlobal.c.EmJogo.Cubos++;
        return ControladorDeJogo.c.RetornaElemento(Elementos.checkComboParticles);
    }

}
