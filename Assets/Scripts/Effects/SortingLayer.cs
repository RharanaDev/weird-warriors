using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    public int layer;
    public SpriteRenderer render;
    // Start is called before the first frame update

    // Update is called once per frame
    public void SortInLayer()
    {
        switch (layer)
        {
            case 0:
                render.sortingOrder = 2;
                break;
            case 1:
                render.sortingOrder = 4;
                break;

            case 2:
                render.sortingOrder = 3;
                break;

            case 3:
                render.sortingOrder = 1;
                break;
        }
    }
}