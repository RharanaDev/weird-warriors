using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    // Start is called before the first frame update
    public InstanceSaver database;
    public FightersData fighter;
    public Transform otherPlayer, mypos;
    public float healthyDistance, actualDistance, attackDistance;
    bool canMoveFreely, canDoNewCombo;
    public bool attackNow, debugKey, onAttackDistance, checkAttackDistance;
    public int inputX, inputY, comboMoment, comboLenght, counter, comboToDo;
    // Update is called once per frame
    void Start()
    {
        otherPlayer = database.enemy.transform;
        mypos = database.playerMe.transform;
        canMoveFreely = true;
        canDoNewCombo = true;
        fighter.lookDirection = -1;
        StartCoroutine(Dod());
    }
    void Update()
    {
        CheckEnvironment();
    }
    void CheckEnvironment()
    {
        actualDistance = otherPlayer.position.x - mypos.position.x;

        if (Mathf.Abs(actualDistance) > healthyDistance && canMoveFreely && onAttackDistance == false)
        {
            inputX = fighter.lookDirection;
            database.myMoveScript.vectX = inputX;
            database.myMoveScript.SideMovement();
        }
        else if(Mathf.Abs(actualDistance) <= healthyDistance && canMoveFreely && onAttackDistance == false)
        {
            canMoveFreely = false;
            inputX = 0;
            database.myMoveScript.vectX = inputX;
            database.myMoveScript.MoveExit();
            StartCoroutine(JustMoved());
        }
        if (checkAttackDistance && Mathf.Abs(actualDistance) <= attackDistance)
        {
            onAttackDistance = true;
        }
        else if (checkAttackDistance && Mathf.Abs(actualDistance) > attackDistance)
        {
            onAttackDistance = false;
            checkAttackDistance = false;
        }
    }
    IEnumerator Dod()
    {
        yield return new WaitForSeconds(4f);
        ComboSet();
    }
    public IEnumerator JustMoved()
    {
        yield return new WaitForSeconds(0.5f);
        checkAttackDistance = true;
        canMoveFreely = true;
    }

    public void ComboSet()
    {
        if (canDoNewCombo)
        {
            int willCombo = Random.Range(1, 5);

            if (willCombo > 3)
            {
                int wichCombo = Random.Range(1, 4);
                comboToDo = wichCombo;
                canDoNewCombo = false;
                ComboChain();
            }
            else
            {
                comboToDo = 0;
                BasicAttack();
            }
        }
    }
    public void BasicAttack()
    {
        database.myAttackScript.Punch();
        StartCoroutine(BasicCoolDown());
    }
    IEnumerator BasicCoolDown()
    {
        yield return new WaitForSeconds(2f);
        ComboSet();
    }

    public IEnumerator WaitUntilHit()
    {
        yield return new WaitUntil(() => fighter.canAIttack == true);
        fighter.canAIttack = false;
        ComboChain();
    }

    public void ComboChain()
    {
        if (database.myAttackScript.hasAIHit)
        {
            comboMoment++;
            database.myAttackScript.hasAIHit = false;
        }
        else if(database.myAttackScript.hasAIHit == false)
        {
            comboMoment = 0;
        }

        switch (comboToDo)
        {
            case 1:
                switch (comboMoment)
                {
                    case 0:
                        StartCoroutine(WaitUntilHit());
                        database.myAttackScript.Punch();
                        break;
                    case 1:
                        StartCoroutine(WaitUntilHit());
                        database.myAttackScript.Punch();
                        break;
                    case 2:
                        database.myAttackScript.HeavyPunch();
                        comboMoment = 0;
                        break;
                }
                break;
            case 2:
                switch (comboMoment)
                {
                    case 0:
                        StartCoroutine(WaitUntilHit());
                        database.myAttackScript.Kick();
                        break;
                    case 1:
                        StartCoroutine(WaitUntilHit());
                        database.myAttackScript.Kick();
                        break;
                    case 2:
                        database.myAttackScript.HeavyKick();
                        comboMoment = 0;
                        break;
                }
                break;
            case 3:
                switch (comboMoment)
                {
                    case 0:
                        StartCoroutine(WaitUntilHit());
                        database.myAttackScript.Punch();
                        break;
                    case 1:
                        StartCoroutine(WaitUntilHit());
                        database.myAttackScript.Kick();
                        break;
                    case 2:
                        StartCoroutine(WaitUntilHit());
                        database.myAttackScript.HeavyPunch();
                        break;
                    case 3:
                        database.myAttackScript.HeavyKick();
                        comboMoment = 0;
                        break;
                }
                break;
        }
    }
}
