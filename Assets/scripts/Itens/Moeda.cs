using UnityEngine;
using System.Collections;

public class Moeda : Coletavel
{
    private Transform personagem;
    private Vector3 vetorDireacao = Vector3.zero;

    protected override GameObject ParticulaDeColetavelEAcaoColetavel(DadosDoPersonagem dados)
    {
        dados.AdicionaDinheiro(valor);
        ControladorGlobal.c.EmJogo.Moedas++;
        return ControladorDeJogo.c.RetornaElemento(Elementos.parMoeda);
    }

    new void Update()
    {
        base.Update();

        if (ControladorDeJogo.c.MoedasMagneticas)
        {
            if (personagem == null)
            {
                personagem = GameObject.FindWithTag("Player").transform;
            }
            else
            {
                if (Vector3.Distance(transform.position, personagem.position) < 20)
                {
                    vetorDireacao = Vector3.Lerp(vetorDireacao,
                        Vector3.ProjectOnPlane((personagem.position - transform.position),Vector3.up).normalized
                        * (40 - Vector3.Distance(transform.position, personagem.position))
                        , Time.deltaTime);

                    transform.position+= (vetorDireacao*Time.deltaTime);
                }
                else
                {
                    vetorDireacao = Vector3.zero;
                }
            }
        }
    }
}
