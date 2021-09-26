using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DramaticPause
{
	public static bool isntRunning;
	public static IEnumerator Pause(float time)
	{
		if (!isntRunning)
		{
			isntRunning = true;
			Time.timeScale = 0;
			yield return new WaitForSecondsRealtime(time);
			Time.timeScale = 1;
			isntRunning = false;
		}
	}
}
