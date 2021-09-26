using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffects : MonoBehaviour
{
    public InstanceSaver dataBase1, dataBase2;
    public GameObject bloodParticle, boneParticle, effect, bleedParticle1, bleedParticle2, blockParticle;
    public GameObject dust, jumpDust, jumpForwardDust, knockDust;
    public GameObject spoof, firework, purple, normal, big, midBlue, bigBlue, whenBlock;
    private Transform player1, player2;
    public Vector3 offset;
    public void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player").transform;
        player2 = GameObject.FindGameObjectWithTag("PlayerTwo").transform;

        switch (dataBase1.myFighter.ID)
        {
            case 1: //insert WakanTanka's ID
                bleedParticle1 = boneParticle;
                break;
            default:
                bleedParticle1 = bloodParticle;
                break;
        }
        switch (dataBase2.myFighter.ID)
        {
            case 1: //insert WakanTanka's ID
                bleedParticle2 = boneParticle;
                break;
            default:
                bleedParticle2 = bloodParticle;
                break;
        }
    }
    public void SplashEffect(int playerNumber, int type)
    {
        GameObject objectToErase, partic1, partic2;

        if (type == 1)
        {
            partic1 = blockParticle;
            partic2 = blockParticle;
        }
        else
        {
            partic1 = bleedParticle1;
            partic2 = bleedParticle2;
        }

        switch (playerNumber)
        {
            case 1:
                objectToErase = Instantiate(partic1, player1.position + offset, Quaternion.Euler(0, -50 * dataBase1.myFighter.lookDirection, 0));
                Destroy(objectToErase, 3);
                break;
            case 2:
                objectToErase = Instantiate(partic2, player2.position + offset, Quaternion.Euler(0, -50 * dataBase2.myFighter.lookDirection, 0));
                Destroy(objectToErase, 3);
                break;
        }
    }
    public void Effects(int playerNumber, int effectType, float lastX, float lastY)
    {
        bool over = false;
        GameObject objectToErase;
        offset = new Vector3(lastX * dataBase1.myFighter.lookDirection, lastY, 0);
        switch (effectType)
        {
            case 1:
                effect = normal;
                break;
            case 2:
                effect = big;
                break;
            case 3:
                effect = purple;
                break;
            case 4:
                effect = firework;
                break;
            case 5:
                effect = spoof;
                break;
            case 6:
                effect = midBlue;
                break;
            case 7:
                effect = bigBlue;
                break;
            case 8:
                over = true;
                break;
            case 9:
                effect = whenBlock;
                break;
        }
        if (!over)
        {
            switch (playerNumber)
            {
                case 1:
                    objectToErase = Instantiate(effect, player1.position + offset, player1.rotation * Quaternion.Euler(0, 180, 0));
                    Destroy(objectToErase, 2);
                    break;
                case 2:
                    objectToErase = Instantiate(effect, player2.position + offset, player2.rotation * Quaternion.Euler(0, 180, 0));
                    Destroy(objectToErase, 2);
                    break;
            }
        }
        over = false;
    }
    public void Dust(int playerNumber, int place)
    {
        Quaternion useMe = Quaternion.Euler(0, 180, 0);
        Quaternion useMe2 = Quaternion.Euler(0, 0, 0);
        GameObject objectToErase;

        if (place == 1)
        {
            useMe = Quaternion.Euler(0, 0, 0);
            useMe2 = Quaternion.Euler(0, 180, 0);
        }

        switch (playerNumber)
        {
            case 1:
                switch (place)
                {
                    case 0:
                        objectToErase = Instantiate(dust, player1.position + dataBase1.myFighter.backDustOffset * -dataBase1.myFighter.lookDirection, player1.rotation * useMe);
                        Destroy(objectToErase, 0.7f);
                        break;
                    case 1:
                        objectToErase = Instantiate(dust, player1.position + dataBase1.myFighter.frontDustOffset * dataBase1.myFighter.lookDirection, player1.rotation * useMe);
                        Destroy(objectToErase, 0.7f);
                        break;
                }

                break;
            case 2:
                switch (place)
                {
                    case 0:
                        objectToErase = Instantiate(dust, player2.position + dataBase2.myFighter.backDustOffset * -dataBase2.myFighter.lookDirection, player2.rotation * useMe);
                        Destroy(objectToErase, 0.7f);
                        break;
                    case 1:
                        objectToErase = Instantiate(dust, player2.position + dataBase2.myFighter.frontDustOffset * dataBase2.myFighter.lookDirection, player2.rotation * useMe);
                        Destroy(objectToErase, 0.7f);
                        break;
                }
                break;
        }
    }
    public void Jumped(int playerNumber)
    {
        GameObject objectToErase;
        switch (playerNumber)
        {
            case 1:
                objectToErase = Instantiate(jumpDust, player1.position, transform.rotation);
                Destroy(objectToErase, 0.7f);
                break;
            case 2:
                objectToErase = Instantiate(jumpDust, player2.position, transform.rotation);
                Destroy(objectToErase, 0.7f);
                break;
        }
    }
    public void ForwardJumped(int playerNumber, int place)
    {
        Quaternion useMe = Quaternion.Euler(0, 0, 0);
        GameObject objectToErase;

        if (place == 1)
        {
            useMe = Quaternion.Euler(0, 180, 0);
        }

        switch (playerNumber)
        {
            case 1:
                switch (place)
                {
                    case 0:
                        objectToErase = Instantiate(jumpForwardDust, player1.position + dataBase1.myFighter.backDustOffset * -dataBase1.myFighter.lookDirection, player1.rotation * useMe);
                        Destroy(objectToErase, 0.7f);
                        break;
                    case 1:
                        objectToErase = Instantiate(jumpForwardDust, player1.position + dataBase1.myFighter.frontDustOffset * dataBase1.myFighter.lookDirection, player1.rotation * useMe);
                        Destroy(objectToErase, 0.7f);
                        break;
                }

                break;
            case 2:
                switch (place)
                {
                    case 0:
                        objectToErase = Instantiate(jumpForwardDust, player2.position + dataBase2.myFighter.backDustOffset * -dataBase2.myFighter.lookDirection, player2.rotation * useMe);
                        Destroy(objectToErase, 0.7f);
                        break;
                    case 1:
                        objectToErase = Instantiate(jumpForwardDust, player2.position + dataBase2.myFighter.frontDustOffset * dataBase2.myFighter.lookDirection, player2.rotation * useMe);
                        Destroy(objectToErase, 0.7f);
                        break;
                }
                break;
        }
    }
    public void KnockBacked(int playerNumber)
    {
        Quaternion useMe = Quaternion.Euler(0, 0, 0);
        GameObject objectToErase;

        switch (playerNumber)
        {
            case 1:
                objectToErase = Instantiate(knockDust, player1.position + (dataBase1.myFighter.frontDustOffset/ 2) * dataBase1.myFighter.lookDirection, player1.rotation * useMe);
                Destroy(objectToErase, 1);
                break;
            case 2:
                objectToErase = Instantiate(knockDust, player2.position + (dataBase1.myFighter.frontDustOffset / 2) * dataBase2.myFighter.lookDirection, player2.rotation * useMe);
                Destroy(objectToErase, 1);
                break;
        }
    }
}
