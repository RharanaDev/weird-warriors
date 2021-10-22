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
    public Color errorColor, ogcolor;
    public void Confirm(int position)
    {
        currentRenderer = rosterIcons[position];
        currentRenderer.sprite = greyIcons[position];
    }
    public IEnumerator Fail(int position)
    {
        currentRenderer = rosterIcons[position];
        for (int i = 0; i < 3; i++)
        {
            currentRenderer.color = errorColor;
            yield return new WaitForSecondsRealtime(0.04f);
            currentRenderer.color = ogcolor;
        }
    }
}