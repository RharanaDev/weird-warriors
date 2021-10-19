using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AnimatedDoodles : MonoBehaviour
{
    public Animator anim;
    public int currentRoundInt;
    public MatchControlScript match;
    [HideInInspector] public bool noTransition;
    [HideInInspector] public static int playersReadyForDoodle;

    //This function is called from the object's animator to keep track of transition state
    void TransitionSwitch(int freeOfTransition)
    {
        noTransition = freeOfTransition == 0 ? false : true;
        match.ControlDisable(noTransition);
    }

    //This function will either be called from player's preparation animation or from script
	public void LetsFightDoodleManager(bool execmode)
	{
		if (execmode)
		{
            playersReadyForDoodle = 2;
        }

		else
		{
            playersReadyForDoodle++;
        }

        if (playersReadyForDoodle == 2)
        {
            playersReadyForDoodle = 0;
            StartCoroutine(WaitForRound(MatchControlScript.currentRound));
        }
	}

    //If round isn't first one, makes doodle wait for time between rounds
    public IEnumerator WaitForRound(int round)
    {
        float timeToWait;
        timeToWait = round == 1 ? 0 : MatchControlScript.timeBetweenRounds;
        yield return new WaitForSeconds(timeToWait);
        anim.SetTrigger("Fight");
    }

    //Called from animator, changes round animator int
    public void NumberUp()
    {
        currentRoundInt++;
        anim.SetInteger("Rounds", currentRoundInt);
    }
}
