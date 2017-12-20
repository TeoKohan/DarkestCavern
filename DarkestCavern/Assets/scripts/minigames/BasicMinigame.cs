using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMinigame : Minigame {

	const int defaultKeyAmount = 5;
	const float defaultDuration = 3f;

	protected KeyInputManager inputManager;

	protected EntityState[] availableKeys;

	public PromptKey[] keyList { get; private set; }

	protected float startTime;
	protected float duration;
	protected int maxErrors;

	public int errors { get; private set; }
	public int currentIndex { get; private set; }

	protected PromptKey currentKey {
		
		get {

            if (currentIndex < keyList.Length) {
                return keyList[currentIndex];
            }

            return null;
		}

		set {
		}
	}

	public BasicMinigame () {
		
		availableKeys = new EntityState[] { EntityState.left, EntityState.up, EntityState.right, EntityState.down};

		keyList = new PromptKey[defaultKeyAmount];
		initialize ();
	}

	public BasicMinigame (List<EntityState> keys) {

		availableKeys = keys.ToArray();

		keyList = new PromptKey[defaultKeyAmount];
		initialize ();
	}

	protected void fillListRandom() {
		
		for (int i = 0; i < keyList.Length; i++) {
			keyList[i] = new PromptKey(availableKeys[Random.Range(0, availableKeys.Length)]);
		}
		return;
	}

	private void initialize() {

		inputManager = new KeyInputManager ("minigame");
		maxErrors = keyList.Length / 5;
		duration = defaultDuration * keyList.Length;
		start ();
	}

	public override void start() {
		
		fillListRandom ();
		startTime = Time.time;
		errors = 0;
		currentIndex = 0;

        Debug.Log(currentKey.key.ToString());
    }

	public override bool update() {
		
		List<EntityState> states = inputManager.getState ();

		if (states != null) {

			if (states.Contains (currentKey.key)) {
				currentKey.state = PromptKey.State.correct;
			} 
			else {
				errors++;
				currentKey.state = PromptKey.State.incorrect;
			}

			nextKey ();
		}

		return finish ();
	}

	protected void nextKey() {

		currentIndex++;
        if (currentKey != null) {
            GameManager.instance.uiManager.showPrompt (currentKey);
            Debug.Log(currentKey.key.ToString());
        }
	}

	protected override bool finish() {
		
		if (errors > maxErrors || duration <= Time.time - startTime || currentIndex >= keyList.Length) {
			return true;
		}

		return false;
	}

    public override bool win () {

        if (errors > maxErrors || duration <= Time.time - startTime) {
            Debug.Log("lose");
            return false;
        }

        else if (currentIndex >= keyList.Length) {
            Debug.Log("win");
            return true;
        }

        return false;
    }
}
