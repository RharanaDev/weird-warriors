using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInstances", menuName = "InstanceSaver")]
public class AInstances : ScriptableObject
{
    public int playerNumber;
    [HideInInspector] public GameObject player1, player2;
    [HideInInspector] public GameObject enemy, playerMe;
    [HideInInspector] public PlayerAttack myAttackScript, enemyAttackScript;
    [HideInInspector] public PlayerGeneralMoveController myMoveScript, enemyMoveScript;
    [HideInInspector] public PlayerDamageController myDamageScript, enemyDamageScript;
    [HideInInspector] public PlayerAnimationController myAnimationScript, enemyAnimationScript;
    [HideInInspector] public PlayerMana myMana, enemyMana;
    [HideInInspector] public FightersData enemyFighter;
    [HideInInspector] public BoxCollider2D myBoxCo, enemyBoxCo;
    [HideInInspector] public Rigidbody2D myRb, enemyRb;
    [HideInInspector] public Animator myAnim, enemyAnim;
    [HideInInspector] public SortingLayer mySort, enemySort;
    // Start is called before the first frame update
    public void Redo()
    {
        player2 = GameObject.FindGameObjectWithTag("PlayerTwo");

        myDamageScript = player2.GetComponentInChildren<PlayerDamageController>();
        enemyDamageScript = player1.GetComponentInChildren<PlayerDamageController>();
        myMoveScript = player2.GetComponent<PlayerGeneralMoveController>();
        enemyMoveScript = player1.GetComponent<PlayerGeneralMoveController>();
        myAttackScript = player2.GetComponent<PlayerAttack>();
        enemyAttackScript = player1.GetComponent<PlayerAttack>();
        myAnimationScript = player2.GetComponentInChildren<PlayerAnimationController>();
        enemyAnimationScript = player1.GetComponentInChildren<PlayerAnimationController>();
        myMana = player2.GetComponent<PlayerMana>();
        enemyMana = player1.GetComponent<PlayerMana>();
        mySort = player2.GetComponentInChildren<SortingLayer>();
        enemySort = player1.GetComponentInChildren<SortingLayer>();
        myBoxCo = player2.GetComponent<BoxCollider2D>();
        enemyBoxCo = player1.GetComponent<BoxCollider2D>();
        myRb = player2.GetComponent<Rigidbody2D>();
        enemyRb = player1.GetComponent<Rigidbody2D>();
        myAnim = player2.GetComponentInChildren<Animator>();
        enemyAnim = player1.GetComponentInChildren<Animator>();
        enemyFighter = enemyAttackScript.fighter;
        enemy = player1;
        playerMe = player2;

    }
}
