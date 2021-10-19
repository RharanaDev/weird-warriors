using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGeneralMoveController : MonoBehaviour
{
    #region Data
    public FightersData fighter;
    public InstanceSaver database;
    public string myLastCoroutine;
    #endregion

    #region References
    public Animator anim;
    public bool booleo;
    public static bool specialBooleo;
    #endregion

    #region Variables
    //CheckBools
    public bool jumpable, movingPressed, debugJump, rotating, isUpwards, canJumpCo, blockJumps;
    //InputNumbers
    public int vectX, vectY;
    public int jumpDirection;
    private float displacement;
    public bool debug;
    public bool timeBetweenLessen;
    //Temporary
    private Vector2 normalBoxSize;
    private Vector2 normalBoxOffset;
    public Vector3 originalPos;
    //public BoxCollider2D box;
    public Vector2 CrouchBoxSize, CrouchBoxOffset;
    //For Two-Player Checks
    private int debugSet = 400;
    public static int onAirNumber = 0;
    public bool canMoveNow; //new corroutine
    public bool beingRelantised;
    public int jumpVarDebugged;
    #endregion

    private void Start()
    {
        fighter.Redo();
        database.Redo();
        originalPos = transform.position;
        fighter.canBeRepeled = true;
        normalBoxSize = database.myBoxCo.size;
        normalBoxOffset = database.myBoxCo.offset;
        canJumpCo = true;
    }
    private void OnDisable()
    {
        debugJump = false;
    }
    private void OnEnable()
    {
        debugJump = true;
        if (fighter.playerNumber == 1)
        {
            fighter.lookDirection = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (fighter.playerNumber == 2)
        {
            fighter.lookDirection = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    //For Inputs and animation//
    void Update()
    {

        InformAnim();

        if (database.myRb.velocity.y <= 0)
        {
            isUpwards = true;
        }
        else if (database.myRb.velocity.y > 0)
        {
            isUpwards = false;
        }
        if (database.enemyFighter.isGrounded == false && fighter.isGrounded == false)
        {
            StopCoroutine(Ignore());
            Physics2D.IgnoreCollision(database.myBoxCo, database.enemyBoxCo, true);
        }
        if (fighter.resistance < fighter.originalRes && fighter.resistance >= 1)
        {
            if (!booleo)
            {
                StopCoroutine("RechargeBar");
                StartCoroutine("RechargeBar");
            }
        }
        if (onAirNumber == 2)
        {
            EmpujeScript.isPushingAllowed = false;
            specialBooleo = true;
            Physics2D.IgnoreCollision(database.myBoxCo, database.enemyBoxCo, true);
        }
    }
    public IEnumerator ResetBar()
    {
        booleo = true;
        yield return new WaitForSeconds(2.5f);
        fighter.resistance = 1;
        database.myAnim.SetInteger("Resistance", fighter.resistance);
        database.myDamageScript.blockBar.resistance = fighter.resistance;
        booleo = false;
    }
    public IEnumerator RechargeBar()
    {
        booleo = true;
        yield return new WaitForSeconds(1.2f);
        fighter.resistance++;
        database.myAnim.SetInteger("Resistance", fighter.resistance);
        database.myDamageScript.blockBar.resistance = fighter.resistance;
        booleo = false;
    }
    //For physics only//
    void FixedUpdate()
    {
        Movement();
    }

    //Rotate player//
    public void Turn()
    {
        rotating = true;
        StartCoroutine(Turning());
    }

    public void MoveExit()
    {
        movingPressed = false;
        fighter.direction = 0;
        UnCrouch();
    }

    public void FixedMoveExit()
    {
        movingPressed = false;
        fighter.direction = 0;
    }

    public void SideMovement()
    {
        if (fighter.canWaitForDodgeCombo == false)
        {
            UnCrouch();
            if (!fighter.isInverted)
            {
                fighter.direction = vectX;
            }
            else if (fighter.isInverted)
            {
                fighter.direction = -vectX;
            }
            movingPressed = true;
        }
        else if (fighter.canWaitForDodgeCombo && fighter.lookDirection == vectX && database.myMana.manaBars >= 2 && !fighter.isCrouch)
        {
            database.myAnimationScript.DodgeCombo();
        }
    }

    public void JumpSelection()
    {
        fighter.canAttack = false;
        blockJumps = true;
        StartCoroutine(JumpFix());
    }
    IEnumerator JumpFix()
    {
        yield return new WaitForSeconds(0.1f);

        if (movingPressed)
        {
            ForwardJump();
        }
        else
        {
            Jump();
        }
    }
    public void Jump()
    {
        if (!fighter.isCrouch && fighter.canMove)
        {
            database.myAnimationScript.AbsoluteDebugger();
            fighter.canAttack = false;
            canJumpCo = true;
            if (fighter.canMove)
            {
                if (fighter.isGrounded == true && debugJump && fighter.isCombo == false && fighter.isStunned == false && movingPressed == false)
                {
                    database.myAnimationScript.Jump();
                    fighter.isGrounded = false;
                }
            }
        }
    }

    public void PhysicJump()
    {
        PlayerGeneralMoveController.onAirNumber++;
        fighter.customRotate = false;
        Cam_Movement.bigDummyCount++;
        database.myRb.AddForce(new Vector2(0, fighter.jumpForce));
        StartCoroutine(CameraFixJump());
    }
    public void ForwardJump()
    {
        if (!fighter.isCrouch && fighter.canMove)
        {
            fighter.canAttack = false;
            database.myAnimationScript.AbsoluteDebugger();
            canJumpCo = true;
            StopCoroutine(JumpFix());
            if (fighter.canMove)
            {
                if (fighter.isGrounded == true && debugJump && fighter.isCombo == false && fighter.isStunned == false)
                {
                    if (fighter.lookDirection == jumpDirection)
                    {
                        database.myAnimationScript.ForwardJump();
                        jumpVarDebugged = jumpDirection;
                        fighter.isGrounded = false;
                    }
                    else if (fighter.lookDirection == -jumpDirection)
                    {
                        database.myAnimationScript.BackWardJump();
                        jumpVarDebugged = jumpDirection;
                        fighter.isGrounded = false;
                    }
                }
            }
        }
    }
    public void PhysicForwardJump()
    {
        PlayerGeneralMoveController.onAirNumber++;
        fighter.customRotate = false;
        Cam_Movement.dummyCount++;
        Vector2 force;
        force = new Vector2(debugSet * jumpVarDebugged, 3100);
        jumpVarDebugged = 0;
        jumpDirection = 0;
        StartCoroutine(Ignore());
        database.myRb.AddForce(force);
    }
    public IEnumerator CameraFixJump()
    {
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => isUpwards);
        Cam_Movement.bigDummyCount--;
    }
    public IEnumerator Ignore()
    {
        fighter.canBeRepeled = false;
        Physics2D.IgnoreCollision(database.myBoxCo, database.enemyBoxCo, true);
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => isUpwards);
        Cam_Movement.dummyCount--;
        fighter.canBeRepeled = true;
        if (!fighter.isAirAttacking)
        {
            if (!database.myAnimationScript.collisionIgnored)
            {
                Physics2D.IgnoreCollision(database.myBoxCo, database.enemyBoxCo, false);
            }
            EmpujeScript.isPushingAllowed = true;
        }
    }
    void Movement()
    {

        if (fighter.direction == fighter.lookDirection)
        {
            if (!beingRelantised)
            {
                displacement = fighter.speed * fighter.direction * Time.fixedDeltaTime;
            }
            else
            {
                displacement = 5 * fighter.direction * Time.fixedDeltaTime;
            }
        }
        else if (fighter.direction == -fighter.lookDirection)
        {
            displacement = (fighter.speed - 4) * fighter.direction * Time.fixedDeltaTime;
        }
        else
        {
            displacement = 0;
        }

        if (fighter.isCrouch == false && fighter.isStunned == false && fighter.isGrounded == true && fighter.canMove && fighter.direction != 0)
        {
            database.myRb.MovePosition(database.myRb.position + new Vector2(displacement, 0));
        }
    }
    public void Crouch()
    {
        if (fighter.isGrounded == true && fighter.isStunned == false && !fighter.isBlock)
        {
            database.myBoxCo.size = fighter.crouchHitBoxSize;
            database.myBoxCo.offset = fighter.crouchHitBoxOffSet;
            fighter.isCrouch = true;
            database.myAnimationScript.Crouch(true);
            database.myPush.alture = database.myPush.otherAlture;
        }
        else if (fighter.isBlock && debug == false)
        {
            database.myBoxCo.size = fighter.crouchHitBoxSize;
            database.myBoxCo.offset = fighter.crouchHitBoxOffSet;
            fighter.isCrouch = true;
            database.myAnimationScript.CrouchBlock(true);
            database.myAnimationScript.Block(true);
            database.myAnimationScript.Crouch(true);
            database.myPush.alture = database.myPush.otherAlture;
            debug = true;
        }
    }
    private void UnCrouch()
    {
        if (fighter.isCrouch)
        {
            database.myBoxCo.size = normalBoxSize;
            database.myBoxCo.offset = normalBoxOffset;
            fighter.isCrouch = false;
            database.myAnimationScript.Crouch(false);
            if (fighter.isBlock)
            {
                database.myAnimationScript.CrouchBlock(false);
            }
            database.myPush.alture = database.myPush.normalAlture;
        }
    }
    public void Block()
    {
        if (fighter.resistance != 0 && fighter.isCombo == false && fighter.isStunned == false && fighter.canAttack && !fighter.beenUnblocked)
        {
            if (fighter.isGrounded == true && fighter.isCrouch == false)
            {
                database.myAnimationScript.Block(true);
                debug = false;
            }
            else if (fighter.isCrouch == true)
            {
                database.myAnimationScript.CrouchBlock(true);
                database.myAnimationScript.Block(true);
            }
        }
    }
    public void UnBlock()
    {
        if (fighter.isBlock)
        {
            database.myAnimationScript.Block(false);
            database.myAnimationScript.CrouchBlock(false);
        }
    }

    void InformAnim()
    {
        if (displacement != 0 && fighter.isGrounded)
        {
            fighter.isWalking = true;
            database.myAnimationScript.Step(true);
        }
        else
        {
            database.myAnimationScript.Step(false);
            fighter.isWalking = false;
            database.myAnimationScript.lastWalked = false;
        }

    }

    //Floor collisions//
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("PlayerTwo") || col.gameObject.CompareTag("Player"))
        {
            database.myRb.velocity = Vector2.zero;
            beingRelantised = true;
        }

        fighter.customRotate = true;
        if (col.gameObject.CompareTag("floor") && fighter.canRebound && !database.myAnimationScript.isCurrentlyDying)
        {
            database.myAnimationScript.FallFix(false);
            database.myRb.AddForce(new Vector2(-fighter.lookDirection * 200, 800));
            fighter.canRebound = false;
            if (!database.myAnimationScript.collisionIgnored)
            {
                Physics2D.IgnoreCollision(database.myBoxCo, database.enemyBoxCo, false);
            }
            EmpujeScript.isPushingAllowed = true;
        }
        if (col.gameObject.CompareTag("floor") && database.myAnimationScript.isCurrentlyDying)
        {
            database.myAnimationScript.isCurrentlyDying = false;
            database.myAnimationScript.SecondRebound();
            fighter.canRebound = false;
        }
        if (col.gameObject.CompareTag("floor") && fighter.canRebound == false)
        {
            fighter.isAirAttacking = false; //DUDOSA
            fighter.isGrounded = true;
            database.myRb.velocity = new Vector2(0, database.myRb.velocity.y);
            fighter.isForwardJumping = false;
            fighter.canAirHit = true;
            database.myAnimationScript.FallFix(false);
            if (!database.myAnimationScript.collisionIgnored)
            {
                Physics2D.IgnoreCollision(database.myBoxCo, database.enemyBoxCo, false);
            }
            EmpujeScript.isPushingAllowed = true;
            if (onAirNumber == 0)
            {
                PlayerGeneralMoveController.onAirNumber = 0;
                specialBooleo = false;
            }
            else
            {
                PlayerGeneralMoveController.onAirNumber--;
                specialBooleo = false;
            }

        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("PlayerTwo") || col.gameObject.CompareTag("Player"))
        {
            beingRelantised = false;
        }
    }

    //Foe collisions over player//
    IEnumerator Turning()
    {
        while (fighter.isGrounded == false && !fighter.customRotate || fighter.canRotate == false)
        {
            yield return null;
        }
        transform.Rotate(0, 180, 0, Space.Self);
        fighter.lookDirection = -fighter.lookDirection;
        rotating = false;
    }
    public void ActuallyBlocking(bool blocking)
    {
        if (blocking)
        {
            fighter.isBlock = true;
        }
        else if (!blocking)
        {
            fighter.isBlock = false;
        }
    }

}