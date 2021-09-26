using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSonScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Animator anim;
    public InstanceSaver instances;
    public float timeLife;
    bool alreadyDone;
    public string hurty;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector2.right * speed * instances.myFighter.lookDirection;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(hurty) && !alreadyDone)
        {
            alreadyDone = true;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Swallow");
        }
    }
    public void ToZero()
    {
        rb.velocity = Vector2.zero;
    }

    public void Humanize()
    {
        instances.myAnimationScript.SpecialHemogoblin();
    }
    public void EndOfLife()
    {
        instances.myAnimationScript.cantChangeInstantVars = false;
        instances.enemyAnimationScript.cantChangeInstantVars = false;
        Destroy(gameObject);
    }
}
