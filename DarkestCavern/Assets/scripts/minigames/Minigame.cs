using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame {

	public enum Response {wait, correct, incorrect}

	public List<PickaxeAction> availableKeys { get; private set; }
	public PickaxeAction[] keyList { get; private set; }
	public Response[] keystates { get; private set; }

	public float duration { get; private set; }
	public int errors { get; private set; }

	public int currentKey { get; private set; }

	public Minigame () {
		
		availableKeys = new List<PickaxeAction> ();
		availableKeys.Add (PickaxeAction.left_arrow);
		availableKeys.Add (PickaxeAction.up_arrow);
		availableKeys.Add (PickaxeAction.right_arrow);
		availableKeys.Add (PickaxeAction.down_arrow);
		availableKeys.Add (PickaxeAction.spacebar);

		refreshKeys (3);
	}

	public void start(int keys, float duration, int errors) {
		this.duration = duration;
		this.errors = errors;

		refreshKeys (keys);
		currentKey = 0;
	}

	public Response handleInput(PickaxeAction action) {
		if (action != PickaxeAction.idle) {
			if (action == keyList [currentKey]) {
				keystates [currentKey] = Response.correct;
				currentKey++;
				return Response.correct;
			}
			else {
				keystates [currentKey] = Response.incorrect;
				currentKey++;
				errors--;
				return Response.incorrect;
			}
		} 
		else {
			return Response.wait;
		}
	}

	public void refreshKeys(int keys) {

		keyList = new PickaxeAction[keys];
		keystates = new Response[keys];
		for (int i = 0; i < keys; i++) {
			keyList [i] = availableKeys[Random.Range (0, availableKeys.Count)];
			keystates [i] = Response.wait;
		}
	}
}
