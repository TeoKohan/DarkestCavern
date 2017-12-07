using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyAtlas {

	public static Dictionary<CharacterState, List<KeyCode>> keys { get; protected set; }

	public static void setKey(CharacterState state, KeyCode keycode) {
		if (keys) {
			if (keys [CharacterState]) {
				keys [CharacterState].Add (keycode);
			}
			else {
				keys.Add (state, new List<KeyCode> (keycode));
			}
		}

		else {
			keys = new Dictionary<CharacterState, List<KeyCode>> ();
			setKey (state, keycode);
		}
	}
}
