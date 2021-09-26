using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    public void Beam()
    {
        anim.SetTrigger("Ray");
    }
}
