using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCabinScript : MonoBehaviour
{

    public Animator[] animations;
    public float buttonCooldown, cabinCooldown;
    public GameObject[] cratePool; //Is Preset From Inspector

    public void SelectNewCabin(int position)
    {
        if (position == -1)
        {
            StopCoroutine("AnimateOnHover");
        }
        else
        {
            GetAnimations(position);
            StopCoroutine("AnimateOnHover");
            StartCoroutine("AnimateOnHover");
        }
    }

    //With positioning info, save buttons with animator on array
    private void GetAnimations(int cabinID)
    {
        animations = cratePool[cabinID].GetComponentsInChildren<Animator>();
    }

    //While activated, animates cabin
    private IEnumerator AnimateOnHover()
    {
        while (true)
        {
            foreach (Animator anim in animations)
            {
                anim.SetTrigger("Activated");
                yield return new WaitForSecondsRealtime(buttonCooldown);
            }
            yield return new WaitForSecondsRealtime(cabinCooldown);
        }
    }
}
