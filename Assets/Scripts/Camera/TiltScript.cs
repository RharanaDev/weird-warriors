using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltScript : MonoBehaviour
{
	public Animator anim;
	public MatchControlScript match;
	public static int playersReady;
	public Animator UIanim;
	bool notFirstRound;
	public float timeBetweenRounds;
	public IEnumerator isTilting(float time)
	{
		anim.SetBool("Tilting", true);
		yield return new WaitForSeconds(time);
		anim.SetBool("Tilting", false);
	}
	public void Fade(bool aboutToend)
	{
		if (aboutToend)
		{
			anim.SetTrigger("FadeTotalBlack");
		}
		else if(!aboutToend)
		{
			anim.SetTrigger("FadeBlack");
		}
	}
	public void ResetMatch()
	{
		match.ResetMatch();
	}
	public void LetsFight(int subject)
	{
		if (subject == 0)
		{
			notFirstRound = true;
			playersReady++;
			if (playersReady == 2)
			{
				playersReady = 0;
				UIanim.SetTrigger("Fight");
			}
		}
		else if(subject == 1 && notFirstRound)
		{
			StartCoroutine("WaitForRound");
		}
	}
	public IEnumerator WaitForRound()
	{
		yield return new WaitForSeconds(timeBetweenRounds);
		UIanim.SetTrigger("Fight");
	}
}
