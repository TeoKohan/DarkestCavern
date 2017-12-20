using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptKey {

	public enum State {incorrect = -1, neutral = 0, correct = 1 }

	public EntityState key;
	public State state;

	public PromptKey (EntityState key) {

		this.key = key;
		state = State.neutral;
	}

	public PromptKey (EntityState key, State state) {

		this.key = key;
		this.state = state;
	}

	public void correct() {

		state = State.correct;
	}

	public void incorrect() {

		state = State.incorrect;
	}

	public void reset() {

		state = State.neutral;
	}

	public static PromptKey[] getArray(EntityState[] ES) {
		
		PromptKey[] promptKeys = new PromptKey[ES.Length];

		for (int i = 0; i < ES.Length; i++) {
			promptKeys [i] = new PromptKey (ES [i]);
		}

		return promptKeys;
	}
}
