using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]private float tempo = 10;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, tempo);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
