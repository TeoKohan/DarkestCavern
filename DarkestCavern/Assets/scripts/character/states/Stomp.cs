using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : State {

	const float deadZone = 0.05f;
	const float stompDuration = 0.5f;

	protected float enterTime;

	public Stomp(Character character) : base(character) {
		
	}

	public override void enter () {
		enterTime = Time.time;
		character.animator.SetBool ("Stomping", true);
		return;
	}

	public override State handleInput() {
		
		if (Time.time - enterTime > stompDuration) {
			return new Idle (character);
		}

		return null;
	}

	public override void exit () {
		character.animator.SetBool ("Stomping", false);
		return;
	}
}
