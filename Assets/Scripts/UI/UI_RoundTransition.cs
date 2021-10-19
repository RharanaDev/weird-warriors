using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_RoundTransition : MonoBehaviour
{
	public Animator anim;
	public MatchControlScript match;
	public UI_AnimatedDoodles animatedUI;
	[HideInInspector] public static int playersReady;

	//Produce endgame fade or betweengame fade
	public void FadeEffect(bool lastRound)
	{
		anim.SetTrigger(lastRound ? "FadeTotalBlack" : "FadeBlack");
	}
	//Waits until the character's death/ victory is over
	public IEnumerator WaitForEventToFade()
	{
		yield return new WaitUntil(() => PlayerAnimationController.timeToFade);
		if (MatchControlScript.matchWillOver)
		{
			yield return new WaitUntil(() => PlayerAnimationController.timeToFadeAbs);
		}
		PlayerAnimationController.timeToFade = false;
		PlayerAnimationController.timeToFadeAbs = false;
		FadeEffect(MatchControlScript.matchWillOver);
	}
	public void ResetMatch()
	{
		match.ResetMatch();
	}
	//This function is called from animator to call 2nd and 3rd round doodles
	public void CallNextRoundDoodle()
	{
		animatedUI.LetsFightDoodleManager(true);
	}
	public void BackToMenu()
	{
		SceneManager.LoadScene("MenuSelection");
	}
}
