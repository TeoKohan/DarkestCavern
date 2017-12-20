using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {

	protected Character character;

	public State() {
	}

	public State(Character character) {
		this.character = character;
	}

	public virtual void enter () {
		return;
	}

	public virtual State handleInput () {
		return null;
	}

	public virtual State update () {
		return handleInput();
	}

	public virtual void exit () {
		return;
	}
}
