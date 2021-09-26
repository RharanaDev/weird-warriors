using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public PointScript points;
    public InstanceSaver dataBase;
    public FightersData fighter;
    public ComboCounter comboCo;
    public int punchCount, heavyPunchCount, kickCount, heavyKickCount;
    public bool hasHit, hasAIHit;
    int comboMatter;
    public ComboReaction comboCheck;
    // Start is called before the first frame update
    void Start()
    {
        punchCount = heavyPunchCount = kickCount = heavyKickCount = 0;
        comboCo = GameObject.Find("ComboCounter" + fighter.playerNumber).GetComponent<ComboCounter>();
        points = GameObject.Find("Points" + fighter.playerNumber).GetComponent<PointScript>();
    }
    public void ComboTracker()
    {
        //manage.SoundProyect(false);
        comboMatter++;
        StopCoroutine("ComboCountdown");
        StartCoroutine("ComboCountdown");
        if (comboMatter > 1)
        {
            if (comboCo.isOutScreen)
            {
                comboCo.SpriteChange(comboMatter);
                comboCo.isOutScreen = false;
                comboCo.isEntering = true;
                comboCo.targetAcquired = false;
                StopCoroutine("UntilOnScreen");
            }
            else if (comboCo.isOnScreen)
            {
                comboCo.SpriteChange(comboMatter);
                StopCoroutine("UntilOnScreen");
            }
            else if (comboCo.isRetiring)
            {
                comboCo.isRetiring = false;
                comboCo.isEntering = true;
                comboCo.targetAcquired = false;
                StopCoroutine("UntilOnScreen");
                StartCoroutine("UntilOnScreen");
            }
            else if (comboCo.isEntering)
            {
                StopCoroutine("UntilOnScreen");
                StartCoroutine("UntilOnScreen");
            }
        }
    }
    public IEnumerator UntilOnScreen()
    {
        yield return new WaitUntil(() => comboCo.isOnScreen == true);
        comboCo.SpriteChange(comboMatter);
    }
    public IEnumerator ComboCountdown()
    {
        yield return new WaitForSeconds(1);
        points.puntuation += comboMatter * 1000;
        points.ApplyPuntuation();
        comboMatter = 0;
        if (comboCo.isOnScreen)
        {
            comboCo.isOnScreen = false;
            comboCo.isRetiring = true;
            comboCo.targetAcquired = false;
            comboCo.ChangeOfCombo();
        }
    }
    // Update is called once per frame
    public void Punch()
    {
        hasHit = false;
        if (fighter.canAttack == true && fighter.isCrouch == false && fighter.isGrounded == true && fighter.isStunned == false)
        {
            dataBase.myAnimationScript.debuggerJustOneHit = 0;
            if (comboCheck.CheckForPunchCombo() == true)
            {
                return;
            }
            else
            {
                punchCount++;
                dataBase.myAnimationScript.Punch_1();
                dataBase.myMoveScript.MoveExit();
                fighter.canAttack = false;
                //LightPunch
            }
        }
        else if (fighter.isCrouch == false && fighter.isGrounded == false && fighter.isStunned == false && fighter.isForwardJumping && fighter.canAirHit)
        {
            fighter.canAirHit = false;
            dataBase.myAnimationScript.ForwardJumpPunch();
            //ForwardJumpPunch
        }
        else if (fighter.canAttack == true && fighter.isCrouch == true && fighter.isGrounded == true && fighter.isStunned == false)
        {
            dataBase.myAnimationScript.debuggerJustOneHit = 0;
            if (comboCheck.CheckForPunchCombo() == true)
            {
                return;
            }
            dataBase.myAnimationScript.CrouchPunch_1();
        }

    }
    public void HeavyPunch()
    {
        hasHit = false;
        if (fighter.canAttack == true && fighter.isCrouch == false && fighter.isGrounded == true && fighter.isStunned == false)
        {
            dataBase.myAnimationScript.debuggerJustOneHit = 0;
            if (comboCheck.CheckForHardPunchCombo() == true)
            {
                return;
            }
            else
            {
                heavyPunchCount++;
                dataBase.myAnimationScript.Punch_2();
                fighter.canAttack = false;
                dataBase.myMoveScript.MoveExit();
                //HeavyPunch
            }
        }
        else if (fighter.isCrouch == false && fighter.isGrounded == false && fighter.isStunned == false && fighter.isForwardJumping && fighter.canAirHit)
        {
            fighter.canAirHit = false;
            dataBase.myAnimationScript.ForwardJumpPunch();
            //ForwardJumpPunch
        }
        else if (fighter.canAttack == true && fighter.isCrouch == true && fighter.isGrounded == true && fighter.isStunned == false)
        {
            dataBase.myAnimationScript.debuggerJustOneHit = 0;
            if (comboCheck.CheckForHardPunchCombo() == true)
            {
                return;
            }
            else
            {
                dataBase.myAnimationScript.CrouchPunch_1();
                fighter.canAttack = false;
            }
        }
    }
    public void Kick()
    {
        hasHit = false;
        if (fighter.canAttack == true && fighter.isCrouch == false && fighter.isGrounded == true && fighter.isStunned == false)
        {
            dataBase.myAnimationScript.debuggerJustOneHit = 0;
            if (comboCheck.CheckForKickCombo() == true)
            {
                return;
            }
            else
            {
                kickCount++;
                dataBase.myAnimationScript.Kick_1();
                fighter.canAttack = false;
                dataBase.myMoveScript.MoveExit();
                //LightKick
            }
        }
        else if (fighter.isCrouch == false && fighter.isGrounded == false && fighter.isStunned == false && fighter.isForwardJumping && fighter.canAirHit)
        {
            fighter.canAirHit = false;
            dataBase.myAnimationScript.ForwardJumpKick();
            //ForwardJumpKick
        }
        else if (fighter.canAttack == true && fighter.isCrouch == true && fighter.isGrounded == true && fighter.isStunned == false)
        {
            dataBase.myAnimationScript.debuggerJustOneHit = 0;
            if (comboCheck.CheckForKickCombo() == true)
            {
                return;
            }
            else
            {
                dataBase.myAnimationScript.CrouchKick_1();
                fighter.canAttack = false;
            }
        }
    }
    public void HeavyKick()
    {

        if (fighter.canAttack == true && fighter.isCrouch == false && fighter.isGrounded == true && fighter.isStunned == false)
        {
            dataBase.myAnimationScript.debuggerJustOneHit = 0;
            if (comboCheck.CheckForHardKickCombo() == true)
            {
                return;
            }
            else
            {
                dataBase.myAnimationScript.Kick_2();
                fighter.canAttack = false;
                dataBase.myMoveScript.MoveExit();
            }
        }

        else if (fighter.canAttack == true && fighter.isCrouch == true && fighter.isGrounded == true && fighter.isStunned == false)
        {
            dataBase.myAnimationScript.debuggerJustOneHit = 0;
            if (comboCheck.CheckForHardKickCombo() == true)
            {
                return;
            }
            else
            {
                dataBase.myAnimationScript.CrouchKick_1();
                fighter.canAttack = false;
            }
        }

        else if (fighter.isCrouch == false && fighter.isGrounded == false && fighter.isStunned == false && fighter.isForwardJumping && fighter.canAirHit)
        {
            fighter.canAirHit = false;
            dataBase.myAnimationScript.ForwardJumpKick();
            //ForwardJumpKick
        }
    }
    public void ResetAll()
    {
        punchCount = heavyPunchCount = kickCount = heavyKickCount = 0;
    }
}
