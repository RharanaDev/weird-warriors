using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBarScript : MonoBehaviour
{
    public float firstPos, secondPos, distance, marker;
    public int originalRes, resistance;
    public bool securityBool;
    public Sprite[] arraySprites;
    public SpriteRenderer myRend;
    private void Start()
    {
        firstPos = transform.localPosition.x;
        marker = firstPos;
        distance = secondPos - firstPos;
    }

    private void Update()
    {
        if (marker != firstPos + (distance / originalRes * (originalRes - resistance)))
        {
            securityBool = false;
            marker = Mathf.MoveTowards(marker, firstPos + (distance / originalRes * (originalRes - resistance)), 4 * Time.deltaTime);
            transform.localPosition = new Vector3(marker, transform.localPosition.y, transform.localPosition.z);
        }
        else if(marker == firstPos + (distance / originalRes * (originalRes - resistance)) && !securityBool)
        {
            securityBool = true;
            if(resistance > 0)
            {
                myRend.sprite = arraySprites[resistance - 1];
            }
        }
    }
}
