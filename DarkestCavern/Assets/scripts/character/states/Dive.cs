using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dive : State {

	const float stompThreshold = 20f;

	protected float yMovement;

	public Dive(Character character) : base(character) {

	}
		
	public override void enter () {
		yMovement = -character.diveForce * 1.5f;
	}

	public override State handleInput() {

		float xMovement = Input.GetAxis ("Horizontal");
		gravity ();

		character.move (xMovement * character.airMovementMultiplier, yMovement);
		character.animator.SetFloat ("XMotion", Mathf.Abs(xMovement * character.airMovementMultiplier));
		character.animator.SetFloat ("YMotion", yMovement);

		if (character.transform.position.y <= 0) {

            character.ground();
			if (yMovement < -stompThreshold) {
				return new Stomp (character);
			}
			else {
				return new Idle (character);
			}
		}

		return null;
	}

	protected void gravity() {

		yMovement -= GameManager.instance.gravity * 2 * Time.deltaTime;
	}

	public override void exit () {
		character.animator.SetFloat ("YMotion", 0f);
		return;
	}
}
