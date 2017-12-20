using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : State {

	protected float yMovement;

	public Fall(Character character) : base(character) {

	}
		
	public override void enter () {
		yMovement = 0f;
	}

	public override State handleInput() {

		List<EntityState> states = character.inputManager.getState ();

		float xMovement = Input.GetAxis ("Horizontal");
		
		character.move (xMovement * character.airMovementMultiplier, yMovement);
		character.animator.SetFloat ("XMotion", Mathf.Abs(xMovement * character.airMovementMultiplier));
		character.animator.SetFloat ("YMotion", yMovement);

		if (character.transform.position.y <= 0) {
            character.ground();
            return new Idle (character);
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

	public override State update() {
		gravity();
		return handleInput();
	}

	public override void exit () {
		character.animator.SetFloat ("YMotion", 0f);
		return;
	}
}
