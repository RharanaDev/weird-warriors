using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour
{
    public ParticleSystem particles;

    // Start is called before the first frame update
    public void Bleed()
    {
        particles.Play();
    } 
}
