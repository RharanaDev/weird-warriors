using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinetics : MonoBehaviour
{
    public Animator anim;
	public Sprite[] horizontal, vertical, diagonal;
	public SpriteRenderer rend1, rend1b, rend2, rend3;
    public void ScreenPlay(int n, int Pnumb, int color)
	{
		switch (n)
		{
			case 0:
				anim.Play("NewCinetics");
				rend1.sprite = horizontal[color];
				rend1b.sprite = horizontal[color];
				break;

			case 1:
				anim.Play("HorizontalCinetic");
				rend2.sprite = vertical[color];
				break;

			case 2:
				rend3.sprite = diagonal[color];
				switch (Pnumb)
				{
					case 1:
						anim.Play("DiagCinetics");
						break;
					case 2:
						anim.Play("DiagCinetics2");
						break;
				}
				break;
		}
	}
}
