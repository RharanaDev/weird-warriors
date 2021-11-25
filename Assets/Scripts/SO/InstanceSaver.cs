using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInstances", menuName = "InstanceSaver")]
public class InstanceSaver : ScriptableObject
{
    public int playerNumber;
    public int playerMode;
    [HideInInspector] public GameObject player1, player2;
    [HideInInspector] public GameObject enemy, playerMe;
    [HideInInspector] public PlayerAttack myAttackScript, enemyAttackScript;
    [HideInInspector] public PlayerGeneralMoveController myMoveScript, enemyMoveScript;
    [HideInInspector] public PlayerDamageController myDamageScript, enemyDamageScript;
    [HideInInspector] public PlayerAnimationController myAnimationScript, enemyAnimationScript;
    [HideInInspector] public PushDetection myDetection, enemyDetection;
    [HideInInspector] public PlayerMana myMana, enemyMana;
    [HideInInspector] public EmpujeScript myPush, enemyPush;
    [HideInInspector] public FightersData myFighter, enemyFighter;
    [HideInInspector] public BoxCollider2D myBoxCo, enemyBoxCo;
    [HideInInspector] public Rigidbody2D myRb, enemyRb;
    [HideInInspector] public Animator myAnim, enemyAnim;
    [HideInInspector] public SortingLayer mySort, enemySort;
    [HideInInspector] public PlayerInputReceiver inputScript;
    // Start is called before the first frame update
    public void Redo()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("PlayerTwo");

        switch (playerNumber)
        {
            case 1:
                inputScript = player1.GetComponent<PlayerInputReceiver>();
                myDamageScript = player1.GetComponentInChildren<PlayerDamageController>();
                enemyDamageScript = player2.GetComponentInChildren<PlayerDamageController>();
                myMoveScript = player1.GetComponent<PlayerGeneralMoveController>();
                enemyMoveScript = player2.GetComponent<PlayerGeneralMoveController>();
                myAttackScript = player1.GetComponent<PlayerAttack>();
                enemyAttackScript = player2.GetComponent<PlayerAttack>();
                myAnimationScript = player1.GetComponentInChildren<PlayerAnimationController>();
                enemyAnimationScript = player2.GetComponentInChildren<PlayerAnimationController>();
                myMana = player1.GetComponent<PlayerMana>();
                enemyMana = player2.GetComponent<PlayerMana>();
                mySort = player1.GetComponentInChildren<SortingLayer>();
                enemySort = player2.GetComponentInChildren<SortingLayer>();
                myBoxCo = player1.GetComponent<BoxCollider2D>();
                enemyBoxCo = player2.GetComponent<BoxCollider2D>();
                myRb = player1.GetComponent<Rigidbody2D>();
                enemyRb = player2.GetComponent<Rigidbody2D>();
                myAnim = player1.GetComponentInChildren<Animator>();
                enemyAnim = player2.GetComponentInChildren<Animator>();
                myPush = player1.GetComponentInChildren<EmpujeScript>();
                enemyPush = player2.GetComponentInChildren<EmpujeScript>();
                myDetection = player1.GetComponentInChildren<PushDetection>();
                enemyDetection = player2.GetComponentInChildren<PushDetection>();
                myFighter = myAttackScript.fighter;
                enemyFighter = enemyAttackScript.fighter;
                enemy = player2;
                playerMe = player1;
                break;
            case 2:
                inputScript = player2.GetComponent<PlayerInputReceiver>();
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
                myPush = player2.GetComponentInChildren<EmpujeScript>();
                enemyPush = player1.GetComponentInChildren<EmpujeScript>();
                myDetection = player2.GetComponentInChildren<PushDetection>();
                enemyDetection = player1.GetComponentInChildren<PushDetection>();
                myFighter = myAttackScript.fighter;
                enemyFighter = enemyAttackScript.fighter;
                enemy = player1;
                playerMe = player2;
                break;
        }
    }
}
