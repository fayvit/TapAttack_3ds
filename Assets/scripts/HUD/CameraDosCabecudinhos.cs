using UnityEngine;
using System.Collections;

public class CameraDosCabecudinhos : MonoBehaviour
{
    [SerializeField]private GameObject[] eles;

    public static CameraDosCabecudinhos c;
    // Use this for initialization
    void Start()
    {
        c = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EscolheCabecudinho(int qual)
    {
        for (int i = 0; i < eles.Length; i++)
        {
            if (qual == i)
                eles[i].SetActive(true);
            else
                eles[i].SetActive(false);
        }
    }
}
