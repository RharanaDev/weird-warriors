using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointScript : MonoBehaviour
{
	public int puntuation;
	public TextMeshPro boxText, shadeText;
	public bool booly = false;
	public void ApplyPuntuation()
	{
		boxText.text = puntuation.ToString("00000");
		shadeText.text = puntuation.ToString("00000");
	}
}
