using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakanCombos : MonoBehaviour
{
    public InstanceSaver dataBase;
    public FightersData fighter;
    public bool isAdvancing;
    public float howMuchAdvance;
    public float speed;
    public Animator anim;

    void FixedUpdate()
    {
        howMuchAdvance = speed * fighter.lookDirection * Time.fixedDeltaTime;
        if (isAdvancing)
        {
            dataBase.myRb.MovePosition(dataBase.myRb.position + new Vector2(howMuchAdvance, 0));
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "HitBox" && isAdvancing == true)
        {
            StopCoroutine("NaturalPause");
            StartCoroutine("NaturalPause");
        }
    }
    IEnumerator NaturalPause()
    {
        yield return new WaitUntil(() => Mathf.Abs(dataBase.enemy.transform.position.x - dataBase.playerMe.transform.position.x) <= 4);
        isAdvancing = false;
    }
}
