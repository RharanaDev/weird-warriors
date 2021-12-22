using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SelectorScreencharge : MonoBehaviour
{
    public GameObject filter;
    public int number;
    public SpriteRenderer rend;
    public SpritesDatabase data;
    public MatchData dataMatch;
    public TextMeshPro names, shade;
    public string[] fighterNames;
    public Animator vs;
    // Start is called before the first frame update
    void Start()
    {
        fighterNames = new string[] { "", "", "", "", "JEAN PIERRE VISAGE", "ASTRAL ZHIMAGO", "ASTRAL LEO", "", "HEMOGOBLIN", "LEO AND HEX", "ZHIMAGO", "DR UTAH", "GEN TAI", "WAKAN TANKA", "PROF. PRIPYAT" };
        rend.sprite = data.fighterPreview[dataMatch.playerCharacter -1];
        names.text = fighterNames[dataMatch.playerCharacter -1];
        shade.text = fighterNames[dataMatch.playerCharacter -1];
    }
    public IEnumerator AppearVs()
	{
        yield return new WaitForSecondsRealtime(0.5f);
        vs.SetTrigger("VS");
    }
}
