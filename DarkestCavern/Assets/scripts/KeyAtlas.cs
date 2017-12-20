using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAtlas {

	public Dictionary<EntityState, List<InputKey>> keys { get; protected set; }

	public KeyAtlas() {

		keys = new Dictionary<EntityState, List<InputKey>>();
	}

	public KeyAtlas(Dictionary<EntityState, List<InputKey>> keys) {

		this.keys = keys;
	}

	public void addKey(EntityState entityState, InputKey key) {
		
		if (keys.ContainsKey(entityState)) {
			keys [entityState].Add (key);
		}
		else {
			keys.Add (entityState, new List<InputKey>());
			addKey (entityState, key);
		}
	}

	public void addKey(EntityState entityState, InputKey[] keys) {

		foreach (InputKey IK in keys) {
			addKey (entityState, IK);
		}
	}

	public List<InputKey> getKeys(EntityState entityState) {

		return keys [entityState];
	}

	public static KeyAtlas getAtlas(string atlas) {

		KeyAtlas keyAtlas = new KeyAtlas ();

		switch (atlas) {

		case "character":
			keyAtlas.addKey (EntityState.action, new InputKey[] { new InputKey (KeyCode.Space, InputKey.Mode.down) });

			keyAtlas.addKey (EntityState.left, new InputKey[] {
				new InputKey (KeyCode.LeftArrow, InputKey.Mode.press),
				new InputKey (KeyCode.A, InputKey.Mode.press)
			});
			keyAtlas.addKey (EntityState.right, new InputKey[] {
				new InputKey (KeyCode.RightArrow, InputKey.Mode.press),
				new InputKey (KeyCode.D, InputKey.Mode.press)
			});
			keyAtlas.addKey (EntityState.jump, new InputKey[] {
				new InputKey (KeyCode.UpArrow, InputKey.Mode.press),
				new InputKey (KeyCode.W, InputKey.Mode.press)
			});
			keyAtlas.addKey (EntityState.crouch, new InputKey[] {
				new InputKey (KeyCode.DownArrow, InputKey.Mode.press),
				new InputKey (KeyCode.S, InputKey.Mode.press)
			});
			return keyAtlas;

		case "minigame":
			keyAtlas.addKey (EntityState.left, new InputKey[] {
				new InputKey (KeyCode.LeftArrow, InputKey.Mode.down),
				new InputKey (KeyCode.A, InputKey.Mode.press)
			});
			keyAtlas.addKey (EntityState.right, new InputKey[] {
				new InputKey (KeyCode.RightArrow, InputKey.Mode.down),
				new InputKey (KeyCode.D, InputKey.Mode.press)
			});
			keyAtlas.addKey (EntityState.right, new InputKey[] {
				new InputKey (KeyCode.UpArrow, InputKey.Mode.down),
				new InputKey (KeyCode.W, InputKey.Mode.press)
			});
			keyAtlas.addKey (EntityState.down, new InputKey[] {
				new InputKey (KeyCode.DownArrow, InputKey.Mode.down),
				new InputKey (KeyCode.S, InputKey.Mode.press)
			});
			return keyAtlas;

		default:
			throw new UnityException ("No key atlas with that name");
		}
	}
}
