using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    public FightersData fighter;
    public InstanceSaver dataBase;
    public ManaBar manaBar;
    public bool isMyControllerOn;
    public string charge;
    public int manaBars;
    public Animator lightning;
    bool debuggo;
    bool manaSumming;
    bool isCharging;
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
        manaBar = GameObject.Find("UIPref/ManaBar" + fighter.playerNumber + "/Mana").GetComponent<ManaBar>();
        lightning = GameObject.Find("UIPref/Lightning" + fighter.playerNumber).GetComponent<Animator>();
        switch (fighter.playerNumber)
        {
            case 1:
                if (GameOptions.player1Controller)
                {
                    charge = "ChargeController1";
                }
                else if (!GameOptions.player1Controller)
                {
                    charge = "Charge1";
                }
                break;
            case 2:
                if (GameOptions.player2Controller)
                {
                    charge = "ChargeController2";
                }
                else if (!GameOptions.player2Controller)
                {
                    charge = "Charge2";
                }
                break;
        }
    }

    void Update()
    {
        ChargeInput();

        if (manaSumming && manaBar.manaFloat < Mathf.Clamp(26.5f * (manaBars + 1),0,100))
        {
            manaBar.manaFloat += 25 * 4 * Time.deltaTime;
        }
        else if(manaSumming && manaBar.manaFloat >= Mathf.Clamp(26.5f * (manaBars + 1), 0, 100))
        {
            manaSumming = false;
            manaBar.manaFloat = Mathf.Clamp(26.5f * (manaBars + 1), 0, 100);
            manaBars++;
            dataBase.myAnim.SetInteger("ActualBars", manaBars);
            isCharging = false;
        }
        if (manaBars >= 2 && fighter.isBlock)
        {
            CanDodgeCombo(true);
        }
        else
        {
            CanDodgeCombo(false);
        }
    }

    public void LessenMana(int mana)
    {
        manaBars -= mana;
        manaBar.manaFloat = Mathf.Clamp(26.5f * manaBars, 0, 100);
        manaBar.LessenSomeMana(Mathf.Clamp(26.5f * mana, 0, 100));
        dataBase.myAnim.SetInteger("ActualBars", manaBars);
    }
    public void LessenAllMana()
    {
        manaBars = 0;
        manaBar.LessenSomeMana(manaBar.manaFloat);
        dataBase.myAnim.SetInteger("ActualBars", manaBars);
        manaBar.manaFloat = 0;
    }

    public void BeginCharging()
    {
        if (fighter.isCrouch == false && fighter.isStunned == false && fighter.isGrounded == true && fighter.direction == 0 && manaBars < 4 && !isCharging && fighter.canAttack) 
        {
            isCharging = true;
            dataBase.myAnim.SetInteger("BarsCharged", manaBars);
            dataBase.myAnimationScript.Charge();
        }
    }
    void ChargeInput()
    {
        if (Input.GetButtonDown(charge) && !isMyControllerOn)
        {
            BeginCharging();
        }

        if (Input.GetAxis(charge) != 0 && isMyControllerOn && !debuggo)
        {
            BeginCharging();
            debuggo = true;
        }

        else if (debuggo && Input.GetAxis(charge) == 0 && isMyControllerOn)
        {
            debuggo = false;
        }
    }
    public void OnManaSum()
    {
        manaSumming = true;
        dataBase.myAnim.SetInteger("BarsCharged", manaBars);
    }
    void CanDodgeCombo(bool canDodge)
    {
        fighter.canWaitForDodgeCombo = canDodge;
    }
}

