using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public static float countDown = 99;
    int firstDigit, secondDigit;
    int countdowner;
    public SpriteRenderer spriteDigit, spriteDigit2;
    public Sprite[] spriteList = new Sprite[15];
    public bool aboutEnd = false;
    public Vector2[] transformRequired;
    bool gameEnded;
    public static bool active;
    public MatchControlScript match;
    public InstanceSaver dataBase1, dataBase2;
    public GameObject timesUp;

    private void Update()
    {
        if (countDown > 0 && active)
        {
            countDown -= Time.deltaTime;
        }
        else if (countDown < 0)
        {
            countDown = 0;
        }

        countdowner = Mathf.RoundToInt(countDown);

        if (countdowner == 9)
        {
            transform.localPosition = transformRequired[0];
        }

        else if (countdowner <= 5)
        {
            aboutEnd = true;
            transform.localPosition = transformRequired[1];
        }

        GetDigits();
        SelectSprites();
    }
    public void GetDigits()
    {
        firstDigit = Mathf.FloorToInt(countdowner / 10);
        secondDigit = countdowner - (firstDigit * 10);

    }
    public void SelectSprites()
    {
        if (aboutEnd == false)
        {
            spriteDigit.sprite = spriteList[firstDigit];
            spriteDigit2.sprite = spriteList[secondDigit];
        }

        else if (aboutEnd) 
        {
            switch (secondDigit)
            {
                case 1:
                    spriteDigit2.sprite = spriteList[11];
                    break;
                case 2:
                    spriteDigit2.sprite = spriteList[12];
                    break;
                case 3:
                    spriteDigit2.sprite = spriteList[13];
                    break;
                case 4:
                    spriteDigit2.sprite = spriteList[14];
                    break;
                case 5:
                    spriteDigit2.sprite = spriteList[15];
                    break;
                case 0:
                    spriteDigit2.sprite = spriteList[0];
                    StopGame();
                    break;
            }
        }

    }
    public void StopGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            timesUp.SetActive(true);
            match.OnWin(CheckForLife(), true);
            active = false;
        }
    }
    public int CheckForLife()
    {
        if (dataBase1.myFighter.currentLife > dataBase2.myFighter.currentLife)
		{
            return 2;
        }
        else if (dataBase2.myFighter.currentLife > dataBase1.myFighter.currentLife)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}