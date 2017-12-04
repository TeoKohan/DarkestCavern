using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class GameManager : MonoBehaviour {

	public static GameManager instance { get; private set;}

	public InputManager inputManager { get; private set;}
	public MinigameManager minigameManager { get; private set;}
	public UIManager uiManager;
	public SoundManager soundManager { get; private set;}

	public Character currentCharacter { get; private set;}

	public bool paused { get; private set;}
	public float lightLevel { get; private set;}

	private enum State {menu, camp, mine, pause, minigame}
	private State state;
	private State previousState;

    void Awake () {
		instance = this;
		DontDestroyOnLoad (gameObject);

		inputManager = new InputManager ();
		minigameManager = new MinigameManager ();

		paused = false;

		//DEBUG
		state = State.mine;
	}

	private void loadLevel(int l) {
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

			updateLightLevel ();
			uiManager.updateUI (currentCharacter.inventory);
			currentCharacter.update (inputData);
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

	public void updateLightLevel() {
		lightLevel = 1f;
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
		character.initialize (new Inventory(new Bag(), new Helmet(), new Pickaxe()));
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
