using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKey {

	public enum Mode { press, down, up, hold, tap }

	public KeyCode key;
	public Mode mode;

	public InputKey (KeyCode key, Mode mode) {

		this.key = key;
		this.mode = mode;
	}

	public InputKey (KeyCode key) {

		this.key = key;
		this.mode = Mode.down;
	}
}
