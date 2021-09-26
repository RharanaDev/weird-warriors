using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReceiver : MonoBehaviour
{
    public FightersData fighter;
    public InstanceSaver database;
    public int vectX, vectY;
    public bool dPadActive;
    public string horizontal, vertical, punch, kick, heavyPunch, heavyKick, block, dPadX, dPadY;
    public bool isMyControllerOn;
    public bool isInverted;
    public int dPad;
    public bool comboCanBeActive;
    public string currentString = "AAAAAA";
    public string comboCompare = "AAAAAA";
    public bool controlZeroCheck;
    public int prevPosX;
    // Start is called before the first frame update
    private void Awake()
    {
        switch (fighter.playerNumber)
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
        switch (fighter.playerNumber)
        {
            case 1:
                if (isMyControllerOn)
                {
                    horizontal = "HorizontalController1";
                    vertical = "VerticalController1";
                    punch = "PunchController1";
                    heavyPunch = "HeavyPunchController1";
                    kick = "KickController1";
                    heavyKick = "HeavyKickController1";
                    block = "BlockController1";
                    dPadX = "DPadX1";
                    dPadY = "DPadY1";
                    fighter.lookDirection = 1;
                }
                else if (!isMyControllerOn)
                {
                    horizontal = "Horizontal1";
                    vertical = "Vertical1";
                    punch = "Punch1";
                    heavyPunch = "HeavyPunch1";
                    kick = "Kick1";
                    heavyKick = "HeavyKick1";
                    block = "Block1";
                    fighter.lookDirection = 1;
                }
                break;
            case 2:
                if (isMyControllerOn)
                {
                    horizontal = "HorizontalController2";
                    vertical = "VerticalController2";
                    punch = "PunchController2";
                    heavyPunch = "HeavyPunchController2";
                    kick = "KickController2";
                    heavyKick = "HeavyKickController2";
                    block = "BlockController2";
                    dPadX = "DPadX2";
                    dPadY = "DPadY2";
                    fighter.lookDirection = -1;
                }
                else if (!isMyControllerOn)
                {
                    horizontal = "Horizontal2";
                    vertical = "Vertical2";
                    punch = "Punch2";
                    heavyPunch = "HeavyPunch2";
                    kick = "Kick2";
                    heavyKick = "HeavyKick2";
                    block = "Block2";
                    fighter.lookDirection = -1;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ActionListen();
    }
    private void ActionListen()
    {   
        int previousPosX, previousPosY;
        previousPosX = vectX;
        previousPosY = vectY;

        if (isMyControllerOn)
        {
            if (Mathf.RoundToInt(Input.GetAxisRaw(dPadX)) != 0|| Mathf.RoundToInt(Input.GetAxisRaw(dPadY)) != 0)
            {
                vectX = Mathf.RoundToInt(Input.GetAxisRaw(dPadX));
                vectY = Mathf.RoundToInt(Input.GetAxisRaw(dPadY));
            }
            else
            {
                vectX = Mathf.RoundToInt(Input.GetAxisRaw(horizontal));
                vectY = Mathf.RoundToInt(Input.GetAxisRaw(vertical));
            }
        }
        else
        {
            vectX = Mathf.RoundToInt(Input.GetAxisRaw(horizontal));
            vectY = Mathf.RoundToInt(Input.GetAxisRaw(vertical));
        }

        prevPosX = previousPosX;

        if (previousPosX != vectX || previousPosY != vectY || vectY == 1 && vectX != 0 || vectY == -1)
        {
            OnMove();
        }

        OnPunch();
        OnHeavyPunch();
        OnKick();
        OnHeavyKick();
        OnBlock();
        UnBlock();
    }

    void OnMove()
    {
        database.myMoveScript.vectX = vectX;
        database.myMoveScript.vectY = vectY;
        switch (vectX)
        {
            case 1:
                switch (vectY)
                {
                    case 1:                         //forwardup
                        ComboManager(3);
                        if (fighter.isInverted)
                        {
                            database.myMoveScript.FixedMoveExit();
                            database.myMoveScript.Crouch();
                        }
                        else if (!fighter.isInverted) 
                        {
                            database.myMoveScript.MoveExit();
                            database.myMoveScript.jumpDirection = 1;
                            database.myMoveScript.ForwardJump();
                        }
                        controlZeroCheck = false;
                        break;

                    case -1:                        //forwardown
                        ComboManager(9);
                        if (!fighter.isInverted)
                        {
                            database.myMoveScript.FixedMoveExit();
                            database.myMoveScript.Crouch();
                        }
                        else if (fighter.isInverted)
                        {
                            database.myMoveScript.MoveExit();
                            database.myMoveScript.jumpDirection = -1;
                            database.myMoveScript.ForwardJump();
                        }
                        controlZeroCheck = false;
                        break;

                    case 0:                         //forward
                        ComboManager(6);
                        database.myMoveScript.SideMovement();
                        controlZeroCheck = false;
                        break;
                }
                break;

            case -1:
                switch (vectY)
                {
                    case 1:                         //backwardup
                        ComboManager(1);
                        if (!fighter.isInverted)
                        {
                            database.myMoveScript.MoveExit();
                            database.myMoveScript.jumpDirection = -1;
                            database.myMoveScript.ForwardJump();
                        }
                        else if (fighter.isInverted)
                        {
                            database.myMoveScript.FixedMoveExit();
                            database.myMoveScript.Crouch();
                        }
                        controlZeroCheck = false;
                        break;

                    case -1:                        //backwarddown
                        ComboManager(7);
                        if (!fighter.isInverted)
                        {
                            database.myMoveScript.FixedMoveExit();
                            database.myMoveScript.Crouch();
                        }
                        else if (fighter.isInverted)
                        {
                            database.myMoveScript.MoveExit();
                            database.myMoveScript.jumpDirection = 1;
                            database.myMoveScript.ForwardJump();
                        }
                        controlZeroCheck = false;
                        break;

                    case 0:                         //backward
                        ComboManager(4);
                        database.myMoveScript.SideMovement();
                        controlZeroCheck = false;
                        break;
                }
                break;

            case 0:
                switch (vectY)
                {
                    case 1:                         //up
                        ComboManager(2);
                        if (!fighter.isInverted)
                        {
                            database.myMoveScript.FixedMoveExit();
                            database.myMoveScript.JumpSelection();
                        }
                        else if (fighter.isInverted)
                        {
                            database.myMoveScript.FixedMoveExit();
                            database.myMoveScript.Crouch();
                        }
                        controlZeroCheck = false;
                        break;
                    case -1:                         //down
                        ComboManager(8);
                        if (!fighter.isInverted)
                        {
                            database.myMoveScript.FixedMoveExit();
                            database.myMoveScript.Crouch();
                        }
                        else if (fighter.isInverted)
                        {
                            database.myMoveScript.MoveExit();
                            database.myMoveScript.JumpSelection();
                        }
                        controlZeroCheck = false;
                        break;
                    case 0:                          //nothing
                        if (prevPosX == 1 && database.myFighter.lookDirection == 1|| prevPosX == -1 && database.myFighter.lookDirection == -1 || prevPosX == 1 && database.myFighter.lookDirection == -1 || prevPosX == -1 && database.myFighter.lookDirection == 1)
                        {
                            ComboManager(5);
                        }
                        database.myMoveScript.MoveExit();
                        break;
                }
                break;
        }
    }
    void OnBlock()
    {
        if(Input.GetAxis(block) != 0 && isMyControllerOn)
        {
            database.myMoveScript.Block();
        }
        if (Input.GetButtonDown(block) && !isMyControllerOn)
        {
            database.myMoveScript.Block();
        }
    }
    public void UnBlock()
    {
        if (Input.GetButtonUp(block) && !isMyControllerOn && fighter.isBlock)
        {
            database.myMoveScript.UnBlock();
        }
        if (Input.GetAxis(block) == 0 && isMyControllerOn && fighter.isBlock)
        {
            database.myMoveScript.UnBlock();
        }
    }
    void OnPunch()
    {
        if (Input.GetButtonDown(punch))
        {
            database.myAttackScript.Punch();
        }
    }
    void OnHeavyPunch()
    {
        if (Input.GetButtonDown(heavyPunch))
        {
            database.myAttackScript.HeavyPunch();
        }
    }
    void OnKick()
    {
        if (Input.GetButtonDown(kick))
        {
            database.myAttackScript.Kick();
        }
    }
    void OnHeavyKick()
    {
        if (Input.GetButtonDown(heavyKick))
        {
            database.myAttackScript.HeavyKick();
        }
    }
    void ComboManager(int num)
    {
        if(dPad != num && database.myFighter.canAttack)
        {
            dPad = num;
            comboCompare = currentString;
            currentString = comboCompare.Substring(1);
            currentString += dPad;
            StopCoroutine("ComboTimeMustFade");
            StartCoroutine("ComboTimeMustFade");
        }

    }
    IEnumerator ComboTimeMustFade()
    {
        comboCanBeActive = true;
        yield return new WaitForSecondsRealtime(0.2f);
        comboCanBeActive = false;
    }
}
