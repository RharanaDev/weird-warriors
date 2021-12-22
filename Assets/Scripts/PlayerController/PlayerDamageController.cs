using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageController : MonoBehaviour
{
    public int limitRes;
    public ParticleSystem particles2;
    public FightersData fighter;
    public InstanceSaver dataBase;
    private MatchControlScript controlScript;
    public ParticleSystem dizzy;
    public static int clashers;
    public static bool clashsituation;
    //Script
    public ShakeEffect shaker;
    public SpecialEffects effex;
    //Script-Only Variables:
    //Objects or Components
    public GameObject hitbox;
    public int residualBars;
    public BoxCollider2D hitBox, damager;

    public Color color; // Blink color for blockBreak
    private Color originalColor; // Original color for blockBreak
    private Camera cam;
    public static bool matchIsOn = true;
    //Camera
    private Vector2 actualKnockBack; // Knockback value and direction
    private Vector2 comboKnockBack;
    //Integers
    public int hitCount = 1; // Saves number of hits to display hit Animations
    //Booleans
    public int hitPerformed, hitDone;
    public int runUpValue;
    public int parameter;
    public int attackHardness;
    public Vector2 runUp;
    public int yValue;
    public int xValue;
    LifeBarScript lifeBar;
    public int anim;
    public static int clash;
    public bool isClashing;
    public string IgnoreString, noIgnoreString, IgnoreString2, noIgnoreString2;
    public bool cancelRunup;
    public int[] knockBacks;
    public bool overrideMissile;
    public bool specialMissile;
    public bool generatingDust;
    bool lastBoolOnMissile;
    public GameObject effectIfBlocked;
    public BlockBarScript blockBar;
    private ClashScript clashy;
    public static bool OverrideAllOthers;
    public static bool clashEffecting;
    public float effectDur;
    public Transform HitterPos;
    public GameObject effectClash;
    public static int clashNum;
    public static bool alreadyHitted;
    public int numberOfHits;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        shaker = cam.GetComponent<ShakeEffect>();
        clashy = cam.GetComponent<ClashScript>();
        controlScript = cam.GetComponent<MatchControlScript>();
        effex = cam.GetComponent<SpecialEffects>();
    }
    void Start()
    {
        matchIsOn = true;
        Physics2D.IgnoreCollision(hitBox, damager, true); 
        lifeBar = GameObject.Find("UIPref/LifeBar" + fighter.playerNumber + "/Life").GetComponent<LifeBarScript>();
        blockBar = GameObject.Find("UIPref/BlockBar" + fighter.playerNumber).GetComponent<BlockBarScript>();
        blockBar.originalRes = fighter.originalRes;
        blockBar.resistance = fighter.resistance;
        anim = Random.Range(0,3);
    }
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        Projectile proj;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        if (!collision.CompareTag(IgnoreString) && !collision.CompareTag(IgnoreString2) && dataBase.enemyAnimationScript.debuggerJustOneHit < 1 || dataBase.enemyAnimationScript.debuggerJustOneHit < 1 && collision.CompareTag(noIgnoreString) && collision.CompareTag(noIgnoreString2))
        {
            if (!clashy.isThisFrameClash() && !OverrideAllOthers && !alreadyHitted)
            {
                specialMissile = false;
                alreadyHitted = true;
                if (dataBase.enemyAnimationScript.debuggerBoolHit)
                {
                    dataBase.enemyAnimationScript.debuggerJustOneHit++;
                }
                if (collision.CompareTag("ComboPartP1") || collision.CompareTag("ComboPartP2"))
                {
                    overrideMissile = true;
                    lastBoolOnMissile = collision.gameObject.GetComponent<Projectile>().isntInstant;
                }
                else if (collision.CompareTag("SpecialComboPartP1") || collision.CompareTag("SpecialComboPartP2"))
                {
                    proj = collision.gameObject.GetComponent<Projectile>();
                    specialMissile = true;
                    numberOfHits = 0;
                    hitPerformed = proj.hitType;
                    dataBase.enemyDamageScript.hitDone = hitPerformed;
                    dataBase.enemyAnimationScript.lastX = proj.lastX;
                    dataBase.enemyAnimationScript.lastY = proj.lastY;
                    dataBase.enemyAnimationScript.lastDamage = proj.lastDamage;
                    attackHardness = proj.attackHardness;
                    dataBase.myAnimationScript.lastEffectType = proj.lastEffectType;
                    dataBase.enemyAnimationScript.cantChangeInstantVars = true;
                }
                if (hitPerformed != 12)
                {
                    BigBlock();
                }
                else
                {
                    if (!collision.gameObject.GetComponent<Projectile>().isntInstant)
                    {
                        dataBase.myRb.AddForce(new Vector3(-fighter.lookDirection * 700, 0));
                    }
                    dataBase.myAnimationScript.Flashed();
                }
            }
            else
            {
                OverrideAllOthers = true;
                if (!clashEffecting && !alreadyHitted)
                {
                    StopCoroutine("ClashEffect");
                    StartCoroutine("ClashEffect");
                }
            }
        }
    }
    public IEnumerator ClashEffect()
    {
        GameObject toDestroy;
        clashEffecting = true;
        if (dataBase.myAnimationScript.attackRecov <= dataBase.enemyAnimationScript.attackRecov)
        {
            effectDur = dataBase.myAnimationScript.attackRecov;
        }
        else
        {
            effectDur = dataBase.enemyAnimationScript.attackRecov;
        }
        toDestroy = Instantiate(effectClash, new Vector3(((HitterPos.position.x + dataBase.enemyDamageScript.HitterPos.position.x)/2), ((HitterPos.position.y + dataBase.enemyDamageScript.HitterPos.position.y) / 2), HitterPos.position.z), transform.rotation);
        Destroy(toDestroy, 0.5f);
        Time.timeScale = 0.5f;
        shaker.StartCoroutine("Shake", 0.15f);
        yield return new WaitForSecondsRealtime(effectDur / 30);
        Time.timeScale = 1;
        OverrideAllOthers = false;
        clashEffecting = false;
    }
    public void RestartBars()
    {
        lifeBar.RestartBar();
        dataBase.myMana.LessenAllMana();
    }

    void BigBlock()
    {
        if (!specialMissile)
        {
            SetRunUp();
        }
        if (fighter.isInEnd && !specialMissile)
        {
            actualKnockBack = Vector3.zero;
            comboKnockBack = new Vector3(0, yValue);
        }
        else
        {
            actualKnockBack = new Vector3(fighter.lookDirection * -1, 0);
            comboKnockBack = new Vector3(xValue * fighter.lookDirection * -1, yValue);
        }
        if (fighter.isBlock == false && fighter.isInvincible == false)
        {
            if (fighter.isGrounded == true)
            {
                CheckHit();
            }

            else if (fighter.isGrounded == false)
            {
                fighter.currentLife -= dataBase.enemyAnimationScript.lastDamage;
                lifeBar.damage = dataBase.enemyAnimationScript.lastDamage;
                lifeBar.LessenMyLife();
                OnDeath();
                if (fighter.isAlive)
                {
                    if (hitPerformed != 15 && hitPerformed != 12)
                    {
                        if (PlayerGeneralMoveController.onAirNumber != 2)
                        {
                            dataBase.myAnimationScript.Air_Hit();
                            dataBase.myRb.velocity = Vector2.zero;
                            dataBase.myRb.AddForce(new Vector2(300 * -fighter.lookDirection, 1700));
                            dataBase.enemyAttackScript.hasHit = dataBase.enemyAttackScript.hasAIHit = true;
                            dataBase.enemyAttackScript.ComboTracker();
                        }
                        else if (PlayerGeneralMoveController.onAirNumber == 2)
                        {
                            dataBase.myAnimationScript.Air_Hit();
                            dataBase.myRb.velocity = Vector2.zero;
                            dataBase.myRb.AddForce(new Vector2(800 * fighter.lookDirection * -1, 0));
                            dataBase.enemyAttackScript.hasHit = dataBase.enemyAttackScript.hasAIHit = true;
                            dataBase.enemyAttackScript.ComboTracker();
                        }
                    }
                    else if(hitPerformed == 15)
                    {
                        dataBase.myAnimationScript.Air_Hit();
                        dataBase.myRb.velocity = Vector2.zero;
                        dataBase.myRb.AddForce(new Vector2(0, 1000));
                        dataBase.enemyAttackScript.hasHit = dataBase.enemyAttackScript.hasAIHit = true;
                        dataBase.enemyAttackScript.ComboTracker();
                    }
                }
                else
                {
                    if (PlayerGeneralMoveController.onAirNumber != 2)
                    {
                        dataBase.myRb.velocity = Vector2.zero;
                        dataBase.myRb.AddForce(new Vector2(300 * -fighter.lookDirection, 1700));
                        dataBase.enemyAttackScript.ComboTracker();
                    }
                    else if (PlayerGeneralMoveController.onAirNumber == 2)
                    {
                        dataBase.myRb.velocity = Vector2.zero;
                        dataBase.myRb.AddForce(new Vector2(800 * fighter.lookDirection * -1, 0));
                        dataBase.enemyAttackScript.ComboTracker();
                    }
                }
            }
        }
        if (fighter.isBlock == true && fighter.isInvincible == false)
        {
            LessenResistance();
        }
    }

    void LessenResistance()
    {
        int actualResistance;
        actualResistance = Mathf.Clamp(attackHardness, 0, fighter.originalRes);
        residualBars = fighter.resistance - actualResistance;
        fighter.resistance -= actualResistance;
        fighter.resistance = Mathf.Clamp(fighter.resistance, 0, fighter.originalRes);
        dataBase.myAnim.SetInteger("Resistance", fighter.resistance);
        blockBar.resistance = fighter.resistance;

        if (fighter.resistance > 0)
        {
            dataBase.myAnimationScript.lastEffectType = 9;
            dataBase.myAnimationScript.sfx.Effects(fighter.playerNumber, dataBase.myAnimationScript.lastEffectType, dataBase.enemyAnimationScript.lastX, dataBase.enemyAnimationScript.lastY);
            dataBase.myAnimationScript.sfx.SplashEffect(fighter.playerNumber, 1);
            CheckHit();
            dataBase.myMoveScript.booleo = true;
            dataBase.myMoveScript.StopCoroutine("RechargeBar");
            dataBase.myMoveScript.StartCoroutine("RechargeBar");
        }
        if (fighter.resistance == 0)
        {
            dataBase.myAnimationScript.lastEffectType = 9;
            dataBase.myAnimationScript.sfx.Effects(fighter.playerNumber, dataBase.myAnimationScript.lastEffectType, dataBase.enemyAnimationScript.lastX, dataBase.enemyAnimationScript.lastY);
            dataBase.myAnimationScript.sfx.SplashEffect(fighter.playerNumber, 1);
            CheckHit();
            dataBase.myMoveScript.booleo = true;
            dataBase.myMoveScript.UnBlock();
            dataBase.myMoveScript.StopCoroutine("RechargeBar");
            dataBase.myMoveScript.StopCoroutine("ResetBar");
            dataBase.myMoveScript.StartCoroutine("ResetBar");
        }
        //if(residualBars < 0)
        //{
        //    fighter.currentLife -= Mathf.Abs(residualBars) * 5;
        //    lifeBar.damage = Mathf.Abs(residualBars) * 5;
        //    lifeBar.LessenMyLife();
        //    OnDeath();
        //}
        //residualBars = 0;

        //Implementate Later!
    }
    void Contact()
    {
            fighter.currentLife -= dataBase.enemyAnimationScript.lastDamage;
            lifeBar.damage = dataBase.enemyAnimationScript.lastDamage;
            lifeBar.LessenMyLife();
            OnDeath();
    }

    void SetRunUp()
    {
        if (fighter.isInEnd && !overrideMissile)
        {
            if(hitPerformed != 13 && hitPerformed != 14 && hitPerformed != 15 && hitPerformed != 12)
            {
                runUp = Vector2.right * -dataBase.enemyFighter.lookDirection * dataBase.enemyDamageScript.knockBacks[dataBase.enemyDamageScript.hitDone];
            }
        }
        else if (fighter.isInEnd && overrideMissile)
        {
            if (!lastBoolOnMissile)
            {
                runUp = Vector2.right * -dataBase.enemyFighter.lookDirection * 600;
            }
            else
            {
                runUp = Vector2.zero;
            }
        }
        else if (!fighter.isInEnd)
        {
            runUp = Vector2.zero;
        }
    }
    void ApplyRunUp()
    {
        dataBase.enemyRb.AddForce(runUp);
    }
    void CheckHit()
    {
        OnDeath();
        dataBase.myFighter.canMove = false;
        switch (hitPerformed)
        {
            case 13: //In Case Attack Is Upperer
                if (!dataBase.myFighter.isBlock)
                {
                    Contact();
                    dataBase.myAnimationScript.Air_Hit();
                    dataBase.enemyAttackScript.hasHit = dataBase.enemyAttackScript.hasAIHit = true;
                    dataBase.enemyAttackScript.ComboTracker();
                }

                if (!overrideMissile)
                {
                    if (!fighter.isBlock)
                    {
                        dataBase.myRb.velocity = Vector2.zero;
                        dataBase.myRb.AddForce(Vector3.up * 3000);
                    }
                    else
                    {
                        dataBase.myRb.AddForce(actualKnockBack * 600);
                    }
                }

                else if (overrideMissile && !fighter.isInEnd)
                {
                    dataBase.myRb.AddForce(actualKnockBack * 600);
                }
                ApplyRunUp();
                overrideMissile = false;
                break;

            case 14:
                if (!dataBase.myFighter.isBlock)
                {
                    Contact();
                    dataBase.myAnimationScript.Air_Hit();
                    dataBase.enemyAttackScript.hasHit = dataBase.enemyAttackScript.hasAIHit = true;
                    dataBase.enemyAttackScript.ComboTracker();
                }

                if (!fighter.isInEnd && !overrideMissile)
                {
                    if (!fighter.isBlock)
                    {
                        dataBase.myRb.AddForce(Vector3.right * 600 * -dataBase.myFighter.lookDirection);
                    }
                    else
                    {
                        dataBase.myRb.AddForce(actualKnockBack * 600);
                    }
                }

                else if (overrideMissile && !fighter.isInEnd)
                {
                    dataBase.myRb.AddForce(actualKnockBack * 600);
                }
                ApplyRunUp();
                overrideMissile = false;
                break;


            case 15:
                if (!dataBase.myFighter.isBlock)
                {
                    Contact();
                    ChooseAnim();
                    dataBase.enemyAttackScript.hasHit = dataBase.enemyAttackScript.hasAIHit = true;
                    dataBase.enemyAttackScript.ComboTracker();
                }
                break;

            default:
                if (numberOfHits <= 0 && specialMissile || specialMissile == false)
                {
                    if (specialMissile)
                    {
                        numberOfHits++;
                    }

                    if (!dataBase.myFighter.isBlock)
                    {
                        Contact();
                        ChooseAnim();
                        dataBase.enemyAttackScript.hasHit = dataBase.enemyAttackScript.hasAIHit = true;
                        dataBase.enemyAttackScript.ComboTracker();
                    }

                    if (!fighter.isInEnd && !overrideMissile)
                    {
                        if (!fighter.isBlock)
                        {
                            dataBase.myRb.AddForce(actualKnockBack * dataBase.enemyDamageScript.knockBacks[hitPerformed]);
                            StopCoroutine("GenerateDust");
                            if (dataBase.enemyDamageScript.knockBacks[hitPerformed] > 600)
                            {
                                StartCoroutine("GenerateDust");
                            }
                        }
                        else
                        {
                            dataBase.myRb.AddForce(actualKnockBack * 600);
                        }
                    }
                    else if (overrideMissile && !fighter.isInEnd)
                    {
                        dataBase.myRb.AddForce(actualKnockBack * 600);
                    }
                    ApplyRunUp();
                    overrideMissile = false;
                }
                break;
        }
    }
    void OnDeath()
    {
        if (fighter.currentLife <= 0 && matchIsOn == true)
        {
            fighter.isAlive = false;
            if (fighter.isGrounded && hitPerformed != 13 && hitPerformed != 14)
            {
                dataBase.myAnimationScript.Death();
            }
            else
            {
                dataBase.myAnim.SetTrigger("AirDeath");
            }
            fighter.currentLife = 0;
            controlScript.OnWin(dataBase.playerNumber, false);
            matchIsOn = false;
        }
    }

    public IEnumerator Sickness()
    {
        fighter.isInverted = true;
        dizzy.Play();
        yield return new WaitForSeconds(4);
        fighter.isInverted = false;
        dizzy.Stop();
    }

    public void ChooseAnim()
    {
        if (!fighter.isCrouch)
        {
            anim++;
            if (anim > 3)
            {
                anim = 0;
            }
            switch (anim)
            {
                case 0:
                    dataBase.myAnimationScript.Hit_1();
                    break;
                case 1:
                    dataBase.myAnimationScript.Hit_2();
                    break;
                case 2:
                    dataBase.myAnimationScript.Hit_3();
                    break;
                case 3:
                    dataBase.myAnimationScript.Hit_4();
                    break;
            }
        }
        else
        {
            dataBase.myAnimationScript.CrouchHit();
        }
    }
    public IEnumerator GenerateDust()
    {
        yield return new WaitForSeconds(0.04f);
        while (Mathf.Abs(dataBase.myRb.velocity.x) > 0)
        {
            effex.KnockBacked(fighter.playerNumber);
            yield return new WaitForSeconds(0.08f);
        }
        yield break;
    }
}