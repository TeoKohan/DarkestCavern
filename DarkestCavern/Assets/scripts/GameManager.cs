using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class GameManager : MonoBehaviour {

	public struct MiningSession{
		public float duration;
		public float endTime;
		public float lightLevel;

		public MiningSession (float duration) {
			this.duration = duration;
			endTime = Time.time + duration;
			lightLevel = 1f;
		}
	}

	public GameObject campMesh;
	public GameObject mine;
	public GameObject shadowPlane;

	public static GameManager instance { get; private set;}

	public InputManager inputManager { get; private set;}
	public MinigameManager minigameManager { get; private set;}
	public UIManager uiManager;
	public SoundManager soundManager;

	public Character character { get; private set;}
	private Inventory wardrobe;

	public bool paused { get; private set;}
	public MiningSession miningSession { get; private set;}

	public float lightLevel {
		get {
			float startTime = miningSession.endTime - miningSession.duration;
			float currentTime = Time.time - startTime;
			float percentage = 1 - (currentTime / miningSession.duration);
			if (percentage <= 0f) {
				changeState(State.camp);
			}
			return percentage;
		} 
		private set {
			;}
	}

	public enum State {menu, camp, mine, pause, minigame}
	private State state;
	private State previousState;

    void Awake () {
		instance = this;

		inputManager = new InputManager ();
		minigameManager = new MinigameManager ();

		uiManager.initialize ();
		soundManager.initialize ();

		paused = false;
		wardrobe = new Inventory(new Bag(), new Helmet(), new Pickaxe());

		getCharacter ();
		character.initialize (wardrobe);

		state = State.menu;
		changeState (state);
	}

	private void loadScene(State state) {
		switch (state) {
		case State.menu:
			Camera.main.transform.position = new Vector3 (0f, 0f, 0f);
			character.gameObject.SetActive (false);
			showMenu ();
			hideCamp ();
			hideMine ();
			break;

		case State.camp:
			Camera.main.transform.position = new Vector3 (-5f, 10f, -21f);
			character.gameObject.SetActive (false);
			hideMenu ();
			showCamp ();
			hideMine ();
			break;

		case State.mine:
			Camera.main.transform.position = new Vector3 (0f, 3f, -10f);
			character.transform.position = new Vector3 (0f, 1.25f, -2f);
			character.gameObject.SetActive (true);
			hideMenu ();
			hideCamp ();
			showMine ();
			Node.refresh ();
			break;

		default:
			character.gameObject.SetActive (false);
			showMenu ();
			hideCamp ();
			hideMine ();
			break;
		}
	}

	private void hideAll() {
		hideMenu ();
		hideCamp ();
		hideMine ();
	}

	private void showMenu() {

	}


	private void hideMenu() {
		
	}

	private void hideCamp () {
		campMesh.SetActive (false);
	}

	private void showCamp () {
		campMesh.SetActive (true);
	}

	private void hideMine () {
		shadowPlane.SetActive (false);
		mine.SetActive (false);
	}

	private void showMine () {
		shadowPlane.SetActive (true);
		mine.SetActive (true);
	}

	public bool startMinigame(Pickaxe pickaxe, Node node) {
		if (state == State.mine) {
			minigameManager.startMinigame (pickaxe, node);
			changeState (State.minigame);
			return true;
		}
		return false;
	}

	public void endMinigame() {
		if (state == State.minigame) {
			changeState (State.mine);
			uiManager.hidePrompts ();
			character.finishMining ();
		}
	}

	public void changeState(State state) {
		previousState = this.state;
		switch (previousState) {

		case State.menu:
			switch (state) {
			case State.menu:
				uiManager.setMode (State.menu);
				loadScene (State.menu);
				break;
			case State.camp:
				if (character != null) {
					wardrobe = character.inventory;
				}
				uiManager.setMode (State.camp);
				loadScene (State.camp);
				break;
			case State.mine:
				getCharacter ();
				dress (wardrobe);
				uiManager.setMode (State.mine);
				miningSession = new MiningSession (character.inventory.helmet.duration);
				break;
			}
			break;

		case State.camp:
			switch (state) {
			case State.mine:
				loadScene (State.mine);
				getCharacter ();
				dress (wardrobe);
				miningSession = new MiningSession (character.inventory.helmet.duration);
				uiManager.setMode (State.mine);
				break;
			}
			break;

		case State.mine:
			switch (state) {
			case State.camp:
				loadScene (State.camp);
				wardrobe = character.inventory;
				uiManager.setMode (State.camp);

				break;
			case State.minigame:
				break;
			}
			break;

		case State.minigame:
			switch (state) {
			case State.camp:
				loadScene (State.camp);
				wardrobe = character.inventory;
				uiManager.setMode (State.camp);
				break;
			case State.mine:
				break;
			}
			break;

		}
		this.state = state;
	}

	void Update() {

		switch (state) {
		case State.menu:
			break;
		
		case State.camp:
			break;

		case State.mine:
			CharacterInputData inputData = inputManager.characterInput ();

			character.update (inputData);
			uiManager.updateUI (character.inventory, state);

			break;
		case State.minigame:
			
			character.update ();
			uiManager.updateUI (character.inventory, state);
			minigameManager.handleInput (inputManager.pickaxeInput ());

			break;
		case State.pause:
			break;
		default:
			break;
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

	public Character getCharacter() {
		if (character != null) {
			return character;
		} 
		else {
			character = GameObject.Find ("character").GetComponent<Character>() as Character;
			return character;
		}
	}

	private void dress (Inventory clothes) {
		character.initialize (clothes);
	}

	public void changeZone(int zone) {
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
}
