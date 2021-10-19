using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchControlScript : MonoBehaviour
{
    public InstanceSaver dataBasePlayer1;
    public InstanceSaver dataBasePlayer2;
    public int player1wins, player2wins;
    public VictoriesHandler victor;
    public int lastPlayerWon;
    public UI_KO KO;
    public Vector3 camPos;
    public bool alreadyChosenCamPos;
    public UI_RoundTransition roundTrans;
    public UI_AnimatedDoodles animatedUI;
    public Cam_Movement camMov;
    [HideInInspector] public static bool matchWillOver;
    [HideInInspector] public static float timeBetweenRounds = 0.25f;
    [HideInInspector] public static int currentRound = 1;
    public void Start()
    {
        ControlDisable(false);
    }
    public void OnWin(int loser, bool fromTime)
    {
        currentRound++;
        StartCoroutine(roundTrans.WaitForEventToFade());
        if (!fromTime)
		{
            switch (loser)
            {
                case 2:
                    player1wins++;
                    victor.StartCoroutine("AddVictory", 1);
                    lastPlayerWon = 1;
                    if (player1wins < 2)
                    {
                        StartCoroutine(KO.AppearKO());
                    }
                    else if (player1wins == 2)
                    {
                        StartCoroutine(WaitVictory(1));
                        KO.MatchWillOver = true;
                        StartCoroutine(KO.AppearKO());
                        matchWillOver = true;
                        StartCoroutine("GoHome");
                    }
                    break;
                case 1:
                    player2wins++;
                    victor.StartCoroutine("AddVictory", 2);
                    lastPlayerWon = 2;
                    if (player2wins < 2)
                    {
                        StartCoroutine(KO.AppearKO());
                    }
                    else if (player2wins == 2)
                    {
                        StartCoroutine(WaitVictory(2));
                        KO.MatchWillOver = true;
                        matchWillOver = true;
                        StartCoroutine(KO.AppearKO());
                        StartCoroutine("GoHome");
                    }
                    break;
            }
        }
		else
		{
            switch (loser)
            {
                case 2:
                    player1wins++;
                    victor.StartCoroutine("AddVictory", 1);
                    lastPlayerWon = 1;
                    if (player1wins < 2)
                    {
                        roundTrans.FadeEffect(matchWillOver);
                    }
                    else if (player1wins == 2)
                    {
                        StartCoroutine(WaitVictory(1));
                        KO.MatchWillOver = true;
                        matchWillOver = true;
                        roundTrans.FadeEffect(matchWillOver);
                        StartCoroutine("GoHome");
                    }
                    break;
                case 1:
                    player2wins++;
                    victor.StartCoroutine("AddVictory", 2);
                    lastPlayerWon = 2;
                    if (player2wins < 2)
                    {
                        roundTrans.FadeEffect(matchWillOver);
                    }
                    else if (player2wins == 2)
                    {
                        StartCoroutine(WaitVictory(2));
                        matchWillOver = true;
                        roundTrans.FadeEffect(matchWillOver);
                        StartCoroutine("GoHome");
                    }
                    break;
            }
		}
        
        ControlDisable(false);
    }
    public void ControlDisable(bool enable)
    {
        if (enable)
        {
            if (!alreadyChosenCamPos)
            {
                camPos = transform.position;
                alreadyChosenCamPos = true;
            }
            camMov.enableCamMove = true;
            dataBasePlayer1.inputScript.enabled = dataBasePlayer1.myAttackScript.enabled = dataBasePlayer1.myDamageScript.enabled = dataBasePlayer1.myMoveScript.enabled = dataBasePlayer1.myMana.enabled = true;
            dataBasePlayer2.inputScript.enabled = dataBasePlayer2.myAttackScript.enabled = dataBasePlayer2.myDamageScript.enabled = dataBasePlayer2.myMoveScript.enabled = dataBasePlayer2.myMana.enabled = true;
            CountDown.active = true;
            PauseScript.pauseCanBeMade = true;
        }
        else
        {
            dataBasePlayer1.myMoveScript.MoveExit();
            dataBasePlayer1.inputScript.UnBlock();
            dataBasePlayer2.myMoveScript.MoveExit();
            dataBasePlayer2.inputScript.UnBlock();
            dataBasePlayer1.inputScript.enabled = dataBasePlayer1.myAttackScript.enabled = dataBasePlayer1.myDamageScript.enabled = dataBasePlayer1.myMoveScript.enabled = dataBasePlayer1.myMana.enabled = false;
            dataBasePlayer2.inputScript.enabled = dataBasePlayer2.myAttackScript.enabled = dataBasePlayer2.myDamageScript.enabled = dataBasePlayer2.myMoveScript.enabled = dataBasePlayer2.myMana.enabled = false;
            CountDown.active = false;
            PauseScript.pauseCanBeMade = false;
        }
    }
    public void ResetMatch()
    {
        if (lastPlayerWon == 1)
        {
            dataBasePlayer2.myAnimationScript.Revive();
        }
        else if(lastPlayerWon == 2)
        {
            dataBasePlayer1.myAnimationScript.Revive();
        }
        camMov.enableCamMove = false;
        transform.position = camPos;
        CountDown.countDown = 99;
        dataBasePlayer1.player1.transform.position = dataBasePlayer1.myMoveScript.originalPos;
        dataBasePlayer2.player2.transform.position = dataBasePlayer2.myMoveScript.originalPos;
        dataBasePlayer1.enemyFighter.Redo();
        dataBasePlayer2.enemyFighter.Redo();
        dataBasePlayer1.myDamageScript.RestartBars();
        dataBasePlayer2.myDamageScript.RestartBars();
        PlayerDamageController.matchIsOn = true;
    }

    public IEnumerator WaitVictory(int player)
    {
        yield return new WaitForSeconds(1f);
        switch (player)
        {
            case 1:
                dataBasePlayer1.myAnimationScript.Victory();
                break;
            case 2:
                dataBasePlayer2.myAnimationScript.Victory();
                break;
        }
    }
}
