using UnityEngine;
using System.Collections;

public class Sapinho : inimigo1
{
    

    [System.Serializable]
    private class ElementosDoSomDosSapos
    {
        public float tempoMinDeCoachada = 1;
        public float tempoMaxDeCoachada = 20;
        public float tempoAteProximaCoachada = 0;
        public float tempoDecorrido = 0;
        [SerializeField]public AudioClip[] coachadas;
        [SerializeField]public AudioSource audioX;
    }

    [SerializeField]private ElementosDoSomDosSapos elementosDeSom;
    

    protected override void SomDoInimigo()
    {
        elementosDeSom.tempoDecorrido += Time.deltaTime;
        if (elementosDeSom.tempoDecorrido > elementosDeSom.tempoAteProximaCoachada)
        {
            elementosDeSom.audioX.clip = elementosDeSom.coachadas[Random.Range(0, elementosDeSom.coachadas.Length)];
            elementosDeSom.audioX.Play();
            elementosDeSom.tempoDecorrido = 0;
            elementosDeSom.tempoAteProximaCoachada = Random.Range(elementosDeSom.tempoMinDeCoachada, elementosDeSom.tempoMaxDeCoachada);
        }
    }
    protected override Vector3 PontoAlvo()
    {
        return base.PontoAlvo();
    }
    protected override void AnimaGiro()
    {
        animator.SetBool("proLado", true);
    }

    protected override void ParaAnimacaoDeGiro()
    {
        animator.SetBool("proLado", false);
    }
}
