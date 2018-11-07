using UnityEngine;
using System.Collections;

public class SpriteDeEquipamento : MonoBehaviour
{
    public Sprite[] SpritesDeEquipamentos;
    public static SpriteDeEquipamento s;
    void Start()
    {
        s = this;
    }

    public Sprite RetornaSprite(string s)
    {
        Sprite retorno = SpritesDeEquipamentos[0];

        for (int i = 0; i < SpritesDeEquipamentos.Length; i++)
        {
            if (SpritesDeEquipamentos[i].name == s)
            {
                retorno = SpritesDeEquipamentos[i];
            }
        }

        return retorno;
    }
    public Sprite RetornaSprite(TiposDeEquipamento tipo)
    {
        Sprite retorno = SpritesDeEquipamentos[0];
        retorno = RetornaSprite(tipo.ToString());
        return retorno;
    }
}
