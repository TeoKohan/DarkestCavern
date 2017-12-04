using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class GameManager : MonoBehaviour {

	public static GameManager instance { get; private set;}

    public Zone[] zones; 
	public Wall[] walls;

	public bool paused { get; private set;}

	public InputManager inputManager { get; private set;}
	public UIManager uiManager;
	public SoundManager soundManager { get; private set;}
	public Character currentCharacter { get; private set;}

	private enum State {menu, camp, mine}
	private State state;
	private State previousState;

    void Awake () {
		DontDestroyOnLoad (gameObject);
		instance = this;

		inputManager = new InputManager ();
		soundManager = gameObject.AddComponent (typeof(SoundManager)) as SoundManager;
		uiManager = gameObject.AddComponent (typeof(UIManager)) as UIManager;

		paused = false;
		state = State.mine;
	}
		

	public void blackout() {

		changeLevel (1);
	}

	private void changeLevel(int l) {
		SceneManager.LoadScene (l);
		switch (l) {
		case 0:
			state = State.menu;
			currentCharacter.gameObject.SetActive (false);
			break;
		case 1:
			state = State.camp;
			uiManager.camp ();
			currentCharacter.gameObject.SetActive (false);
			break;
		case 2:
			state = State.mine;
			currentCharacter.gameObject.SetActive (true);
			break;
		}
	}

	void Update() {

		switch (state) {
		case State.mine:
			CharacterInputData inputData = inputManager.characterInput ();
			uiManager.updateUI (currentCharacter.inventory, 0));
			currentCharacter.update (inputData);
			checkPickups ();
			break;

		case State.camp:
			
			break;

		case State.menu:
			break;
		}
	}

	public void changeZone(int zone) {
		Debug.Log ("change");
		StartCoroutine (moveCamera(0.5f, zone));
	}

	IEnumerator moveCamera(float seconds, int zone) {

		Transform mainCamera = Camera.main.transform;

		Vector3 origin = new Vector3 (mainCamera.position.x, 3, -10);
		Vector3 target = new Vector3 (zone * 19, 3, -10);

		float startTime = Time.time;
		while (Time.time - startTime < seconds) {
			float t = (Mathf.Clamp01((Time.time - startTime) / seconds));
			mainCamera.position = Vector3.Slerp (origin, target, t);
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
				Vector3 n = Camera.main.WorldToScreenPoint (N.transform.position);
				n = Camera.main.ScreenToWorldPoint(new Vector3(n.x, 0f, 4.5f));
				Debug.DrawRay (n, Vector3.up, Color.red, 5f);
				if (Mathf.Abs (currentCharacter.transform.position.x - N.transform.position.x) <= currentCharacter.pickaxe.range) {
					return N;
				}
			}
		}

		return null;
	}

	public void setCharacter(Character character) {
		currentCharacter = character;
		character.initialize (new Inventory(new Bag(), new Helmet(), new Pickaxe()), 5f);
    }
}
