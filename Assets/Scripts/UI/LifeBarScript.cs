using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarScript : MonoBehaviour
{
    public float target, actual, mine, velocity, velocity2;
    public float destinyPoint, damage, myLife, firstPos, anotherFirstPos;
    public SpriteRenderer sprite;
    public Transform another;
    public float time;
    public bool timeHasPassed, coroutineRestarted;
    public int direction;
    public float accumulatedDamage;

    // Start is called before the first frame update
    void Awake()
    {
        firstPos = transform.localPosition.x;
        anotherFirstPos = another.localPosition.x;
        destinyPoint = transform.localPosition.x;
    }

    public void RestartBar()
    {
        transform.localPosition = new Vector3(firstPos, transform.localPosition.y, transform.localPosition.z);
        another.localPosition = new Vector3(anotherFirstPos, another.localPosition.y, another.localPosition.z);
        destinyPoint = transform.localPosition.x;
        sprite.enabled = true;
    }

    void Update()
    {
        if(transform.localPosition.x < firstPos + target)
        {
            mine = Mathf.MoveTowards(transform.localPosition.x, destinyPoint, velocity * Time.deltaTime);
            transform.localPosition = new Vector3(mine, transform.localPosition.y, transform.localPosition.z);
        }
        if (timeHasPassed && another.localPosition != transform.localPosition && !coroutineRestarted)
        {
            another.localPosition = Vector3.MoveTowards(another.localPosition, transform.localPosition, velocity2 * (accumulatedDamage /2) * Time.deltaTime);
        }
        else if (timeHasPassed && another.localPosition == transform.localPosition)
        {
            timeHasPassed = false;
            accumulatedDamage = 0;
        }
        else if(transform.localPosition.x >= firstPos + target)
        {
            sprite.enabled = false;
        }
    }
    public void LessenMyLife()
    {
        if (destinyPoint != transform.localPosition.x + target)
        {
            myLife -= damage;
            destinyPoint = transform.localPosition.x + (direction * target / 100) * damage;
            StopCoroutine("FancyEffect");
            StartCoroutine("FancyEffect");
        }
    }
    IEnumerator FancyEffect()
    {
        accumulatedDamage += damage;
        coroutineRestarted = true;
        yield return new WaitForSeconds(time);
        timeHasPassed = true;
        coroutineRestarted = false;
    }
}
