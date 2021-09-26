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
            switch (firstDigit)
            {
                case 1:
                    spriteDigit.sprite = spriteList[1];
                    break;
                case 2:
                    spriteDigit.sprite = spriteList[2];
                    break;
                case 3:
                    spriteDigit.sprite = spriteList[3];
                    break;
                case 4:
                    spriteDigit.sprite = spriteList[4];
                    break;
                case 5:
                    spriteDigit.sprite = spriteList[5];
                    break;
                case 6:
                    spriteDigit.sprite = spriteList[6];
                    break;
                case 7:
                    spriteDigit.sprite = spriteList[7];
                    break;
                case 8:
                    spriteDigit.sprite = spriteList[8];
                    break;
                case 9:
                    spriteDigit.sprite = spriteList[9];
                    break;
                case 0:
                    spriteDigit.sprite = spriteList[0];
                    break;
            }

            switch (secondDigit)
            {
                case 1:
                    spriteDigit2.sprite = spriteList[1];
                    break;
                case 2:
                    spriteDigit2.sprite = spriteList[2];
                    break;
                case 3:
                    spriteDigit2.sprite = spriteList[3];
                    break;
                case 4:
                    spriteDigit2.sprite = spriteList[4];
                    break;
                case 5:
                    spriteDigit2.sprite = spriteList[5];
                    break;
                case 6:
                    spriteDigit2.sprite = spriteList[6];
                    break;
                case 7:
                    spriteDigit2.sprite = spriteList[7];
                    break;
                case 8:
                    spriteDigit2.sprite = spriteList[8];
                    break;
                case 9:
                    spriteDigit2.sprite = spriteList[9];
                    break;
                case 0:
                    spriteDigit2.sprite = spriteList[10];
                    break;
            }

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
            Debug.Log("TimeIsOver"); //
            active = false;
            //enabled = false;
        }

    }
}