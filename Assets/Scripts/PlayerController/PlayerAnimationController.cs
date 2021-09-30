using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XInputDotNetPure;

public class PlayerAnimationController : MonoBehaviour
{
    public InstanceSaver dataBase;
    public FightersData fighter;
    public SpecialEffects sfx;
    private Animator anim;
    public ShootProjectile shoot;
    public Transform obj;
    public Cinetics cinetics;
    //public PlayerIndex playerIndex;
    bool canDust = true;
    public bool lastWalked = false;
    bool myGamePadIsConnected;
    public int lastEffectType;
    public float lastX, lastY;
    public int lastDamage;
    public GameObject Ray;
    public GameObject Missile;
    public TiltScript tilt;
    public bool collisionIgnored;
    public bool hasAdvanced;
    public int debuggerJustOneHit;
    public bool debuggerBoolHit;
    public int catSituationLeo;
    public static int actives;
    public static bool timeToFade, timeToWin;

    #region StringID
    [HideInInspector] public int movingID = Animator.StringToHash("Moving");
    [HideInInspector] public int directionID = Animator.StringToHash("Direction");
    [HideInInspector] public int fullPowerID = Animator.StringToHash("FullPower");
    [HideInInspector] public int chargeID = Animator.StringToHash("Charge");
    [HideInInspector] public int punchCombo1ID = Animator.StringToHash("PunchCombo1");
    [HideInInspector] public int punchCombo2ID = Animator.StringToHash("PunchCombo2");
    [HideInInspector] public int punchCombo3ID = Animator.StringToHash("PunchCombo3");
    [HideInInspector] public int kickCombo1ID = Animator.StringToHash("KickCombo1");
    [HideInInspector] public int kickCombo2ID = Animator.StringToHash("KickCombo2");
    [HideInInspector] public int kickCombo3ID = Animator.StringToHash("KickCombo3");
    [HideInInspector] public int destroyerComboID = Animator.StringToHash("DestroyerCombo");
    [HideInInspector] public int crouchPunchID = Animator.StringToHash("CrouchPunchCombo1");
    [HideInInspector] public int crouchKickID = Animator.StringToHash("CrouchKickCombo1");
    [HideInInspector] public int hit1ID = Animator.StringToHash("Hit1");
    [HideInInspector] public int hit2ID = Animator.StringToHash("Hit2");
    [HideInInspector] public int hit3ID = Animator.StringToHash("Hit3");
    [HideInInspector] public int crouchHitID = Animator.StringToHash("CrouchHit");
    [HideInInspector] public int restID = Animator.StringToHash("Rest");
    [HideInInspector] public int comboSpecialID = Animator.StringToHash("ComboSpecial");
    [HideInInspector] public int deathID = Animator.StringToHash("Death");
    [HideInInspector] public int reviveID = Animator.StringToHash("Revive");
    [HideInInspector] public int jumpID = Animator.StringToHash("Jump");
    [HideInInspector] public int jumpHitID = Animator.StringToHash("JumpHit");
    [HideInInspector] public int crouchID = Animator.StringToHash("Crouch");
    [HideInInspector] public int blockID = Animator.StringToHash("Block");
    [HideInInspector] public int crouchBlockID = Animator.StringToHash("CrouchBlock");
    [HideInInspector] public int forwardJumpID = Animator.StringToHash("ForwardJump");
    [HideInInspector] public int backwardJumpID = Animator.StringToHash("BackwardJump");
    [HideInInspector] public int forwardJumpKickID = Animator.StringToHash("ForwardJumpKick");
    [HideInInspector] public int forwardJumpPunchID = Animator.StringToHash("ForwardJumpPunch");
    [HideInInspector] public int fallFixID = Animator.StringToHash("FallFix");
    [HideInInspector] public int fallID = Animator.StringToHash("Fall");
    [HideInInspector] public int stunComboID = Animator.StringToHash("StunCombo");
    [HideInInspector] public int disappearID = Animator.StringToHash("Disappear");
    [HideInInspector] public int confusedID = Animator.StringToHash("Confused");
    [HideInInspector] public int flashedID = Animator.StringToHash("Flashed");
    [HideInInspector] public int airHitID = Animator.StringToHash("AirHit");
    public TiltEffect tilter;
    public bool isCurrentlyDying;
    public SoundManager manage;
    public WakanCombos wakan;
    public float height;
    public ClashScript clashy;
    public int attackTime, attackDur, attackRecov;
    public int screenPlayColor;
    public bool cantChangeInstantVars;
    public bool vibrationActive;
    #endregion
    void Awake()
    {
        cinetics = GameObject.Find("Cinetics").GetComponent<Cinetics>();
        tilt = GameObject.Find("Filter").GetComponent<TiltScript>();
        anim = GetComponentInParent<Animator>();
        sfx = Camera.main.GetComponent<SpecialEffects>();
        clashy = Camera.main.GetComponent<ClashScript>();
        tilter = GameObject.Find("TiltingEffect").GetComponent<TiltEffect>();
    }
    private void Start()
    {
        if(fighter.playerNumber == 1)
        {
            myGamePadIsConnected = GameOptions.player1Controller;
        }
        else if (fighter.playerNumber == 2)
        {
            myGamePadIsConnected = GameOptions.player2Controller;
        }
    }
    void Update()
    {
        height = dataBase.playerMe.transform.position.y;
        anim.SetFloat("Height", height);
        anim.SetFloat("Velocity",dataBase.myRb.velocity.y);
    }
    public void Step(bool ismoving)
    {
        anim.SetBool(movingID, ismoving);
        anim.SetFloat(directionID, fighter.direction * fighter.lookDirection);
    }
    public void Charge()
    {
        anim.SetTrigger(chargeID);
    }
    public void FullPower()
    {
        anim.SetTrigger(fullPowerID);
        dataBase.mySort.SortInLayer();
    }
    public void Punch_1()
    {
        anim.SetTrigger(punchCombo1ID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void Punch_2()
    {
        anim.SetTrigger(punchCombo2ID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void Punch_3()
    {
        anim.SetTrigger(punchCombo3ID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void Kick_1()
    {
        anim.SetTrigger(kickCombo1ID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void Kick_2()
    {
        anim.SetTrigger(kickCombo2ID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void Kick_3()
    {
        anim.SetTrigger(kickCombo3ID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void DestroyerCombo()
    {
        anim.SetTrigger(destroyerComboID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }

    public void CrouchPunch_1()
    {
        anim.SetTrigger(crouchPunchID);
    }
    public void CrouchKick_1()
    {
        anim.SetTrigger(crouchKickID);
    }
    public void Hit_1()
    {
        anim.SetTrigger(hit1ID);
        dataBase.mySort.layer = 2;
        dataBase.mySort.SortInLayer();
        sfx.SplashEffect(fighter.playerNumber, 0);
        sfx.Effects(fighter.playerNumber, lastEffectType, dataBase.enemyAnimationScript.lastX, dataBase.enemyAnimationScript.lastY);
        //StopCoroutine("ControllerVibratto");
        //StartCoroutine("ControllerVibratto", 1);
    }
    public void Hit_2()
    {
        anim.SetTrigger(hit2ID);
        dataBase.mySort.layer = 2;
        dataBase.mySort.SortInLayer();
        sfx.SplashEffect(fighter.playerNumber, 0);
        sfx.Effects(fighter.playerNumber, lastEffectType, dataBase.enemyAnimationScript.lastX, dataBase.enemyAnimationScript.lastY);
        //StopCoroutine("ControllerVibratto");
        //StartCoroutine("ControllerVibratto", 1);
    }
    public void Hit_3()
    {
        anim.SetTrigger(hit3ID);
        dataBase.mySort.layer = 2;
        dataBase.mySort.SortInLayer();
        sfx.SplashEffect(fighter.playerNumber, 0);
        sfx.Effects(fighter.playerNumber, lastEffectType, dataBase.enemyAnimationScript.lastX, dataBase.enemyAnimationScript.lastY);
        //StopCoroutine("ControllerVibratto");
        //StartCoroutine("ControllerVibratto", 1);
    }
    public void Hit_4()
    {
        anim.SetTrigger("Hit4");
        dataBase.mySort.layer = 2;
        dataBase.mySort.SortInLayer();
        sfx.SplashEffect(fighter.playerNumber, 0);
        sfx.Effects(fighter.playerNumber, lastEffectType, dataBase.enemyAnimationScript.lastX, dataBase.enemyAnimationScript.lastY);
        //StopCoroutine("ControllerVibratto");
        //StartCoroutine("ControllerVibratto", 1);
    }
    public void Air_Hit()
    {
        anim.SetTrigger(airHitID);
        dataBase.mySort.layer = 2;
        dataBase.mySort.SortInLayer();
        sfx.SplashEffect(fighter.playerNumber, 0);
        sfx.Effects(fighter.playerNumber, lastEffectType, dataBase.enemyAnimationScript.lastX, dataBase.enemyAnimationScript.lastY);
        //StopCoroutine("ControllerVibratto");
        //StartCoroutine("ControllerVibratto", 2);
    }
    public void CrouchHit()
    {
        anim.SetTrigger(crouchHitID);
        dataBase.mySort.layer = 2;
        dataBase.mySort.SortInLayer();
        sfx.SplashEffect(fighter.playerNumber, 0);
        sfx.Effects(fighter.playerNumber, lastEffectType, dataBase.enemyAnimationScript.lastX, dataBase.enemyAnimationScript.lastY);
        //StopCoroutine("ControllerVibratto");
        //StartCoroutine("ControllerVibratto", 2);
    }
    public void Rest()
    {
        anim.SetTrigger(restID);
    }
    public void ComboSpecial()
    {
        anim.SetTrigger(comboSpecialID);
    }
    public void Death()
    {
        anim.SetTrigger(deathID);
        //anim.SetBool(fallFixID, false);
    }
    public void Revive()
    {
        anim.SetTrigger(reviveID);
        anim.SetBool(deathID, false);
        //dataBase.myDamageScript.slider.value = fighter.life;
    }
    public void Jump()
    {
        anim.SetTrigger(jumpID);
    }
    public void JumpHit()
    {
        anim.SetTrigger(jumpHitID);
    }
    public void Crouch(bool iscrouching)
    {
        anim.SetBool(crouchID, iscrouching);
    }
    public void CrouchDebugger(int crouch)
    {
        if (crouch != 0)
        {
            fighter.isCrouch = true;
        }
        else
        {
            fighter.isCrouch = false;
        }
    }
    public void Block(bool isblocking)
    {
        anim.SetBool(blockID, isblocking);
        dataBase.mySort.layer = 3;
        dataBase.mySort.SortInLayer();
    }
    public void CrouchBlock(bool iscrouchblocking)
    {
        anim.SetBool(crouchBlockID, iscrouchblocking);
    }
    public void Shoot()
    {
        shoot.Throw(1);
    }
    public void BigShoot()
    {
        shoot.Throw(2);
    }
    public void ComboEnd()
    {
        fighter.isCombo = false;
    }
    public void Invincibility()
    {
        fighter.isInvincible = true;
    }
    public void UnInvincibility()
    {
        fighter.isInvincible = false;
    }
    public void ForwardJump()
    {
        anim.SetTrigger(forwardJumpID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void BackWardJump()
    {
        anim.SetTrigger(backwardJumpID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void PhysicJump()
    {
        dataBase.myMoveScript.PhysicJump();
    }
    public void PhysicForwardJump()
    {
        dataBase.myMoveScript.PhysicForwardJump();
    }
    public void ForwardJumpKick()
    {
        anim.SetTrigger(forwardJumpKickID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void ForwardJumpPunch()
    {
        anim.SetTrigger(forwardJumpPunchID);
        dataBase.mySort.layer = 1;
        dataBase.mySort.SortInLayer();
    }
    public void Falled()
    {
        anim.SetTrigger(fallID);
        //StopCoroutine("ControllerVibratto");
        //StartCoroutine("ControllerVibratto", 3);
        sfx.Effects(fighter.playerNumber, lastEffectType, dataBase.enemyAnimationScript.lastX, dataBase.enemyAnimationScript.lastY);
    }
    public void Fall()
    {
        //fighter.canRebound = true;
        fighter.isStunned = true;
    }
    public void FallFix(bool fallFix)
    {
        if(fighter.currentLife > 0)
        {
            anim.SetBool(fallFixID, fallFix);
        }
    }
    public void Recover()
    {
        fighter.isStunned = false;
        fighter.resistance = 4;
    }
    public void SlowDownTime()
    {
        Time.timeScale = 0.7f;
    }
    public void UnSlowDownTime()
    {
        Time.timeScale = 1;
    }
    public void CanAttack()
    {
        fighter.canAttack = true;
        vibrationActive = false;
    }
    public void StunCombo()
    {
        anim.SetTrigger(stunComboID);
    }
    public void CantAttack()
    {
        fighter.canAttack = false;
    }
    public void AttacksReset()
    {
        dataBase.myAttackScript.ResetAll();
    }
    public void AirAttackFramesStart()
    {
        fighter.isForwardJumping = true;
    }
    public void AirAttackFramesStop()
    {
        fighter.isForwardJumping = false;
    }
    public void Idle()
    {
        dataBase.mySort.layer = 0;
        dataBase.mySort.SortInLayer();
    }
    public void DodgeCombo()
    {
        fighter.beenUnblocked = true;
        dataBase.myMoveScript.UnBlock();
        anim.SetTrigger(disappearID);
    }
    public void ShootStun()
    {
        shoot.Throw(3);
    }
    public void Confused()
    {
        anim.SetTrigger(confusedID);
    }
    public void Confusion(int confusion)
    {
        switch (confusion)
        {
            case 0:
                fighter.isStunned = false;
                break;
            case 1:
                fighter.isStunned = true;
                break;
        }
    }
    public void Limitations(int type)
    {
        switch (type)
        {
            case 0:
                dataBase.myRb.gravityScale = 22.5f;
                fighter.canMove = false;
                fighter.canRotate = true;
                break;
            case 1:
                dataBase.myRb.gravityScale = 22.5f;
                fighter.canMove = fighter.canRotate = true;
                break;
            case 2:
                dataBase.myRb.gravityScale = 22.5f;
                fighter.canMove = false;
                fighter.canRotate = false;
                break;
        }
        switch (fighter.playerNumber)
        {
            case 1:
                CameraMove.specialCameraMove1 = false;
                break;
            case 2:
                CameraMove.specialCameraMove2 = false;
                break;
        }
    }
    public void Flashed()
    {
        anim.SetTrigger(flashedID);
        dataBase.myRb.velocity = Vector2.zero;
    }
    public void Flash()
    {
        dataBase.enemyAnimationScript.Flashed();
    }
    public void LessenMana(int bars)
    {
        dataBase.myMana.LessenMana(bars);
    }
    public void CanAIttack()
    {
        fighter.canAIttack = true;
    }
    public void ColorScreenPlay(int color)
    {
        screenPlayColor = color;
    }
    public void ScreenPlay(int mode)
    {
        cinetics.ScreenPlay(mode, fighter.playerNumber, screenPlayColor);
    }
    public void ReturnBlock(int isblock)
    {
        switch (isblock)
        {
            case 0:
                dataBase.myMoveScript.ActuallyBlocking(false);
                break;
            case 1:
                dataBase.myMoveScript.ActuallyBlocking(true);
                break;
        }
    }
    public void TimeToJumpAttack()
    {

    }
    public void TimeToUnJumpAttack()
    {

    }
    public IEnumerator Advance()
    {
        yield return new WaitUntil(() => dataBase.myRb.velocity == Vector2.zero);
        dataBase.myRb.AddForce(new Vector2(fighter.lookDirection * 1000, 0));
        hasAdvanced = true;
    }
    public IEnumerator HowMuchAdvance(int variable)
    {
        yield return new WaitUntil(() => dataBase.myRb.velocity == Vector2.zero);
        dataBase.myRb.AddForce(new Vector2(fighter.lookDirection * variable, 0));
        hasAdvanced = true;
    }
    public void Vorgov()
    {
        dataBase.myMoveScript.debug = false;
    }
    public void DustInWind(int place)
    {
        if (canDust && !lastWalked)
        {
            canDust = false;
            lastWalked = true;
            sfx.Dust(fighter.playerNumber, place);
            StartCoroutine(TimeBetweenDusts());
        }
    }
    public IEnumerator TimeBetweenDusts()
    {
        yield return new WaitForSeconds(1f);
        if (dataBase.myFighter.isWalking)
        {
            lastWalked = true;
        }
        canDust = true;
    }

    public void JumpSfx()
    {
        sfx.Jumped(fighter.playerNumber);
    }
    public void ForwardJumpSfx(int place)
    {
        sfx.ForwardJumped(fighter.playerNumber, place);
    }
    public IEnumerator DewIt()
    {
        Ray.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Ray.SetActive(false);
    }
    public void GenerateMissile()
    {
        Instantiate(Missile, transform.position + (Vector3.up * 13), transform.rotation);
    }
    public void StunMyself()
    {
        dataBase.myAnimationScript.Confused();
    }
    public void FranDebugger(int option)
    {
        if(option != 0)
        {
            debuggerBoolHit = true;
        }
        else
        {
            debuggerBoolHit = false;
        }
    }
    public void Fran()
    {
        fighter.isAirAttacking = true;
        Physics2D.IgnoreCollision(dataBase.myBoxCo, dataBase.enemyBoxCo, true);
        EmpujeScript.isPushingAllowed = false;
        debuggerJustOneHit = 0;
    }
    public void BlockFix()
    {
        fighter.beenUnblocked = false;
    }
    public void Matrix()
    {
        if (dataBase.myDamageScript.hitPerformed == 16 || dataBase.myDamageScript.hitPerformed == 3)
        {
            if (!dataBase.enemyAnimationScript.vibrationActive)
            {
                StartCoroutine(DramaticPause.Pause(0.17f));
            }
            else if (dataBase.enemyAnimationScript.vibrationActive)
			{
                vibrationActive = false;
                ShakeEffect.isShaking = true;
                StartCoroutine(DramaticPause.Pause(0.2f));
                dataBase.myDamageScript.shaker.StartCoroutine("Shake", 0.15f);
            }
        }
        else if(dataBase.myDamageScript.hitPerformed == 1 || dataBase.myDamageScript.hitPerformed == 4 || dataBase.myDamageScript.hitPerformed == 13 || dataBase.myDamageScript.hitPerformed == 14)
        {
            if (!dataBase.enemyAnimationScript.vibrationActive)
            {
                StartCoroutine(DramaticPause.Pause(0.10f));
            }
            else if (dataBase.enemyAnimationScript.vibrationActive)
            {
                vibrationActive = false;
                ShakeEffect.isShaking = true;
                StartCoroutine(DramaticPause.Pause(0.2f));
                dataBase.myDamageScript.shaker.StartCoroutine("Shake", 0.15f);
            }
        }
        else if (dataBase.myDamageScript.hitPerformed == 15 || dataBase.myDamageScript.hitPerformed == 5)
        {
            ShakeEffect.isShaking = true;
            StartCoroutine(DramaticPause.Pause(0.1f));
            dataBase.myDamageScript.shaker.StartCoroutine("Shake", 0.08f);
        }
    }
    //public IEnumerator ControllerVibratto(int intensity)
    //{
    //    float vibratto = 0;
    //    WaitForSecondsRealtime counter = new WaitForSecondsRealtime(0);
    //    if (myGamePadIsConnected)
    //    {
    //        switch (intensity)
    //        {
    //            case 1:
    //                vibratto = 0.5f;
    //                counter = new WaitForSecondsRealtime(0.2f);
    //                break;
    //            case 2:
    //                vibratto = 0.8f;
    //                counter = new WaitForSecondsRealtime(0.4f);
    //                break;
    //            case 3:
    //                vibratto = 1;
    //                counter = new WaitForSecondsRealtime(0.6f);
    //                break;
    //        }
    //        GamePad.SetVibration(playerIndex, vibratto, vibratto);
    //        yield return counter;
    //        GamePad.SetVibration(playerIndex, 0, 0);
    //    }
    //}
    public void Levitate()
    {
        fighter.isDoingCombo2 = true;
    }
    public void StopLevitate()
    {
        fighter.isDoingCombo2 = false;
    }
    public void Tilt(float time)
    {
        StartCoroutine(tilt.isTilting(time));
    }
    public void Transformium(int transforming)
    {
        if(transforming == 0)
        {
            anim.SetBool("Transformium", false);
        }
        else if (transforming == 1)
        {
            anim.SetBool("Transformium", true);
        }
    }
    public void Idled(int fromBlock)
    {
        if (fromBlock == 0)
        {
            anim.SetBool("Debugium", false);
        }
        else if (fromBlock == 1)
        {
            anim.SetBool("Debugium", true);
        }
    }
    public void SpecialHemogoblin()
    {
        anim.SetTrigger("BackNormal");
    }
    public void Eaten()
    {
        anim.SetTrigger("Eaten");
    }
    public void IsChild(int ischild)
    {
        switch (ischild)
        {
            case 0:
                anim.SetBool("IsChild", false);
                break;

            case 1:
                anim.SetBool("IsChild", true);
                break;
        }
    }
    public void FirstRebound()
    {
        if (fighter.ID == 1)
        {
            dataBase.myRb.velocity = Vector2.zero;
            dataBase.myRb.AddForce(new Vector2(-fighter.lookDirection * 1400, 0));
        }
        else
        {
            dataBase.myRb.velocity = Vector2.zero;
            dataBase.myRb.AddForce(new Vector2(-fighter.lookDirection * 500, 1500));
            isCurrentlyDying = true;
        }
    }
    public void SecondRebound()
    {
        dataBase.myRb.AddForce(new Vector2(-fighter.lookDirection * 200, 500));
    }
    public void TimeNormal()
    {
        Time.timeScale = 1f;
    }
    public void IsReady()
    {
        tilt.LetsFight(0);
    }
    public void Victory()
    {
        anim.SetTrigger("Victory");
    }
    public void Shoryuken(int mode)
    {
        switch (mode)
        {
            case 0:
                dataBase.myRb.isKinematic = true;
                dataBase.myRb.velocity = new Vector2(dataBase.myFighter.lookDirection * 15, 0);
                break;
            case 1:
                dataBase.myRb.isKinematic = false;
                dataBase.myRb.velocity = new Vector2(0, 0);
                dataBase.myRb.gravityScale = 15f;
                break;
            case 2:
                fighter.isAirAttacking = true;
                fighter.isGrounded = false;
                fighter.customRotate = false;
                StartCoroutine(dataBase.myMoveScript.Ignore());
                dataBase.myRb.velocity = new Vector2(0, 30);
                break;
        }

    }
    public void Ignorability(int Ignore)
    {
        switch (Ignore)
        {
            case 0:
                collisionIgnored = false;
                Physics2D.IgnoreCollision(dataBase.myBoxCo, dataBase.enemyBoxCo, false);
                break;
            case 1:
                collisionIgnored = true;
                Physics2D.IgnoreCollision(dataBase.myBoxCo, dataBase.enemyBoxCo, true);
                break;
        }
    }
    public void LeoCatRemainStatic(int cat)
    {
        switch (cat)
        {
            case 0:
                catSituationLeo = 0;
                break;
            case 1:
                catSituationLeo = 1; //remain quieto
                break;
        }
    }
    public void AbsoluteDebugger()
    {
        anim.ResetTrigger(punchCombo1ID);
        anim.ResetTrigger(punchCombo2ID);
        anim.ResetTrigger(punchCombo3ID);
        anim.ResetTrigger(kickCombo1ID);
        anim.ResetTrigger(kickCombo2ID);
        anim.ResetTrigger(kickCombo3ID);
        anim.ResetTrigger(crouchPunchID);
        anim.ResetTrigger(crouchKickID);
        anim.ResetTrigger(destroyerComboID);
        anim.ResetTrigger(comboSpecialID);
        anim.ResetTrigger(stunComboID);
        if (fighter.ID == 0)
        {
            anim.ResetTrigger("SuperCombo");
        }
    }
    public void NewSumMana()
    {
        dataBase.myMana.OnManaSum();
    }
    public void SoundManager(int sound)
    {
        manage.AttackSound(sound);
    }
    public void WakanAdvance(int advancing)
    {
        if(advancing == 1)
        {
            wakan.isAdvancing = true;
        }
        else
        {
            wakan.isAdvancing = false;
            wakan.StopCoroutine("NaturalPause");
        }
    }

    public void ClashTime(int start)
    {
        attackTime = start;
    }

    public void ClashDur(int duration)
    {
        attackDur = duration;
    }
    public void ClashRecov(int recoveryTime)
    {
        attackRecov = recoveryTime;
    }
    public void MakeACall()
    {
        clashy.AttackCall(fighter.playerNumber, attackTime - 1, attackDur);
    }
    public void FixAll()
    {
        PlayerDamageController.alreadyHitted = false;
    }
    public void BigJumps()
    {
        switch (fighter.playerNumber)
        {
            case 1:
                CameraMove.specialCameraMove1 = true;
                break;
            case 2:
                CameraMove.specialCameraMove2 = true;
                break;
        }
    }
    public void BulletTime()
    {
        Time.timeScale = 0.25f;
    }
    public IEnumerator StartFading()
    {
        timeToWin = true;
        yield return new WaitForSeconds(1.5f);
        if (!MatchControlScript.matchWillOver)
        {
            timeToFade = true;
        }
        else if (MatchControlScript.matchWillOver)
        {
            timeToFade = true;
        }
    }

    public void TiltUp(int HowMuchBars)
    {
        tilter.StartCoroutine("Hey", HowMuchBars);
    }
    public void ActivateVibration()
	{
        vibrationActive = true;
    }

	#region AutoCheckedVariables

	public void TypeOfHit(int type)
    {
        if (!cantChangeInstantVars)
        {
            dataBase.enemyDamageScript.hitPerformed = type;
            dataBase.myDamageScript.hitDone = type;
        }
    }

    public void CustomEffectNeeded(int effect)
    {
        if (!cantChangeInstantVars)
        {
            dataBase.enemyAnimationScript.lastEffectType = effect;
        }
    }

    public void ConcreteEffectX(float x)
    {
        if (!cantChangeInstantVars)
        {
            lastX = x;
        }
    }

    public void ConcreteEffectY(float y)
    {
        if (!cantChangeInstantVars)
        {
            lastY = y;
        }
    }

    public void LastDamage(int Damage)
    {
        if (!cantChangeInstantVars)
        {
            lastDamage = Damage;
        }
    }

    public void Hardness(int hardness)
    {
        if (!cantChangeInstantVars)
        {
            dataBase.enemyDamageScript.attackHardness = hardness;
        }
    }
    #endregion AutoCheckedVariables
}
