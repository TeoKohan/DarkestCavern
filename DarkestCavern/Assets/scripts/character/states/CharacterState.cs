using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState {

	protected CharacterState() {
	}

	public abstract void handleInput(Character character, CharacterInputData input) {
	}

	public abstract void update(Character character) {
	}
}
