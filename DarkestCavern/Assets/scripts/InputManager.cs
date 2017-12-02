using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	private Dictionary<Action, KeyCode> actions;

	public Action action { get; private set;}
	public float movement { get; private set;}

	public void initialize() {
		actions = new Dictionary<Action, KeyCode> ();
		actions.Add (Action.action, KeyCode.Space);
	}

	public void handleInput() {

		handleAction ();
		handleMotion ();
	}

	private Action handleAction() {
		
		KeyCode keycode;
		foreach (Action A in actions.Keys) {
			actions.TryGetValue(A, out keycode);
			if (Input.GetKey (keycode)) {
				return A;
			}
		}

		return Action.idle;
	}

	private float handleMotion() {
		return Input.GetAxis ("Horizontal");
	}

	public Action getAction() {
		return action;
	}

	public float getMovement() {
		return movement;
	}
}
