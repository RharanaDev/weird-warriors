using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public InstanceSaver dataBase;
    public int number;
    public bool isMyControllerOn;
    public string horizontal, vertical, punch, kick;
    int vectX, vectY;
    public Transform[] buttons, comboCabins;
    public Transform[] moveSpace;
    public Transform myMarker;
    int previousPosX, previousPosY;
    public int mode;
    public int buttonPos;
    public Sprite[] controls;
    public SpriteRenderer imageControls, imageChamp;
    public Sprite[] fighters;
    public Sprite[] markerOptions;
    public ComboCabinScript glowUp;
    public PauseScript pause;

    private void Awake()
    {
        switch (number)
        {
            case 1:
                isMyControllerOn = GameOptions.player1Controller;
                break;
            case 2:
                isMyControllerOn = GameOptions.player2Controller;
                break;
        }
    }
    void Start()
    {
        switch (number)
        {
            case 1:
                if (isMyControllerOn)
                {
                    horizontal = "HorizontalController1";
                    vertical = "VerticalController1";
                    punch = "PunchController1";
                    kick = "KickController1";
                }
                else if (!isMyControllerOn)
                {
                    horizontal = "Horizontal1";
                    vertical = "Vertical1";
                    punch = "Punch1";
                    kick = "Kick1";
                }
                break;

            case 2:
                if (isMyControllerOn)
                {
                    horizontal = "HorizontalController2";
                    vertical = "VerticalController2";
                    punch = "PunchController2";
                    kick = "KickController2";
                }
                else if (!isMyControllerOn)
                {
                    horizontal = "Horizontal2";
                    vertical = "Vertical2";
                    punch = "Punch2";
                    kick = "Kick2";
                }
                break;
        }
    }
    private void OnEnable()
    {
        imageChamp.sprite = fighters[dataBase.myFighter.ID];
        if(number != PauseScript.whoStoppedFirst)
        {
            mode = 1;
            buttonPos = 0;
            moveSpace = comboCabins;
            myMarker.GetComponent<SpriteRenderer>().sprite = markerOptions[1];
            myMarker.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
        }
        else
        {
            mode = 0;
            buttonPos = 0;
            moveSpace = buttons;
            myMarker.GetComponent<SpriteRenderer>().sprite = markerOptions[0];
            myMarker.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
    void Update()
    {
        previousPosX = vectX;
        previousPosY = vectY;
        vectX = Mathf.RoundToInt(Input.GetAxisRaw(horizontal));
        vectY = Mathf.RoundToInt(Input.GetAxisRaw(vertical));
        ButtonsMovility();
        OnConfirm();
    }
    void ButtonsMovility()
    {
        if (vectY == -1 && vectX == 0 && previousPosY != -1)
        {
            if (moveSpace.Length - 1 > buttonPos)
            {
                buttonPos++;
            }
            else
            {
                buttonPos = 0;
            }
            CursorMoved();
        }
        else if (vectY == 1 && vectX == 0 && previousPosY != 1)
        {
            if (buttonPos > 0)
            {
                buttonPos--;
            }
            else
            {
                buttonPos = moveSpace.Length - 1;
            }
            CursorMoved();
        }
        myMarker.position = moveSpace[buttonPos].position;
    }
    void OnConfirm()
    {
        if (Input.GetButtonDown(punch))
        {
            if (mode == 0)
            {
                mode = buttonPos + 1;
                switch (mode)
                {
                    case 1:  //On Controls
                        myMarker.GetComponent<SpriteRenderer>().sprite = markerOptions[1];
                        myMarker.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
                        buttonPos = 0;
                        moveSpace = comboCabins;
                        CursorMoved();
                        break;
                    case 2:  //On Sounds
                        mode = 0;
                        break;
                    case 3:  //On  Menu
                        SceneManager.LoadScene(0);
                        break;
                    case 4:  //On  Back
                        pause.PlayerUniquePause(number, false);
                        break;
                }
            }
        }
        else if (Input.GetButtonDown(kick))
        {
            if (mode != 0 && number == PauseScript.whoStoppedFirst)
            {
                buttonPos = 0;
                moveSpace = buttons;
                myMarker.GetComponent<SpriteRenderer>().sprite = markerOptions[0];
                myMarker.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 100);
                mode = 0;
                glowUp.SelectNewCabin(-1);
            }
            else if(mode != 0 && number != PauseScript.whoStoppedFirst && !pause.player1Paused && number == 2 || mode != 0 && number != PauseScript.whoStoppedFirst && !pause.player2Paused && number == 1)
            {
                buttonPos = 0;
                moveSpace = buttons;
                myMarker.GetComponent<SpriteRenderer>().sprite = markerOptions[0];
                myMarker.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 100);
                mode = 0;
                glowUp.SelectNewCabin(-1);
            }
        }
    }
    void CursorMoved()
    {
        if (mode == 1)
        {
            glowUp.SelectNewCabin(buttonPos);
        }
    }

}
