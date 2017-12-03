using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public Camera mainCamera;
    public Zone[] zones; 
	public Wall[] walls;
	public List<OrePickup> ores; 
	public static GameManager instance { get; private set;}
	public bool paused { get; private set;}
	public InputManager inputManager { get; private set;}
	public UIManager uiManager;
	public SoundManager soundManager { get; private set;}
	public Character currentCharacter { get; private set;}

    void Awake () {
		
		instance = this;
		paused = false;
		inputManager = new InputManager ();
		soundManager = gameObject.AddComponent (typeof(SoundManager)) as SoundManager;

		foreach (Zone Z in zones) {
			Z.activateNodes ();
		}
		ores = new List<OrePickup> ();
	}
		

	public void blackout() {
	}

	void Update() {
		
		CharacterInputData inputData = inputManager.characterInput ();
		uiManager.updateUI (new UIData(currentCharacter.bag.ores, 0));
		currentCharacter.update (inputData);
		checkPickups ();
	}

	public void changeZone(int zone) {
		Debug.Log ("change");
		StartCoroutine (moveCamera(0.5f, zone));
	}

	IEnumerator moveCamera(float seconds, int zone) {

		Vector3 origin = new Vector3 (mainCamera.transform.position.x, 3, -10);
		Vector3 target = new Vector3 (zone * 19, 3, -10);

		float startTime = Time.time;
		while (Time.time - startTime < seconds) {
			float t = (Mathf.Clamp01((Time.time - startTime) / seconds));
			mainCamera.transform.position = Vector3.Slerp (origin, target, t);
			yield return null;
		}
		mainCamera.transform.position = target;
	}

	private void checkPickups() {

		OrePickup temp = null;

		foreach (OrePickup OP in ores) {
			if (Mathf.Abs(currentCharacter.transform.position.x - OP.transform.position.x) < 1f) {
				temp = OP;
				break;
			}
		}

		if (temp != null) {
			temp.pickup ();
		}
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

	public float checkCollision(float pos) {
		if (pos <= -7f) {
			return -7f;
		}
		foreach (Wall W in walls) {
			if (pos > W.transform.position.x - W.radius && W.active) {
				return W.transform.position.x - W.radius;
			}
		}

		return pos;
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
