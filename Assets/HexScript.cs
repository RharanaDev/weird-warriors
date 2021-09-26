using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexScript : MonoBehaviour
{
    // Start is called before the first frame update
    public InstanceSaver leo;
    public float target;
    public float speed;
    public Vector3 myMoves;
    bool isOnPos;
    bool isMoving;
    public Animator anim;
    void Start()
    {
        myMoves = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(leo.myRb.velocity.x) > 0 && leo.myAnimationScript.catSituationLeo != 1)
        {
            anim.SetInteger("TypeOfAction", 2);
            speed = 13;
        }
        else if (leo.myRb.velocity.x == 0)
        {
            anim.SetInteger("TypeOfAction", 1);
            speed = 8;
        }


        if (Mathf.Abs(transform.position.x - leo.playerMe.transform.position.x) > 1)
        {
            transform.position = (myMoves * Mathf.MoveTowards(transform.position.x, leo.playerMe.transform.position.x, speed * Time.deltaTime)) + (Vector3.up * -8.14f);
            isMoving = true;
        }
        else
        {
            isMoving = false;
            transform.rotation = leo.playerMe.transform.rotation;
        }
        anim.SetBool("IsRunning", isMoving);
    }
}
