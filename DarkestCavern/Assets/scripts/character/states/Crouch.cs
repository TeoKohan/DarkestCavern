using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : State {

	const float deadZone = 0.05f;
	const float chargeTime = 2f;

	protected float enterTime;

	public Crouch(Character character) : base(character) {
		
	}

	public override void enter () {
		enterTime = Time.time;
		character.animator.SetBool ("Crouching", true);
		return;
	}

	public override State handleInput() {

		List<EntityState> states = character.inputManager.getState ();

		float xMovement = Input.GetAxis ("Horizontal");

		if (Time.time - enterTime < chargeTime / 2f) {
			
			if (Input.GetAxisRaw ("Horizontal") != 0) {
				character.move (xMovement * character.crouchMovementMultiplier, 0);
				enterTime = Time.time;
			}
		}



		if (states == null) {
			if (Time.time - enterTime > chargeTime) {
				return new SuperJump (character);
			}

			return new Idle(character);
		}

		if (states.Contains(EntityState.crouch)) {
			return null;
		}

		return new Idle(character);
	}

	public override void exit () {
		character.animator.SetBool ("Crouching", false);
		return;
	}
}
