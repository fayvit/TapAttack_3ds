using UnityEngine;
using System.Collections;

[System.Serializable]
public class PiscarMaterial
{
    [SerializeField]private float velocidadeDaMudanca = 10f;
    private float opacidade = 0;
    private bool opacidadeCrescente = true;

    float InterpolacaoZeroUm()
    {
        if (opacidadeCrescente)
        {
            opacidade = Mathf.Lerp(opacidade, 1, velocidadeDaMudanca * Time.deltaTime);

            if (Mathf.Abs(opacidade - 1) < 0.1f)
                opacidadeCrescente = false;
        }
        else
        {
            opacidade = Mathf.Lerp(opacidade, 0, velocidadeDaMudanca * Time.deltaTime);

            if (Mathf.Abs(opacidade) < 0.1f)
                opacidadeCrescente = true;
        }

        return opacidade;
    }

    public void PiscarCor(Material material,string cor = "_TintColor")
    {
        Color C = material.GetColor(cor);        

        C = new Color(C.r, C.g, C.b, InterpolacaoZeroUm());
        material.SetColor(cor,C);

    }

    public void PiscarFloat(Material material, string prop)
    {
        material.SetFloat(prop, InterpolacaoZeroUm());
    }
}
