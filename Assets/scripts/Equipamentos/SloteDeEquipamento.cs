using UnityEngine;
using System.Collections;

[System.Serializable]
public struct SloteDeEquipamento
{

    [SerializeField]private bool desbloqueado;
    [SerializeField]private bool preenchido;
    [SerializeField]private EquipamentoBase equipamentoNoSlote;

    public bool Desbloqueado
    {
        get { return desbloqueado; }
        set { desbloqueado = value; }
    }

    public bool Preenchido
    {
        get { return preenchido; }
        set { preenchido = value; }
    }

    public EquipamentoBase EquipamentoNoSlote
    {
        get { return equipamentoNoSlote; }
        set { equipamentoNoSlote = value; }
    }

    public void RemoveEquipamento()
    {
        equipamentoNoSlote = new EquipamentoBase();
        preenchido = false;
    }
}
