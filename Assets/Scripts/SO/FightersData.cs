using UnityEngine;

[CreateAssetMenu(fileName = "New Fighter", menuName = "Fighter")]
public class FightersData : ScriptableObject
{
	#region CharacterData
	//Information
	public string alias;
	public int ID;
	//StringReferences
	public string attackPart, slider, manaSlider, blockBar, blockBarSkin, comboPart;
	//Skills
	public float speed, direction, jumpForce, pushModule;
	public int currentLife, life, damage, playerNumber, lookDirection, resistance;
	public int[] effect = new int[5];
	public int originalRes;
	//Dimensions
	public Vector2 crouchHitBoxSize, crouchHitBoxOffSet;
	public Vector3 backDustOffset, frontDustOffset;
	public string possibleCombo1, possibleCombo2, possibleCombo3;
	#endregion

	#region States
	//Player Situation
	public bool isGrounded, isBlock, isCrouch, isAlive, isWalking, isCombo, isInvincible, isPhysical, isStunned, isForwardJumping, isInEnd, isAirAttacking, isInverted, isDoingCombo2;
	//Player Posibilities
	public bool canRotate, canAirHit, canAttack, canOverJump, canBeRepeled, canWaitForCombo, canWaitForDodgeCombo, canRebound, canMove, canAIttack, beenUnblocked, customRotate, isBig, onActiveFrame;
	#endregion\

	public void Redo()
	{
		canRebound = false;
		canWaitForCombo = false;
		//Booleans
		onActiveFrame = false;	
		customRotate = false;
		direction = 0;
		beenUnblocked = false;
		isAirAttacking = false;
		isDoingCombo2 = false;
		isPhysical = true;
		isInverted = false;
		canBeRepeled = true;
		isInEnd = false;
		isInvincible = false;
		isBlock = false;
		isCrouch = false;
		isGrounded = true;
		isWalking = false;
		isAlive = true;
		isCombo = false;
		canOverJump = false;
		canMove = true;
		isStunned = false;
		canAIttack = true;
		resistance = originalRes;
		currentLife = life;
		canRotate = true;
	}
}
