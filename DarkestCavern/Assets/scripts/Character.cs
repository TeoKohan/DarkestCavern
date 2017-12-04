using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : MonoBehaviour {

	const float movespeed = 5f;

		public Sprite[] idle;
		public Sprite[] walk;
		public Sprite[] mine;

		public GameObject helmetSprite;

		public Vector3[] helmetIdlePositions;
		public Vector3[] helmetWalkPositions;
		public Vector3[] helmetMinePositions;

		public GameObject pickaxeSprite;

		public Vector3[] pickaxeIdlePositions;
		public float[] pickaxeIdleRotations;
		public Vector3[] pickaxeWalkPositions;
		public float[] pickaxeWalkRotations;
		public Vector3[] pickaxeMinePositions;
		public float[] pickaxeMineRotations;

	public Inventory inventory { get; protected set; }

	protected int currentZone;

	protected enum State {idle, walking, mining, locked}
	protected State state;
	protected State previousState;

	protected GameManager gameManager;
	protected SpriteRenderer sprite;


	public void initialize (Inventory inventory) {
		
		this.inventory = inventory;
		gameManager = GameManager.instance;

		currentZone = updateZone ();
		state = State.idle;
	}

	void Start() {
		
		GameManager.instance.setCharacter (this);
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}

	public void update (CharacterInputData inputData) {
		handleAction (inputData);
		updateGraphics();
		updateZone ();
	}

	protected void updateGraphics() {

		switch (state) {
		case State.idle:
			sprite.sprite = idle [(int)(Time.time * 1 % idle.Length)];
			helmetSprite.transform.localPosition = helmetIdlePositions [(int)(Time.time * 1 % helmetIdlePositions.Length)];
			pickaxeSprite.transform.localPosition = pickaxeIdlePositions [(int)(Time.time * 1 % pickaxeIdlePositions.Length)];
			pickaxeSprite.transform.rotation =Quaternion.Euler(0, 0, pickaxeIdleRotations [(int)(Time.time * 1 % pickaxeIdleRotations.Length)] * transform.localScale.x);
			break;
		case State.walking:
			sprite.sprite = walk[(int)(Time.time * 6 % walk.Length)];
			helmetSprite.transform.localPosition = helmetWalkPositions [(int)(Time.time * 6 % helmetWalkPositions.Length)];
			pickaxeSprite.transform.localPosition = pickaxeWalkPositions [(int)(Time.time * 6 % pickaxeWalkPositions.Length)];
			pickaxeSprite.transform.rotation = Quaternion.Euler(0, 0, pickaxeWalkRotations [(int)(Time.time * 6 % pickaxeWalkRotations.Length)] * transform.localScale.x);
			break;
		case State.mining:
			sprite.sprite = mine[(int)(Time.time * 3 % mine.Length)];
			helmetSprite.transform.localPosition = helmetMinePositions [(int)(Time.time * 3 % helmetMinePositions.Length)];
			pickaxeSprite.transform.localPosition = pickaxeMinePositions [(int)(Time.time * 3 % pickaxeMinePositions.Length)];
			pickaxeSprite.transform.rotation =Quaternion.Euler(0, 0, pickaxeMineRotations [(int)(Time.time * 3 % pickaxeMineRotations.Length)] * transform.localScale.x);
			break;
		}
	}

	protected void handleAction (CharacterInputData inputData) {

		CharacterAction characterAction = inputData.characterAction;
		PickaxeAction pickaxeAction = inputData.pickaxeAction;

		//STATES
		switch (state) {

		//IDLE STATE
		case State.idle:

			switch (characterAction) {
			case CharacterAction.action:
				changeState (State.mining);
				attemptMining ();
				break;
			case CharacterAction.move:
				changeState (State.walking);
				move (inputData.movement);
				break;
			}
			break;

			//WALKING STATE
		case State.walking:

			switch (characterAction) {
			case CharacterAction.idle:
				changeState (State.idle);
				break;
			case CharacterAction.action:
				changeState (State.mining);
				attemptMining ();
				break;
			case CharacterAction.move:
				changeState (State.walking);
				move (inputData.movement);
				break;
			}
			break;

			//MINING STATE
		case State.mining:
			//pickaxe.minigame.handleInput (pickaxeAction);
			break;

			//LOCKED STATE
		case State.locked:
			break;

		default:
			break;

		}
	}

	protected void changeState(State newState) {
		previousState = state;
		state = newState;
	}

	protected void restorePreviousState() {
		State temp = previousState;
		previousState = state;
		state = temp;
	}

	protected void move(float xMovement) {

		if (xMovement >= 0) {
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		}

		else {
			transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);
		}

		float result = GameManager.instance.checkCollision (transform.position.x + xMovement * movespeed * Time.deltaTime);
		transform.position = new Vector3(result, transform.position.y, transform.position.z);
	}

	protected int updateZone() {

		//Reimplement if theres enough time
		return Mathf.Clamp((int)(transform.position.x + 9.5f) / 19, 0, 5);
	}

	protected void attemptMining() {
		
		Node n;
		n = gameManager.getNode (0);
		if (n != null) {
			//pickaxe.startMinigame (this, n);
		} 
		else {
			finishMining ();
		}
    }

	public void finishMining() {
		changeState (State.idle);
	}

	protected void pause() {
		changeState (State.locked);
	}

	protected void unpause() {
		restorePreviousState ();
	}
}
