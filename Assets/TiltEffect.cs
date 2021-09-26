using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltEffect : MonoBehaviour
{
    bool doIt;
    public static int bars;
    public SpriteRenderer rend;
    public float cooldown, stayance;

    public IEnumerator Hey(int HowMuchBars)
    {
        for (int i = 0; i < HowMuchBars; i++)
        {
            rend.enabled = true;
            yield return new WaitForSecondsRealtime(stayance);
            rend.enabled = false;
            yield return new WaitForSecondsRealtime(cooldown);
        }
    }
}
