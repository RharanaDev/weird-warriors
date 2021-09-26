using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GentaiProj : MonoBehaviour
{
    public Vector3 pos;
    public Transform trans;
    public string target;
    public InstanceSaver fatherlyInstances;
    int mode;
    public Animator anim;
    public int id, player;
    public int direction;
    public float ascendVel, followVel;
    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public float timeAscending;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(fatherlyInstances.myBoxCo, cc);
        mode = 1;
        ChangeMode();
        StartCoroutine(timeAscend());
        trans = GameObject.FindGameObjectWithTag(target).transform;
    }
    private void Update()
    {
        if(transform.position.y <= -3)
        {
            anim.SetTrigger("Explode");
            StartCoroutine("StopVelocityFade");
            Destroy(this.gameObject, 0.5f);
        }
    }
    // Update is called once per frame
    IEnumerator timeAscend()
    {
        yield return new WaitForSeconds(timeAscending);
        mode = 2;
        CalculateNewPos();
        ChangeMode();
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.5f);
        mode = 3;
        ChangeMode();
    }
    void Numbered(int number)
    {
        id = number;
    }
    void CalculateNewPos()
    {
        if (id == 2)
        {
            pos = trans.position + (Vector3.up * 14) + Vector3.right * -1.5f * fatherlyInstances.enemyFighter.lookDirection;
        }
        else if (id == 3)
        {
            pos = trans.position + (Vector3.up * 14) + Vector3.right * -3f * fatherlyInstances.enemyFighter.lookDirection;
        }
        else
        {
            pos = trans.position + (Vector3.up * 14);
        }
    }
    void ChangeMode()
    {
        switch (mode)
        {
            case 1: //FULLUP
                rb.velocity = Vector2.up * ascendVel;
                break;
            case 2:
                rb.velocity = Vector2.zero;
                transform.position = pos;
                StartCoroutine("Pause");
                break;
            case 3:
                rb.velocity = Vector2.down * 3 *ascendVel;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HurtBoxPlayer" + fatherlyInstances.enemyFighter.playerNumber))
        {

            anim.SetTrigger("Explode");
            StartCoroutine("StopVelocityFade");
            Destroy(this.gameObject, 0.5f);
        }
    }
    IEnumerator StopVelocityFade()
    {
        yield return new WaitForSeconds(0.02f);
        rb.velocity = Vector3.zero;
    }
}
