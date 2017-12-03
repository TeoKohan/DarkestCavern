using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Zone[] zones; 
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

		foreach (Zone Z in zones) {
			Z.activateNodes ();
		}
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

	public Node getNode(int zone) {
		
		foreach (Node N in zones[zone].nodes) {
			if (N.active) {
				if (Mathf.Abs (currentCharacter.transform.position.x - N.transform.position.x) <= currentCharacter.pickaxe.range) {
					return N;
				}
			}
		}

		return null;
	}

	public void setCharacter(Character character) {
		currentCharacter = character;
		character.initialize (new Pickaxe(), new Lamp(), new Bag(), 5f);
    }
}
