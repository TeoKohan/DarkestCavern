using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : State {

	protected float yMovement;

	public Jump(Character character) : base(character) {

	}

	public override void enter () {
		yMovement = character.jumpForce;
	}

	public override State handleInput() {

		List<EntityState> states = character.inputManager.getState ();

		float xMovement = Input.GetAxis ("Horizontal");
		gravity ();

		character.move (xMovement * character.airMovementMultiplier, yMovement);
		character.animator.SetFloat ("XMotion", Mathf.Abs(xMovement * character.airMovementMultiplier));
		character.animator.SetFloat ("YMotion", yMovement);

		if (yMovement <= 0) {
			return new Fall(character);
		}

		if (states == null) {
			return null;
		}

		if (states.Contains(EntityState.crouch)) {
			return new Dive(character);
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
