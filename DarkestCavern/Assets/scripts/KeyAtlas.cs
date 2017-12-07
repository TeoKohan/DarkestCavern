using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action {left, up, right, down, mine};

public static class KeyAtlas {

	public static Dictionary<Action, List<KeyCode>> keys { get; protected set; }

	public static void setKey(Action action, KeyCode keycode) {
		if (keys) {
			if (keys [Action]) {
				keys [Action].Add (keycode);
			}
			else {
				keys.Add (action, new List<KeyCode> (keycode));
			}
		}

		else {
			keys = new Dictionary<Action, List<KeyCode>> ();
			setKey (action, keycode);
		}
	}
}
