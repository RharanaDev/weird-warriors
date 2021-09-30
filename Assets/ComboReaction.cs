using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboReaction : MonoBehaviour
{
    public InstanceSaver dataBase;
    string requiredString, securityString;
    public bool cooldowning;
    int dontOverUse;
    public bool stringAntique;
    public bool CheckForPunchCombo()
    {
        bool result;
        switch (dataBase.myFighter.ID)
        {
            case 0: //Zhimago
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "896" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "874" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.ComboSpecial();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse >= 3)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else
                {
                    result = false;
                }
                break;

            case 1: //WakanTanka
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "896" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4 || requiredString == "874" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4)
                {
                    result = true;
                    dataBase.myAnimationScript.ComboSpecial();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse >= 3)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else
                {
                    result = false;
                }
                break;

            case 2: //Utah
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "698" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 2 || requiredString == "478" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 2 || requiredString == "987" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 2 || requiredString == "789" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 2)
                {
                    result = true;
                    dataBase.myAnimationScript.ComboSpecial();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                }
                else if (requiredString == "896" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "874" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)                
                {
                    result = true;
                    dataBase.myAnim.SetTrigger("SuperCombo");
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                }
                else
                {
                    result = false;
                }
                break;

            case 3: //Gen Tai
                requiredString = dataBase.inputScript.currentString.Substring(3);
                securityString = dataBase.inputScript.currentString.Substring(4);
                if (securityString == "69" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || securityString == "47"
                    && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "659" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || requiredString == "457" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnim.SetTrigger("SuperCombo");
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse == 1)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else
                {
                    result = false;
                }
                break;

            case 4: //Hemogoblin
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "896" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning|| requiredString == "874" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnim.SetTrigger("SuperCombo");
                    dataBase.inputScript.currentString = "AAAAAA";
                }
                else if (requiredString == "456" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 2 || requiredString == "654" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 2)
                {
                    result = true;
                    dataBase.myAnimationScript.StunCombo();
                    dataBase.inputScript.currentString = "AAAAAA";
                }
                else
                {
                    result = false;
                }
                break;

            default:
                result = false;
                break;
        }

        return result;
    }

    public bool CheckForKickCombo()
    {
        bool result;
        switch (dataBase.myFighter.ID)
        {
            case 0: //Zhimago
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "896" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 2 || requiredString == "874" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 2)
                {
                    result = true;
                    dataBase.myAnimationScript.StunCombo();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse >= 3)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else
                {
                    result = false;
                }
                break;

            case 1: //WakanTanka
                requiredString = dataBase.inputScript.currentString.Substring(3);
                securityString = dataBase.inputScript.currentString.Substring(2);
                if (requiredString == "454" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning|| requiredString == "656" 
                    && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "4545" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "6565" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.StunCombo();
                    dataBase.inputScript.currentString = "AAAAAA";
                }
                else
                {
                    result = false;
                }
                break;



            case 4: //Hemogoblin
                requiredString = dataBase.inputScript.currentString.Substring(3);
                securityString = dataBase.inputScript.currentString.Substring(2);
                if (requiredString == "896" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4 || requiredString == "874" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4)
                {
                    result = true;
                    dataBase.myAnimationScript.ComboSpecial();
                    dataBase.inputScript.currentString = "AAAAAA";
                }
                else
                {
                    result = false;
                }
                break;

            default:
                result = false;
                break;
        }

        return result;
    }

    public bool CheckForHardPunchCombo()
    {
        bool result;
        switch (dataBase.myFighter.ID)
        {
            case 0: //Zhimago
                requiredString = dataBase.inputScript.currentString.Substring(4);
                securityString = dataBase.inputScript.currentString.Substring(3);
                if (securityString == "456" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || securityString == "654" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.Punch_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse >= 1)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else if (securityString == "896" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4 || securityString == "874" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4)
                {
                    result = true;
                    dataBase.myAnim.SetTrigger("SuperCombo");
                    dataBase.inputScript.currentString = "AAAAAA";
                }
                else
                {
                    result = false;
                }
                break;

            case 1: //WakanTanka
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "456" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "654" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.Punch_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse >= 3)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else
                {
                    result = false;
                }
                break;

            case 2: //Utah
                requiredString = dataBase.inputScript.currentString.Substring(3);
                securityString = dataBase.inputScript.currentString.Substring(2);
                if (requiredString == "454" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "656"
                    && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "4545" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "6565" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.Punch_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse == 1)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }

                else
                {
                    result = false;
                }
                break;

            case 3: //GenTai
                requiredString = dataBase.inputScript.currentString.Substring(3);
                securityString = dataBase.inputScript.currentString.Substring(2);
                if (requiredString == "454" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "656"
                    && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "4545" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "6565" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.Punch_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse == 1)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else if (requiredString == "896" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 3 || requiredString == "874" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars >= 3)
                {
                    result = true;
                    dataBase.myAnimationScript.StunCombo();
                    dataBase.inputScript.currentString = "AAAAAA";
                }
                else
                {
                    result = false;
                }
                break;

            default:
                result = false;
                break;

            case 4: //Hemoglobin
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "456" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "654" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.Punch_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse >= 3)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else
                {
                    result = false;
                }
                break;
        }

        return result;
    }
    public bool CheckForHardKickCombo()
    {
        bool result;
        switch (dataBase.myFighter.ID)
        {
            case 0: //Zhimago
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "456" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "654" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    dataBase.myAnimationScript.Kick_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    result = true;
                }
                else
                {
                    result = false;
                }
                break;

            case 1: //WakanTanka
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "456" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "654" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.Kick_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse >= 3)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else
                {
                    result = false;
                }
                break;

            case 2: //Utah

                requiredString = dataBase.inputScript.currentString.Substring(3);
                securityString = dataBase.inputScript.currentString.Substring(2);
                if (requiredString == "454" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "656"
                    && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "4545" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "6565" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.Kick_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse == 1)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else if (requiredString == "456" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4 || requiredString == "654" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4)
                {
                    result = true;
                    dataBase.myAnimationScript.StunCombo();
                    dataBase.inputScript.currentString = "AAAAAA";
                }
                else
                {
                    result = false;
                }
                break;
            default:
                result = false;
                break;

            case 3: //GenTai
                requiredString = dataBase.inputScript.currentString.Substring(3);
                securityString = dataBase.inputScript.currentString.Substring(2);
                if (requiredString == "454" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "656"
                    && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "4545" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning
                    || securityString == "6565" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    result = true;
                    dataBase.myAnimationScript.Kick_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    dontOverUse++;
                    if (dontOverUse == 1)
                    {
                        StartCoroutine(Cooldown(1.5f));
                    }
                }
                else if (requiredString == "456" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4|| requiredString == "654" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning && dataBase.myMana.manaBars == 4)
                {
                    dataBase.myAnimationScript.ComboSpecial();
                    dataBase.inputScript.currentString = "AAAAAA";
                    result = true;
                }
                else
                {
                    result = false;
                }
                break;

            case 4: //Hemogoblin
                requiredString = dataBase.inputScript.currentString.Substring(3);
                if (requiredString == "456" && dataBase.myFighter.lookDirection == 1 && dataBase.inputScript.comboCanBeActive && !cooldowning || requiredString == "654" && dataBase.myFighter.lookDirection == -1 && dataBase.inputScript.comboCanBeActive && !cooldowning)
                {
                    dataBase.myAnimationScript.Kick_3();
                    dataBase.inputScript.currentString = "AAAAAA";
                    result = true;
                }
                else
                {
                    result = false;
                }
                break;
        }
        return result;
    }
    public IEnumerator Cooldown(float coolio)
    {
        dontOverUse = 0;
        cooldowning = true;
        yield return new WaitForSeconds(coolio);
        cooldowning = false;
    }
}
