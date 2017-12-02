using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

	private Dictionary<KeyCode, GameAction> gameActions;
	private Dictionary<KeyCode, CharacterAction> characterActions;
	private Dictionary<KeyCode, PickaxeAction> pickaxeActions;

	public InputManager() {

		gameActions = new Dictionary<KeyCode, GameAction> ();

		gameActions.Add (KeyCode.Escape, GameAction.pause);
		gameActions.Add (KeyCode.Backspace, GameAction.pause);
		gameActions.Add (KeyCode.M, GameAction.mute);

		characterActions = new Dictionary<KeyCode, CharacterAction> ();

		characterActions.Add (KeyCode.Space, CharacterAction.action);
		characterActions.Add (KeyCode.LeftArrow, CharacterAction.move);
		characterActions.Add (KeyCode.RightArrow, CharacterAction.move);
		characterActions.Add (KeyCode.A, CharacterAction.move);
		characterActions.Add (KeyCode.D, CharacterAction.move);

		pickaxeActions = new Dictionary<KeyCode, PickaxeAction> ();

		pickaxeActions.Add (KeyCode.Space, PickaxeAction.spacebar);
		pickaxeActions.Add (KeyCode.LeftArrow, PickaxeAction.left_arrow);
		pickaxeActions.Add (KeyCode.UpArrow, PickaxeAction.up_arrow);
		pickaxeActions.Add (KeyCode.RightArrow, PickaxeAction.right_arrow);
		pickaxeActions.Add (KeyCode.DownArrow, PickaxeAction.down_arrow);
		pickaxeActions.Add (KeyCode.A, PickaxeAction.left_arrow);
		pickaxeActions.Add (KeyCode.W, PickaxeAction.up_arrow);
		pickaxeActions.Add (KeyCode.D, PickaxeAction.right_arrow);
		pickaxeActions.Add (KeyCode.S, PickaxeAction.down_arrow);
	}

	public GameAction gameInput() {

		GameAction action = handleGameAction (gameActions);

		return action;
	}

	public CharacterInputData characterInput() {

		CharacterAction action = handleCharacterAction (characterActions);
		float movement = handleMotion ();

		return new CharacterInputData (action, movement);
	}

	public PickaxeAction pickaxeInput() {

		PickaxeAction action = handlePickaxeAction (pickaxeActions);

		return action;
	}

	private GameAction handleGameAction(Dictionary<KeyCode, GameAction> actions) {
		
		foreach (KeyCode K in actions.Keys) {
			if (Input.GetKey (K)) {
				
				GameAction action;
				actions.TryGetValue(K, out action);
				return action;
			}
		}

		return GameAction.idle;
	}

	private CharacterAction handleCharacterAction(Dictionary<KeyCode, CharacterAction> actions) {

		foreach (KeyCode K in actions.Keys) {
			if (Input.GetKey (K)) {

				CharacterAction action;
				actions.TryGetValue(K, out action);
				return action;
			}
		}

		return CharacterAction.idle;
	}

	private PickaxeAction handlePickaxeAction(Dictionary<KeyCode, PickaxeAction> actions) {

		foreach (KeyCode K in actions.Keys) {
			if (Input.GetKey (K)) {

				PickaxeAction action;
				actions.TryGetValue(K, out action);
				return action;
			}
		}

		return PickaxeAction.idle;
	}

	private float handleMotion() {
		return Input.GetAxis ("Horizontal");
	}
}
