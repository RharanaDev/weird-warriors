using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    public float target, velocity;
    public float destinyPoint, manaFloat, firstPos;
    public bool suitch;
    public Transform another;
    public float time;
    public bool timeHasPassed, coroutineRestarted;
    public bool manaLost;
    public int direction;
    public float accumulatedMana;

    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.localPosition.x;
        destinyPoint = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        destinyPoint = firstPos + (direction * target / 100) * manaFloat;
        transform.localPosition = new Vector3(destinyPoint, transform.localPosition.y, transform.localPosition.z);

        if (manaLost && timeHasPassed && !coroutineRestarted)
        {
            another.localPosition = Vector3.MoveTowards(another.localPosition, transform.localPosition, velocity * (accumulatedMana / 2) * Time.deltaTime);
            if (another.localPosition.x == transform.localPosition.x)
            {
                accumulatedMana = 0;
                manaLost = false;
            }
        }
        else if (!manaLost && !coroutineRestarted)
        {
            another.localPosition = transform.localPosition;
        }
    }
    IEnumerator FancyEffect(float manaToLessen)
    {
        accumulatedMana += manaToLessen;
        timeHasPassed = false;
        coroutineRestarted = true;
        yield return new WaitForSeconds(time);
        timeHasPassed = true;
        coroutineRestarted = false;
    }
    public void LessenSomeMana(float manaToLessen)
    {
        manaLost = true;
        StopCoroutine("FancyEffect");
        StartCoroutine("FancyEffect", manaToLessen);
    }
}
