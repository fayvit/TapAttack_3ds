using UnityEngine;
using System.Collections;

public class VerificaAcoesEspeciais
{
    public static void VerificaAcaoDeBonus(Vector3 T, BonusDePersonagem bonus)
    {
        Debug.Log(T);
        GameObject elemento;
        switch(bonus)
        {
            case BonusDePersonagem.checkComboEspecial:
                elemento = ControladorDeJogo.c.RetornaElemento(Elementos.checkCombo);
                SpawnaItem(T,elemento);
            break;
            case BonusDePersonagem.moedasEspecial:
                elemento = ControladorDeJogo.c.RetornaElemento(Elementos.Moeda);
                SpawnaItem(T,elemento);
            break;
        }
            
    }

    public static Vector3 PosMelhorada(Vector3 V)
    {
        return V+2*Vector3.ProjectOnPlane(
            Random.onUnitSphere,
            Vector3.up
            ).normalized+1.5f*Vector3.up;
        }

    public static void SpawnaItem(Vector3 posBase,GameObject item)
    {
        Vector3 posMelhorada = PosMelhorada(posBase);
        for (int i = 0; i < 5; i++)
        {
            MonoBehaviour.Instantiate(item, posMelhorada, item.transform.rotation);
            posMelhorada = PosMelhorada(posBase);
        }
    }
}
