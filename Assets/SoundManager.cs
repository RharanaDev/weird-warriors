using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] attackSounds;
    public AudioClip current;
    public AudioSource evoke;
    // Update is called once per frame
    public void AttackSound(int number)
    {
        current = attackSounds[number];
        evoke.clip = current;
        evoke.Play();
    }
}
