using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_KO : MonoBehaviour
{
    public GameObject KO;
    public Animator anim;
    public float delay;
    public UI_RoundTransition roundTrans;
    public bool MatchWillOver;
    public bool koAnimEnded;
    public float previousTime;
    // Update is called once per frame
    private void Start()
    {
        anim = KO.GetComponent<Animator>();
    }
    //Interferes in time and awaits for an event to end the slowdown
    public IEnumerator AppearKO()
    {
        koAnimEnded = false;
        yield return new WaitForSecondsRealtime(delay);
        KO.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitUntil(()=> koAnimEnded == true);
        Time.timeScale = 1;
        KO.SetActive(false);
    }

    //This event makes
    public void EndKO()
    {
        koAnimEnded = true;
    }
}
