using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraScript : MonoBehaviour
{
    public ParticleSystem particle;
    Animator anim;
    // Start is called before the first frame update


    // Update is called once per frame

    public void PlayAura()
    {
        particle.Play();
    }
    public void StopAura()
    {
        particle.Stop();
    }
}