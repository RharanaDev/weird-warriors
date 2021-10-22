using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PressStartInput : MonoBehaviour
{
	public string activate;
	public string vertical;
	public string[] anybutton;
	public int previousY;
	public int moveValue, posValue = 1;
	public SpriteRenderer[] sprites;
	public Color activeColor, deactiveColor;
	public MarkerScript marker;
	public Preview prev;
	public Animator anim, anim2;
	public int number;
	bool alreadyActive;
	public string[] triggers;
	// Start is called before the first frame update
	bool activatedController;
	bool ableTomove;
	// Update is called once per frame

	void Update()
    {
		if (!alreadyActive)
		{
			CheckNewActive();
		}
		if (ableTomove)
		{
			MoveAxisY();
			SetDisplay();
		}
	}
	void CheckNewActive()
	{
		if (Input.GetButtonDown(activate) && !activatedController)
		{
			activatedController = true;
			AddNewController(true);
		}
		else if (Input.GetButtonDown(activate) && activatedController)
		{
			activatedController = false;
			AddNewController(false);
		}
	}
	void AddNewController(bool active)
	{
		if (active)
		{
			prev.PreviewOutSight();
			marker.enabled = false;
			anim.SetTrigger("Appear");
		}
		else
		{
			anim.SetTrigger("Disappear");
			marker.cursorProgressX = 0;
			prev.ChangeSprite(marker.position - 1);
			marker.CheckIfMorao();
		}
	}
	void MoveAxisY()
	{
		int currentInt;
		currentInt = ((int)Input.GetAxisRaw(vertical));
		moveValue = currentInt == previousY ? 0 : currentInt;
		previousY = currentInt;

		if (moveValue != 0)
		{
			foreach (SpriteRenderer spr in sprites)
			{
				spr.color = deactiveColor;
			}

			posValue += moveValue;
			posValue = posValue > 2 ? 0 : posValue;
			posValue = posValue < 0 ? 2 : posValue;
			sprites[posValue].color = activeColor;
		}
	}
	void SetDisplay()
	{
		foreach(string str in anybutton)
		{
			if (Input.GetButtonDown(str))
			{
				if(number == 1)
				{
					GameOptions.display1 = posValue;
					GameOptions.player1Controller = true;
				}
				else
				{
					GameOptions.display2 = posValue;
					GameOptions.player2Controller = true;
				}
				marker.enabled = true;
				anim2.SetTrigger(triggers[posValue]);
				AllowMovement(0);
				alreadyActive = true;
				AddNewController(false);
			}
		}

	}
	public void AllowMovement(int value)
	{
		ableTomove = value == 1 ? true : false;
	}
	public void isThisDefinitive()
	{
		if (alreadyActive)
		{
			gameObject.SetActive(false);
		}
	}
}
