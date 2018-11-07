using UnityEngine;
using System.Collections;

public class AtaquePrincipal
{
    public static void InsereAtaque(Vector3 partida, Vector3 chegada,float altura,GameObject dono,int forca)
    {
        //Debug.Log(partida + " : " + chegada + " : " + Screen.width + " : " + Screen.height);
        bool foi = true;

        PosNoMapa posM = MelhoraInstancia.TelaParaMundo3D(partida);
        foi = posM.estaNoMapa;
        partida = posM.pos;

        posM = MelhoraInstancia.TelaParaMundo3D(chegada);
        foi &= posM.estaNoMapa;
        chegada = posM.pos;

        if (foi)
        {
            partida = new Vector3(partida.x, altura, partida.z);
            chegada = new Vector3(chegada.x, altura, chegada.z);

            Elementos elemento;

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Tutorial")
            {
                switch (ControladorGlobal.c.DadosGlobais.PerfilAtualSelecionado.PersonagemAtualSelecionado.Bonus)
                {
                    case BonusDePersonagem.maisAtaque:
                        elemento = Elementos.cuboMaisAtaque;
                    break;
                    case BonusDePersonagem.esferasDeFogo:
                        elemento = Elementos.cuboEmChamas;
                    break;
                    default:
                        elemento = Elementos.cuboColisor;
                    break;
                }
            }else
                elemento = Elementos.cuboColisor;

            Transform T = MonoBehaviour.Instantiate(ControladorDeJogo.c.RetornaElemento(elemento)).transform;
            AplicadorDeDano apD = T.GetComponent<AplicadorDeDano>();
            apD.dono = dono;
            apD.dano = forca;

            T.rotation = Quaternion.LookRotation(chegada - partida);
            T.position = 0.5f * (chegada + partida);
            T.localScale = new Vector3(elemento==Elementos.cuboMaisAtaque? 10:5, 1, 0.75f * (chegada - partida).magnitude);

            MonoBehaviour.Destroy(T.gameObject, 0.25f);
        }
    }
}
