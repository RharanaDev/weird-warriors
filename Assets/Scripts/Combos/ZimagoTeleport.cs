using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZimagoTeleport : MonoBehaviour
{
    public Animator anim;
    public InstanceSaver database;
    public Transform tf;
    public FightersData fighter;

    void Teleport()
    {
        if (database.enemyFighter.isInEnd || database.enemy.transform.position.x >= 18 && database.enemyFighter.lookDirection == -1 || database.enemy.transform.position.x <= -18 && database.enemyFighter.lookDirection == 1)
        {
            tf.position = new Vector3(database.enemy.transform.position.x + (4 * -fighter.lookDirection), database.playerMe.transform.position.y, database.playerMe.transform.position.z);
        }
        else if (!database.enemyFighter.isInEnd)
        {
            tf.position = new Vector3(database.enemy.transform.position.x + (4 * fighter.lookDirection), database.playerMe.transform.position.y, database.playerMe.transform.position.z);
        }
    }
}
