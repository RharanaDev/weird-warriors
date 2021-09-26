using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    public bool betweenBonds;
    public Vector3 bond1, bond2, currentBond;
    public float speed;
    bool isCurrentlyMoving;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        bond1 = GameObject.Find("CatBond1").transform.position;
        bond2 = GameObject.Find("CatBond2").transform.position;
        currentBond = bond1;
        isCurrentlyMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != currentBond && isCurrentlyMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentBond, speed * Time.deltaTime);
        }
        else if(transform.position == bond1)
        {
            Rotate(2);
        }
        else if (transform.position == bond2)
        {
            Rotate(1);
        }
    }

    void Rotate(int bond)
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        switch (bond)
        {
            case 1:
                currentBond = bond1;
                break;
            case 2:
                currentBond = bond2;
                break;
        }
    }
    void StopMoving(int isMoving)
    {
        switch (isMoving)
        {
            case 0:
                isCurrentlyMoving = false;
                break;
            case 1:
                isCurrentlyMoving = true;
                break;
        }
    }
    void RandomizeActions()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                anim.SetTrigger("Sleep");
                break;
            //case 1:
            //    break;
            //case 2:
            //    break;
            //case 3:
            //    break;
            //case 4:
            //    break;
            //case 5:
            //    break;
        }
    }
}
