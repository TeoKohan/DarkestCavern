using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputManager : InputManager {

	public KeyAtlas keyAtlas { get; set; }

	public KeyInputManager(string atlasName) {
		this.keyAtlas = KeyAtlas.getAtlas (atlasName);
	}

	public KeyInputManager(KeyAtlas keyAtalas) {
		
		this.keyAtlas = keyAtlas;
	}

	public override float getMovement() {
		return Input.GetAxis ("Horizontal");
	}

	public override List<EntityState> getState() {

		List<EntityState> states = new List<EntityState> ();

		foreach (EntityState ES in keyAtlas.keys.Keys) {
			List<InputKey> keylist = keyAtlas.keys [ES];
			foreach (InputKey IK in keylist) {

				switch (IK.mode) {

				case InputKey.Mode.down:
					if (Input.GetKeyDown (IK.key)) {
						states.Add (ES);
						break;
					}
					break;
				case InputKey.Mode.press:
					if (Input.GetKey (IK.key)) {
						states.Add (ES);
						break;
					}
					break;
				case InputKey.Mode.up:
					if (Input.GetKeyUp (IK.key)) {
						states.Add (ES);
						break;
					}
					break;
				case InputKey.Mode.tap:
					break;
				case InputKey.Mode.hold:
					break;
				}
			}
		}

		if (states.Count > 0) {
			return states;
		}

		return null;
	}
}
