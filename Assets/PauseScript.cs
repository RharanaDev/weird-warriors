using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public string pauseButtonP1, pauseButtonP2, pauseKeyP1, pauseKeyP2;
    public float previousTime;
    public bool pause;
    public InstanceSaver dataBasePlayer1, dataBasePlayer2;
    public static bool pauseCanBeMade;
    public GameObject buttons, rightUI, leftUI;
    public string testing;
    public bool player1Paused, player2Paused;
    public static int whoStoppedFirst;
    void Update()
    {
        PauseInput();
    }
    public void PauseInput()
    {
        if (pauseCanBeMade)
        {
            if (!pause)
            {
                if (Input.GetButtonDown(pauseButtonP1) && !DramaticPause.isntRunning && !PlayerDamageController.clashEffecting || Input.GetButtonDown(pauseKeyP1) && !DramaticPause.isntRunning && !PlayerDamageController.clashEffecting)
                {
                    whoStoppedFirst = 1;
                    PauseGeneralEvent(true);
                    PlayerUniquePause(1, true);
                }
                else if (Input.GetButtonDown(pauseButtonP2) && !DramaticPause.isntRunning && !PlayerDamageController.clashEffecting || Input.GetButtonDown(pauseKeyP2) && !DramaticPause.isntRunning && !PlayerDamageController.clashEffecting)
                {
                    whoStoppedFirst = 2;
                    PauseGeneralEvent(true);
                    PlayerUniquePause(2, true);
                }
            }
            else
            {
                if(Input.GetButtonDown(pauseButtonP1) && !player1Paused || Input.GetButtonDown(pauseKeyP1) && !player1Paused)
                {
                    PlayerUniquePause(1, true);
                }
                else if (Input.GetButtonDown(pauseButtonP1) && player1Paused || Input.GetButtonDown(pauseKeyP1) && player1Paused) //UnpauseP1
                {
                    PlayerUniquePause(1, false);
                }
                else if (Input.GetButtonDown(pauseButtonP2) && !player2Paused || Input.GetButtonDown(pauseKeyP2) && !player2Paused)
                {
                    PlayerUniquePause(2, true);
                }
                else if (Input.GetButtonDown(pauseButtonP2) && player2Paused || Input.GetButtonDown(pauseKeyP2) && player2Paused) //UnpauseP2
                {
                    PlayerUniquePause(2, false);
                }
            }
        }
    }
    public void PauseGeneralEvent(bool activation)
    {
        if (activation)
        {
            pause = true;
            dataBasePlayer1.inputScript.enabled = dataBasePlayer1.myAttackScript.enabled = dataBasePlayer1.myDamageScript.enabled = dataBasePlayer1.myMoveScript.enabled = dataBasePlayer1.myMana.enabled = false;
            dataBasePlayer2.inputScript.enabled = dataBasePlayer2.myAttackScript.enabled = dataBasePlayer2.myDamageScript.enabled = dataBasePlayer2.myMoveScript.enabled = dataBasePlayer2.myMana.enabled = false;
            previousTime = Time.timeScale;
            Time.timeScale = 0;
            buttons.SetActive(true);
        }
        else
        {
            Time.timeScale = previousTime;
            dataBasePlayer1.inputScript.enabled = dataBasePlayer1.myAttackScript.enabled = dataBasePlayer1.myDamageScript.enabled = dataBasePlayer1.myMoveScript.enabled = dataBasePlayer1.myMana.enabled = true;
            dataBasePlayer2.inputScript.enabled = dataBasePlayer2.myAttackScript.enabled = dataBasePlayer2.myDamageScript.enabled = dataBasePlayer2.myMoveScript.enabled = dataBasePlayer2.myMana.enabled = true;
            pause = false;
            buttons.SetActive(false);
        }
    }

    public void PlayerUniquePause(int player, bool pause)
    {
        switch (player)
        {
            case 1:
                if (pause)
                {
                    rightUI.SetActive(true);
                    player1Paused = true;
                }
                else
                {
                    rightUI.SetActive(false);
                    player1Paused = false;
                    if (!player2Paused)
                    {
                        PauseGeneralEvent(false);
                    }
                }
                break;
            case 2:
                if (pause)
                {
                    leftUI.SetActive(true);
                    player2Paused = true;
                }
                else
                {
                    leftUI.SetActive(false);
                    player2Paused = false;
                    if (!player1Paused)
                    {
                        PauseGeneralEvent(false);
                    }
                }
                break;
        }
    }
}
