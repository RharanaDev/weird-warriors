using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClashScript : MonoBehaviour
{
    public static int playersAttacking;
    public int timeUntilFrame;
    public int localTime;
    public int deltaFrameP1 = 0;
    public int deltaFrameP2 = 0;
    bool oneActive, twoActive, oneAttacking, twoAttacking;
    public int neededFrame1, neededFrame2;
    public int frameDuration1, frameDuration2;
    public bool thisIsClash;
    private void FixedUpdate()
    {
        if (oneAttacking)
        {
            deltaFrameP1++;
            if (deltaFrameP1 >= neededFrame1 && deltaFrameP1 <= (frameDuration1 + neededFrame1))
            {
                oneActive = true;
            }
            else if (deltaFrameP1 > (frameDuration1 + neededFrame1))
            {
                oneActive = false;
                oneAttacking = false;
                deltaFrameP1 = 0;
            }
        }

        if (twoAttacking)
        {
            deltaFrameP2++;
            if (deltaFrameP2 >= neededFrame2 && deltaFrameP2 <= (frameDuration2 + neededFrame2))
            {
                twoActive = true;
            }
            else if (deltaFrameP2 > (frameDuration2 + neededFrame2))
            {
                twoActive = false;
                twoAttacking = false;
                deltaFrameP2 = 0;
            }
        }

        if(oneActive && twoActive)
        {
            thisIsClash = true;
        }
        else if(thisIsClash && !oneActive && !twoActive)
        {
            thisIsClash = false;
        }
        if (!oneActive && !twoActive)
        {
            PlayerDamageController.OverrideAllOthers = false;
        }
    }
    public void AttackCall(int player, int whenActive, int duration)
    {
        switch (player)
        {

            case 1:
                frameDuration1 = duration;
                neededFrame1 = whenActive;
                oneAttacking = true;
                deltaFrameP1 = 0;
                break;
            case 2:
                frameDuration2 = duration;
                neededFrame2 = whenActive;
                twoAttacking = true;
                deltaFrameP2 = 0;
                break;
        }
    }
    public bool isThisFrameClash()
    {
        return thisIsClash;
    }
}
