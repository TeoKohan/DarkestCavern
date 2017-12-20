using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : State {

	protected float yMovement;

	public SuperJump(Character character) : base(character) {

	}

	public override void enter () {
		yMovement = character.jumpForce * 2;
	}

	public override State handleInput() {

		float xMovement = Input.GetAxis ("Horizontal");
		gravity ();

		character.move (xMovement * character.airMovementMultiplier, yMovement);
		character.animator.SetFloat ("XMotion", Mathf.Abs(xMovement * character.airMovementMultiplier));
		character.animator.SetFloat ("YMotion", yMovement);

		if (yMovement <= 0) {
			return new Fall(character);
		}

		return null;
	}


	protected void gravity() {

		yMovement -= GameManager.instance.gravity * Time.deltaTime;
	}

	public override void exit () {
		return;
	}
}
