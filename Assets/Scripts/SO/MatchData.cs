using UnityEngine;

[CreateAssetMenu(fileName = "New MatchData", menuName = "MatchData")]
public class MatchData : ScriptableObject
{
	public int number;
	public int playerCharacter;
	public void Reset()
	{
		playerCharacter = 0;
	}
}
