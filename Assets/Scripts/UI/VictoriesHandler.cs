using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoriesHandler : MonoBehaviour
{
    public GameObject p1v1, p1v2, p2v1, p2v2;
    public int numberOfVictories1 = 0, numberOfVictories2 = 0;
    public bool bul;
    public int num;
    // Start is called before the first frame update

    public IEnumerator AddVictory(int playerNumber)
    {
        yield return new WaitUntil(() => PlayerAnimationController.timeToWin);
        switch (playerNumber)
        {
            case 1:
                numberOfVictories1++;
                if (numberOfVictories1 == 1)
                {
                    p1v1.SetActive(true);
                }
                else if (numberOfVictories1 == 2)
                {
                    p1v2.SetActive(true);
                }
                break;
            case 2:
                numberOfVictories2++;
                if (numberOfVictories2 == 1)
                {
                    p2v1.SetActive(true);
                }
                else if (numberOfVictories2 == 2)
                {
                    p2v2.SetActive(true);
                }
                break;
        }
        PlayerAnimationController.timeToWin = false;
    }
}
