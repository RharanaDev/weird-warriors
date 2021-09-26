using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public FightersData fighter;
    public InstanceSaver dataBase;
    public int direction = 0;
    public string myTag;
    public bool pusheen;
    bool collEnteredOnIgnore;
    BoxCollider2D boxco;
    bool firstEntryTime;
    public bool booleo;
    public int situation;
    private void Start()
    {
        switch (fighter.playerNumber)
        {
            case 1:
                myTag = "PlayerTwo";
                break;
            case 2:
                myTag = "Player";
                break;
        }
    }
    void Update()
    {
        if (booleo)
        {
            if (dataBase.enemy.transform.position.x >= transform.position.x && !fighter.isInEnd)
            {
                direction = -1;
            }
            else if (dataBase.enemy.transform.position.x < transform.position.x && !fighter.isInEnd)
            {
                direction = 1;
            }
            else if (fighter.isInEnd && transform.position.x < 0)
            {
                direction = -1;
            }
            else if (fighter.isInEnd && transform.position.x > 0)
            {
                direction = 1;
            }
        }
        else if (!booleo)
        {
            direction = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(myTag))
        {
            if (dataBase.enemyFighter.canBeRepeled && !dataBase.enemyFighter.isGrounded && EmpujeScript.isPushingAllowed && !PlayerGeneralMoveController.specialBooleo)
            {
                //Debug.Log("Care");
                situation = 1;
                booleo = true;
            }
            else
            {
                //Debug.Log("DontCare");
                situation = 2;
                booleo = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(myTag))
        {
            if (situation == 2)
            {
                //Debug.Log("IgnoreEverything");
                booleo = false;
            }
            else if (situation == 1)
            {
                //Debug.Log("DoSomething");
                booleo = false;
                dataBase.myRb.velocity = new Vector2(0, dataBase.myRb.velocity.y);
                dataBase.enemyRb.velocity = new Vector2(0, dataBase.enemyRb.velocity.y);
            }
        }
    }
}
