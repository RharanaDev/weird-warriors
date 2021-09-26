using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIConfig : MonoBehaviour
{
    public Sprite[] charImg;
    public string[] charNames;
    public Vector3[] customPos;
    public MatchData match1, match2;
    public GameObject firstImg, secondImg, firstName, secondName, firstNameshade, secondNameshade;
    // Start is called before the first frame update
    void Start()
    {
        firstImg.GetComponent<SpriteRenderer>().sprite = charImg[match1.playerCharacter -1];
        secondImg.GetComponent<SpriteRenderer>().sprite = charImg[match2.playerCharacter -1];
        firstName.GetComponent<TextMeshPro>().text = charNames[match1.playerCharacter -1];
        firstNameshade.GetComponent<TextMeshPro>().text = charNames[match1.playerCharacter -1];
        secondName.GetComponent<TextMeshPro>().text = charNames[match2.playerCharacter -1];
        secondNameshade.GetComponent<TextMeshPro>().text = charNames[match2.playerCharacter -1];
    }
}
