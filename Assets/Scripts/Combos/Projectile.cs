using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public InstanceSaver database;
    public Rigidbody2D rb;
    public float velocity = 5f;
    public float longing;
    public int projectiletipe;
    public Animator anim;
    public bool hasDeathTime;
    public float deathtime;
    private bool hasYetDestroyed;
    public bool doesntExplode, isDestroyable;
    public int collisionCount;
    public CircleCollider2D circle;
    public bool isntInstant;
    public float magicIsdone;
    public bool fuckem;
    public string[] explodeWhenNear;
    public bool explodeOrNah;
    public bool specialMissile;
    public float time1, timeIncrement, timeLeft, time2;
    public bool boomerangBack;
    public bool specialProjectile;
    public int hitType, lastDamage, lastEffectType, attackHardness;
    public float lastX, lastY;
    public TiltEffect tilter;
    // Start is called before the first frame update
    void Awake()
    {
        tilter = GameObject.Find("TiltingEffect").GetComponent<TiltEffect>();
    }
    void Start()
    {
        isntInstant = false;
        StopCoroutine("CheckInstant");
        StartCoroutine("CheckInstant");
        switch (projectiletipe) 
        {
            case 1:
                MagicMissile();
                break;
            case 2:
                StartCoroutine("Boomerang", false);
                break;
            case 3:
                MagicMissile();
                break;
            case 4:
                Spit();
                break;
            case 5:
                fuckem = true;
                StartCoroutine("FuckFran");
                break;
        }
        if (hasDeathTime)
        {
            StartCoroutine(TimeToDie());
        }
    }
    IEnumerator CheckInstant()
    {
        yield return new WaitForSeconds(0.02f);
        isntInstant = true;
        yield break;
    }
    public void Spit()
    {
        StartCoroutine("FallSpit");
        rb.velocity = transform.right * velocity;
    }
    public IEnumerator FallSpit()
    {
        yield return new WaitForSeconds(0.02f);
        rb.isKinematic = false;
        rb.velocity = (transform.right * velocity / 4 - transform.up * 18f);
    }
    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (specialProjectile && other.gameObject.name == "HitBox" && collisionCount == 0)
        {
            collisionCount++;
            rb.velocity = Vector2.zero;
            if (projectiletipe == 2)
            {
                if (boomerangBack)
                {
                    timeIncrement = longing - (Time.timeSinceLevelLoad - time2);
                    StopCoroutine("Boomerang");
                    anim.SetTrigger("PRRRT");
                }
                else
                {
                    timeIncrement = Time.timeSinceLevelLoad - time1;
                    StopCoroutine("Boomerang");
                    anim.SetTrigger("PRRRT");
                }
            }
            else if(projectiletipe == 1)
            {
                anim.SetTrigger("PRRRT");
            }
        }

        foreach(string str in explodeWhenNear)
        {
            if (other.CompareTag(str))
            {
                explodeOrNah = true;
            }
        }

        if (explodeOrNah)
        {
            if (!isDestroyable)
            {
                if (!hasYetDestroyed)
                {
                    switch (projectiletipe)
                    {
                        case 1:
                            yield return new WaitForSeconds(0.04f);
                            if (!doesntExplode)
                            {
                                anim.SetTrigger("Explosion");
                            }
                            rb.velocity = Vector2.zero;

                            Destroy(rb.gameObject, 0.15f);
                            break;
                        case 2:
                            break;
                        case 3:
                            yield return new WaitForSeconds(0.04f);
                            rb.velocity = Vector2.zero;
                            if (!doesntExplode)
                            {
                                anim.SetTrigger("Explosion");
                            }
                            Destroy(rb.gameObject, 0.15f);
                            break;
                        case 4:
                            StopCoroutine("Fallspit");
                            rb.velocity = Vector2.zero;
                            Destroy(rb.gameObject, 0.08f);
                            break;
                        case 5:
                            yield return new WaitForSeconds(0.04f);
                            fuckem = false;
                            rb.velocity = Vector2.zero;
                            if (!doesntExplode)
                            {
                                anim.SetTrigger("Explosion");
                            }
                            Destroy(rb.gameObject, 0.15f);
                            break;
                    }
                    hasYetDestroyed = true;
                }
            }
        }
    }

    IEnumerator Boomerang(bool phase2)
    {
        if (phase2)
        {

            rb.velocity = transform.right * 1.5f * -velocity;
            yield return new WaitForSeconds(timeIncrement / 1.5f);
            database.myAnim.SetTrigger("CatchHead");
            Destroy(rb.gameObject);
        }
        else
        {
            time1 = Time.timeSinceLevelLoad;
            rb.velocity = transform.right * velocity;
            yield return new WaitForSeconds(longing);
            rb.velocity = transform.right * 1.5f * -velocity;
            boomerangBack = true;
            time2 = Time.timeSinceLevelLoad;
            yield return new WaitForSeconds(longing / 1.5f);
            database.myAnim.SetTrigger("CatchHead");
            Destroy(rb.gameObject);
        }
    }
    void MagicMissile()
    {
        rb.velocity = transform.right * velocity;
    }
    IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(deathtime);
        if (!hasYetDestroyed) 
        {
            rb.velocity = Vector2.zero;
            if (!doesntExplode)
            {
                anim.SetTrigger("Explosion");
            }
            Destroy(rb.gameObject, 0.2f);
            hasYetDestroyed = true;
        }
    }
    public void FromAnimator(int phase)
    {
        switch (phase)
        {
            case 0:
                rb.velocity = Vector2.zero;
                break;
            case 1:
                StartCoroutine("Boomerang", true);
                break;
        }
    }
    public void Instadestroy()
    {
        database.myAnimationScript.cantChangeInstantVars = false;
        database.enemyAnimationScript.cantChangeInstantVars = false;
        Destroy(rb.gameObject);
    }

    public void TiltUp(int HowMuchBars)
    {
        tilter.StartCoroutine("Hey", HowMuchBars);
    }

    public void TypeOfHit(int type)
    {
        hitType = type;
    }


    public void CustomEffectNeeded(int effect)
    {
        switch (effect)
        {
            case 0:
                lastEffectType = database.myFighter.effect[0];
                break;
            case 1:
                lastEffectType = database.myFighter.effect[1];
                break;
            case 2:
                lastEffectType = database.myFighter.effect[2];
                break;
            case 3:
                lastEffectType = database.myFighter.effect[3];
                break;
            case 4:
                lastEffectType = database.myFighter.effect[4];
                break;
            case 5:
                lastEffectType = database.myFighter.effect[5];
                break;
            case 6:
                lastEffectType = 8;
                break;
        }

    }
    public void ConcreteEffectX(float x)
    {
        lastX = x;
    }

    public void ConcreteEffectY(float y)
    {
        lastY = y;
    }

    public void LastDamage(int Damage)
    {
        lastDamage = Damage;
    }

    public void Hardness(int hardness)
    {
        attackHardness = hardness;
    }
}
