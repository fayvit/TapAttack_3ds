using UnityEngine;
using System.Collections;

[System.Serializable]
public class AplicadorAfastadorDeInimigo
{
    [SerializeField]private float distanciaDeAfastamento = 40;
    [SerializeField]private float tempoDeAfastamento = 0.25f;
    [SerializeField]private float distanciaMaxParaOAfastamento = 50;

    
    public void AfastaInimigoDeDano(Transform heroi)
    {
        GameObject[] inimigos = InimigoBase.InimigosPerto(distanciaMaxParaOAfastamento, heroi.position);
        
        foreach (GameObject I in inimigos)
        {            
            AfastadorDeInimigoEmDano.InsereAfastamento(
                (Vector3.ProjectOnPlane(I.transform.position - heroi.position,Vector3.up)).normalized,                    
                distanciaDeAfastamento,
                tempoDeAfastamento,
                I
                );               
        }
    }
}
