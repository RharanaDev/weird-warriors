using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Preview : MonoBehaviour
{
    bool isOut;
    public SpriteRenderer rend;
    public Animator anim;
    public TextMeshPro texto, shadowText;
    bool randomActive;
    public DatabaseMenu data;
    public MarkerScript marker, enemyMarker;
    public int finalCharID;
    public AudioSource source;
    public MatchData matchy;

    public void PreviewOutSight()
	{
        anim.SetTrigger("Outer");
    }
    public void IsOut(int outie)
	{
        isOut = outie == 1 ? true : false;
    }
    public void ChangeSprite(int sprite)
    {
        anim.SetTrigger("Enter");

        if (sprite == 15)
        {
            StartCoroutine("RandomPos");
            texto.text = data.names[sprite];
            shadowText.text = data.names[sprite];
        }
        else
        {
            if (randomActive)
            {
                StopCoroutine("RandomPos");
                randomActive = false;
            }
            rend.sprite = data.images[sprite];
            texto.text = data.names[sprite];
            shadowText.text = data.names[sprite];
        }
    }
    public IEnumerator RandomPos()
    {
        randomActive = true;
        int n;
        n = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (data.UnlockedChars.Contains(14))
            {
                if (n >= 14)
                {
                    n = 0;
                }
            }
            else
            {
                if (n >= 13)
                {
                    n = 0;
                }
            }

            while (!data.UnlockedChars.Contains(n))
            {
                n++;
            }
            rend.sprite = data.images[n];
            n++;
            finalCharID = n;
        }
    }
    public void ChangeState()
    {
        if (marker.position == 16)
        {
            StopCoroutine("RandomPos");
            data.UnlockedChars.Remove(finalCharID - 1);
            marker.isActivated = false;
            marker.rend.enabled = false;
            data.Confirm(finalCharID - 1);
            MarkerScript.takenPositions.Add(finalCharID);
            rend.sprite = data.greyImages[finalCharID - 1];
            if (finalCharID != 14)
            {
                texto.text = data.names[finalCharID - 1];
                shadowText.text = data.names[finalCharID - 1];
                source.clip = data.clip[finalCharID - 1];
                source.Play();
            }
            else
            {
                texto.text = "BULLSHIT";
                shadowText.text = "BULLSHIT";
                source.clip = data.clip[finalCharID - 1];
                source.Play();
            }

            if (finalCharID == enemyMarker.position)
            {
                enemyMarker.position++;
                enemyMarker.transform.position = enemyMarker.transforms[enemyMarker.position - 1].position;
                enemyMarker.cursorProgressX = 0;
                enemyMarker.prev.ChangeSprite(enemyMarker.position - 1);
            }
            matchy.playerCharacter = finalCharID;
        }
        else
        {
            rend.sprite = data.greyImages[marker.position - 1];
            marker.isActivated = false;
            marker.rend.enabled = false;
            data.Confirm(marker.position - 1);
            MarkerScript.takenPositions.Add(marker.position);
            source.clip = data.clip[marker.position - 1];
            source.Play();
            matchy.playerCharacter = marker.position;
            if (enemyMarker.position == marker.position)
            {
                enemyMarker.position++;
                enemyMarker.transform.position = enemyMarker.transforms[enemyMarker.position - 1].position;
                enemyMarker.cursorProgressX = 0;
                enemyMarker.prev.ChangeSprite(enemyMarker.position - 1);
                enemyMarker.CheckIfMorao();
            }
        }
    }
}
