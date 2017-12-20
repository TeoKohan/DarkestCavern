using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State {

	const float deadZone = 0.05f;

	public Idle(Character character) : base(character) {

		character.animator.SetFloat ("XMotion", 0f);
		character.animator.SetFloat ("YMotion", 0f);
	}

	public override State handleInput() {

		List<EntityState> states = character.inputManager.getState ();

		if (states == null) {
			return null;
		}
		if (states.Contains(EntityState.action)) {
			return new Mine(character);
		}
		if (states.Contains(EntityState.jump)) {
			return new Jump(character);
		}
		if (states.Contains(EntityState.left) || states.Contains(EntityState.right)) {
			return new Walk(character);
		}
		if (states.Contains(EntityState.crouch)) {
			return new Crouch(character);
		}

		return null;
	}
}
