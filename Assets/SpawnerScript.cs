using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] prefabs;
    public MatchData match;
    public static bool bothSpawned;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(prefabs[match.playerCharacter -1], transform.position, transform.rotation);
    }

    // Update is called once per frame
}
