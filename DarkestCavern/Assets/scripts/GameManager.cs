using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class GameManager : MonoBehaviour {

	public static GameManager instance { get; private set;}

	public CollisionManager collisionManager;
	public UIManager uiManager;
	public SoundManager soundManager;

	public List<Character> characters { get; private set;}

	public bool paused { get; private set;}

	public float gravity { get; private set;}

	public float lightLevel {
		get {
			return 100f;
		} 
		private set {
			;}
	}

	public enum State {menu, camp, mine, pause, minigame}
	private State state;
	private State previousState;

    void Awake () {
		instance = this;

		collisionManager = new CollisionManager();
        uiManager.initialize();
		soundManager.initialize ();

		characters = new List<Character> ();

		gravity = 9.8f;
	}

	void Update() {

		foreach (Character character in characters) {

			character.update ();
		}
	}

	private void pause() {
		//PAUSE
	}

	private void unpause() {
		//UNPAUSE
	}

	public void changeZone(int zone) {
		StartCoroutine (moveCamera(0.5f, zone));
	}

	public void addCharacter(Character character) {
		characters.Add (character);
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
