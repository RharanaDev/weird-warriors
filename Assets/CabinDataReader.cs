using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CabinDataReader : MonoBehaviour
{
    public InstanceSaver dataBase;
    public SpritesDatabase sprites;
    public int number;
    private string combo, namely;
    private int mana;
    public string[] comboByParts;
    public Sprite[] actualButtonList;
    public int typeOfGraphic;
    int enumeration;
    public SpriteRenderer[] Buttons; //Use Cheaply
    public TextMeshPro nameText;
    public SpriteRenderer manaNeeded;
    bool yetDone;
    int display;
    public Sprite[] typeOfButons;
    public void OnlyOnce()
    {
        display = dataBase.myFighter.playerNumber == 1 ? GameOptions.display1: GameOptions.display2;
        switch (dataBase.myFighter.ID) //MUST BETTEN LATER
        {
            case 0:
                combo = sprites.ZM_Attacks[number];
                mana = sprites.ZM_Mana[number];
                namely = sprites.ZM_Names[number];
                break;
            case 1:
                combo = sprites.WT_Attacks[number];
                mana = sprites.WT_Mana[number];
                namely = sprites.WT_Names[number];
                break;
            case 2:
                combo = sprites.UH_Attacks[number];
                mana = sprites.UH_Mana[number];
                namely = sprites.UH_Names[number];
                break;
            case 3:
                combo = sprites.GT_Attacks[number];
                mana = sprites.GT_Mana[number];
                namely = sprites.GT_Names[number];
                break;
            case 4:
                combo = sprites.HG_Attacks[number];
                mana = sprites.HG_Mana[number];
                namely = sprites.HG_Names[number];
                break;
            case 5:
                combo = sprites.LH_Attacks[number];
                mana = sprites.LH_Mana[number];
                namely = sprites.LH_Names[number];
                break;
        }
        switch (display)
        {
            case 0:
                typeOfButons = dataBase.playerNumber == 1 ? sprites.graph_keys : sprites.graph_keys2;
                break;
            case 1: //XBOX
                typeOfButons = sprites.graph_xbox;
                break;
            case 2: //PS4
                typeOfButons = sprites.graph_playStation;
                break;
            case 3: //Nintendo
                typeOfButons = sprites.graph_nintendo;
                break;
        }
    }
	private void OnEnable()
	{
        if (!yetDone)
        {
            OnlyOnce();
            yetDone = true;
        }

        actualButtonList = GetButtonSprites();
        Constructor();
        GetSetName();
    }
	public Sprite[] GetButtonSprites()
    {
        enumeration = 0;
        Sprite[] buttonSprites = new Sprite[combo.Length/2];
        comboByParts = Utilities.CutString(combo, 2);
        foreach(string str in comboByParts)
        {
            int number = int.Parse(str);
			if (sprites.reversibleButtons.Contains(number) && dataBase.myFighter.lookDirection == -1)
			{
                number = sprites.reversibleOf[number];
            }
            buttonSprites[enumeration] = typeOfButons[number];
            enumeration++;
        }
        return buttonSprites;
    }
    public void Constructor()
    {
        enumeration = 0;
        foreach (Sprite sprt in actualButtonList)
        {
            Buttons[enumeration].sprite = sprt;
            enumeration++;
        }
    }
    public void GetSetName()
    {
        nameText.text = namely;
        if(mana > 0)
        {
            manaNeeded.sprite = sprites.manaNeeds[mana-1];
        }
    }
}
