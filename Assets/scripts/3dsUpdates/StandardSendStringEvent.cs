using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardSendStringEvent : IGameEvent {
    
    private string _nomeParticulasGolpe;
    private GameObject _sender;
    private EventKey _key;

    public GameObject Sender
    {
        get { return _sender; }
    }

    public EventKey Key
    {
        get { return _key; }
    }

    public string StringContent
    {
        get { return _nomeParticulasGolpe; }
        private set { _nomeParticulasGolpe = value; }
    }

    public StandardSendStringEvent(GameObject sender,string nomeGolpe,EventKey key)
    {
        _nomeParticulasGolpe = nomeGolpe;
        _sender = sender;
        _key = key;
    }
}
