using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseMenu : MonoBehaviour
{

    public Sprite[] images, greyImages;
    public Sprite[] greyIcons;
    public string[] names;
    public SpriteRenderer[] rosterIcons;
    SpriteRenderer currentRenderer;
    public List<int> UnlockedChars;
    public AudioClip[] clip;

    public void Confirm(int position)
    {
        currentRenderer = rosterIcons[position];
        currentRenderer.sprite = greyIcons[position];
    }
}