using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    public Sprite[] sprite = new Sprite[6];
    public SpriteRenderer rend, rend2;
    public Animator onoAnim;
    public Transform transfy;
    public Vector3 outScreenPos, inScreenPos;
    public Vector3 target;
    public bool isOnScreen, isOutScreen, isRetiring, isEntering;
    public float speed;
    public bool erase;
    public bool targetAcquired;
    public Transform Hit;
    public bool HitOnScreen;
    public bool secondBool;
    public Vector3 offset;
    public int previousCombo;
    public int lastcombo;
    public Transform cyphTransf1, cyphTransf2;
    bool amazing;
    // Start is called before the first frame update
    public void Start()
    {
        isOutScreen = true;
    }
    private void FixedUpdate()
    {

        // Action Loop

        if (transfy.localPosition != target && isRetiring || transfy.localPosition != target && isEntering)
        {
            transfy.localPosition = Vector3.MoveTowards(transfy.localPosition, target, speed * Time.fixedDeltaTime);
        }

        // Targets of V3.Mvtwrds

        if (isRetiring && !targetAcquired)
        {
            target = outScreenPos;
            targetAcquired = true;
        }
        else if (isEntering && !targetAcquired)
        {
            target = inScreenPos;
            targetAcquired = true;
        }

        // Out Or In?

        else if (transfy.localPosition == target && isRetiring)
        {
            isOutScreen = true;
            isRetiring = false;
            targetAcquired = false;
        }
        else if (transfy.localPosition == target && isEntering)
        {
            isOnScreen = true;
            isEntering = false;
            targetAcquired = false;
        }
    }

    public void ChangeOfCombo()
    {
        if (lastcombo >= 5 && lastcombo < 7)
        {
            onoAnim.SetTrigger("Nice");
        }
        else if(lastcombo >= 7 && lastcombo < 9)
        {
            onoAnim.SetTrigger("Great");
        }
        else if (lastcombo >= 9)
        {
            onoAnim.SetTrigger("Amazing");
        }
    }
    public void SpriteChange(int combo)
    {
        int decimalCypher, cypher;
        lastcombo = combo;
        decimalCypher = Mathf.FloorToInt(combo / 10);
        cypher = combo - (decimalCypher * 10);
        if(combo <= 9)
        {
            cyphTransf1.localPosition = Vector3.zero;
            cyphTransf2.localPosition = Vector3.right * -0.93f;
        }
        else if (combo > 9)
        {
            cyphTransf2.localPosition = Vector3.zero;
            cyphTransf1.localPosition = Vector3.right * 0.86f;
        }
        switch (cypher)
        {
            case 0:
                rend.sprite = sprite[0];
                break;
            case 1:
                rend.sprite = sprite[1];
                break;
            case 2:
                rend.sprite = sprite[2];
                break;
            case 3:
                rend.sprite = sprite[3];
                break;
            case 4:
                rend.sprite = sprite[4];
                break;
            case 5:
                rend.sprite = sprite[5];
                break;
            case 6:
                rend.sprite = sprite[6];
                break;
            case 7:
                rend.sprite = sprite[7];
                break;
            case 8:
                rend.sprite = sprite[8];
                break;
            case 9:
                rend.sprite = sprite[9];
                break;
        }
        switch (decimalCypher)
        {
            case 1:
                rend2.sprite = sprite[1];
                break;
            case 2:
                rend2.sprite = sprite[2];
                break;
            case 3:
                rend2.sprite = sprite[3];
                break;
            case 4:
                rend2.sprite = sprite[4];
                break;
            case 5:
                rend2.sprite = sprite[5];
                break;
            case 6:
                rend2.sprite = sprite[6];
                break;
            case 7:
                rend2.sprite = sprite[7];
                break;
            case 8:
                rend2.sprite = sprite[8];
                break;
            case 9:
                rend2.sprite = sprite[9];
                break;
        }
    }
}
