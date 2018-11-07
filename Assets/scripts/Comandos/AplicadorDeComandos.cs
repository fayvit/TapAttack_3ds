using UnityEngine;
using System.Collections;

public class AplicadorDeComandos
{
    private Vector3 posDePressao = Vector3.zero;
    private movimentacaoPointinTouch mov;
    private DadosDoPersonagem dados;
    private Transform transform;

    // Use this for initialization
    public AplicadorDeComandos(DadosDoPersonagem dados,GameObject gameObject)
    {
        mov = new movimentacaoPointinTouch(gameObject);
        transform = gameObject.transform;
        this.dados = dados;
    }

    public movimentacaoPointinTouch Mov
    {
        get { return mov; }
    }

    public void RetornarDoPause()
    {
        Mov.PararMovimento();
        posDePressao = Input.mousePosition;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            posDePressao = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            
            if (Vector3.Distance(Input.mousePosition, posDePressao) > 75)
            {
                if (dados.EstaminaCorrente >= 1)
                {
                    AtaquePrincipal.InsereAtaque(posDePressao, Input.mousePosition, transform.position.y + 1, transform.gameObject,
                        dados.Forca
                        );
                    dados.UtilizarEstamina(1);
                }
                else
                {
                    dados.UtilizarEstamina(0);
                    GerenciadorDeHUD.piscaEstamina.AcionarPiscaEstamina();
                }
            }
            else if (Vector3.Distance(Input.mousePosition, posDePressao) <11)
            {
                mov.Update();
            }
        }

        mov.AnimaMove();
    }
}
