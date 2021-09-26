using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpujeScript : MonoBehaviour
{
    public PushDetection pushy;
    private float height;
    public InstanceSaver dataBase;
    public InstanceSaver enemyDataBases;
    public FightersData fighter;
    public bool repel;
    private Vector2 displacement;
    public float offset;
    public float alture;
    public float normalAlture, otherAlture;
    public static bool isPushingAllowed = true;
    public bool imBeingPushed;
    public int pushedDir;
    public float repelSpeed;
    public bool doneOnce;
    void LateUpdate()
    {   
        DeterminatePush();
    }
    private void DeterminatePush()
    {
        if (isPushingAllowed)
        {
            if (dataBase.myDetection.direction != 0 && fighter.canBeRepeled == true && dataBase.enemyFighter.canBeRepeled == true)
            {
                    if (dataBase.myDetection.direction >= 0)
                    {
                        displacement = new Vector2(repelSpeed, 0); //rightmove
                        repel = true;
                    }
                    else if (dataBase.myDetection.direction < 0)
                    {
                        displacement = new Vector2(-repelSpeed, 0); //leftmove
                        repel = true;
                    }
                    Repel();
            }
        }
        else
        {
            repel = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && !collision.gameObject.CompareTag("floor"))
        {
            fighter.isInEnd = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && !collision.gameObject.CompareTag("floor"))
        {
            fighter.isInEnd = true;
        }
    }
    public void Repel()
    {
        dataBase.myRb.velocity = new Vector2(displacement.x, dataBase.myRb.velocity.y);
        dataBase.enemyRb.velocity = new Vector2(-displacement.x, dataBase.enemyRb.velocity.y);
    }
} 
