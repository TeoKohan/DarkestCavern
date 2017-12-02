using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance { get; private set;}
	public bool paused { get; private set;}
	public InputManager inputManager { get; private set;}
	public UIManager uiManager { get; private set;}
	public SoundManager soundManager { get; private set;}
	public Character currentCharacter { get; private set;}

	void Awake () {
		instance = this;
		paused = false;
		inputManager = new InputManager ();
		uiManager = gameObject.AddComponent (typeof(UIManager)) as UIManager;
		soundManager = gameObject.AddComponent (typeof(SoundManager)) as SoundManager;
	}

	void Update() {
		
		CharacterInputData inputData = inputManager.characterInput ();
		uiManager.updateUI (new UIData());
		currentCharacter.update (inputData);
	}

	private void handleInput(GameAction action) {
		switch (action) {
		case GameAction.pause:
			pauseUnpause ();
			break;
		case GameAction.mute:
			soundManager.muteUnmute ();
			break;
		}
	}

	private void pauseUnpause() {
		//PAUSE UNPAUSE CODE
	}

	public void setCharacter(Character character) {
		currentCharacter = character;
		character.initialize (new Pickaxe(), new Lamp(), new Bag(), 5f);
	}
}
