using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public GameObject KO;
    public Animator anim;
    public float delay;
    public TiltScript tilt;
    public bool MatchWillOver;
    public bool koAnimEnded;
    // Update is called once per frame
    private void Start()
    {
        anim = KO.GetComponent<Animator>();
    }
    public IEnumerator AppearKO()
    {
        koAnimEnded = false;
        float previousTime;
        yield return new WaitForSecondsRealtime(delay);
        KO.SetActive(true);
        previousTime = Time.timeScale;
        Time.timeScale = 0.1f;
        yield return new WaitUntil(()=> koAnimEnded == true);
        Time.timeScale = previousTime;
    }
    public IEnumerator FadeToGrey()
    {
        yield return new WaitUntil(() => PlayerAnimationController.timeToFade);
        PlayerAnimationController.timeToFade = false;
        tilt.Fade(MatchWillOver);
        StopCoroutine("Destroyance");
        StartCoroutine("Destroyance");
    }
    public IEnumerator Destroyance()
    {
        yield return new WaitForSeconds(0.5f);
        KO.SetActive(false);
    }
    public void EndKO() // CODING THIS DRUNK SPECIAL ATTENTION TO LINES UNDER THIS COMMENT
    {
        koAnimEnded = true;
    }
}
