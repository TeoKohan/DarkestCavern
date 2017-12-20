using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State {

	public Walk(Character character) : base(character) {

	}
		
	public override State handleInput() {

		float xMovement = Input.GetAxis ("Horizontal");

		character.move (xMovement, 0f);
		character.animator.SetFloat ("XMotion", Mathf.Abs(xMovement));

		List<EntityState> states = character.inputManager.getState ();

		if (states == null) {
			return new Idle(character);
		}

		if (states.Contains(EntityState.action)) {
			return new Mine(character);
		}

		if (states.Contains(EntityState.jump)) {
			return new Jump(character);
		}

		if (states.Contains(EntityState.crouch)) {
			return new Crouch(character);
		}

		if (states.Contains(EntityState.left) || states.Contains(EntityState.right)) {
			return null;
		}

		return new Idle(character);
	}

	public override void exit () {
		character.animator.SetFloat ("XMotion", 0f);
		return;
	}
}
