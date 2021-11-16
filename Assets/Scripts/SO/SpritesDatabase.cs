using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpritesDatabase", menuName = "SpritesDataBase")]
public class SpritesDatabase : ScriptableObject
{
	public Sprite[] graph_keys, graph_keys2, graph_playStation, graph_xbox, graph_nintendo;
	public string[] ZM_Attacks, UH_Attacks, WT_Attacks, GT_Attacks, LH_Attacks, HG_Attacks;
	public string[] ZM_Names, UH_Names, WT_Names, GT_Names, LH_Names, HG_Names;
	public int[] ZM_Mana, UH_Mana, WT_Mana, GT_Mana, LH_Mana, HG_Mana;
	public Sprite[] manaNeeds;
	public List<int> reversibleButtons;
	public int[] reversibleOf;
}
