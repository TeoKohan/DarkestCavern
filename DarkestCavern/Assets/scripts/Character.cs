using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : MonoBehaviour {

	const float movespeed = 5f;
	const float pickUpDistance = 1f;

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

	protected enum Facing {left, right}
	protected enum State {idle, walking, mining, locked}
	Facing facing;
	protected State state;
	protected State previousState;

	protected GameManager gameManager;
	protected SpriteRenderer sprite;


	public void initialize (Inventory inventory) {
		
		this.inventory = inventory;
		gameManager = GameManager.instance;

		updateZone ();
		state = State.idle;
	}

	void Start() {
		
		GameManager.instance.setCharacter (this);
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}

	public void update () {
		checkPickups ();
		updateGraphics();
	}

	public void update (CharacterInputData inputData) {
		handleAction (inputData);
		updateZone ();
		checkPickups ();
		updateGraphics();
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
			sprite.sprite = walk [(int)(Time.time * 6 % walk.Length)];
			helmetSprite.transform.localPosition = helmetWalkPositions [(int)(Time.time * 6 % helmetWalkPositions.Length)];
			pickaxeSprite.transform.localPosition = pickaxeWalkPositions [(int)(Time.time * 6 % pickaxeWalkPositions.Length)];
			pickaxeSprite.transform.rotation = Quaternion.Euler (0, 0, pickaxeWalkRotations [(int)(Time.time * 6 % pickaxeWalkRotations.Length)] * transform.localScale.x);
			setFacing (facing);
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

		//STATES
		switch (state) {

		//IDLE STATE
		case State.idle:

			switch (characterAction) {
			case CharacterAction.action:
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
				attemptMining ();
				break;
			case CharacterAction.move:
				move (inputData.movement);
				break;
			}
			break;

			//MINING STATE
		case State.mining:
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

	protected void move(float movement) {
		
		if (movement >= 0) { facing = Facing.right;}
		else { facing = Facing.left;}

		float result = Wall.checkCollision (transform.position.x + movement * movespeed * Time.deltaTime);
		transform.position = new Vector3(result, transform.position.y, transform.position.z);
	}

	protected void setFacing(Facing facing) {
		switch (facing) {
		case Facing.right:
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			break;
		case Facing.left:
			transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);
			break;
		default:
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			break;
		}
	}

	protected void updateZone() {

		//Reimplement if theres enough time
		int newZone = Mathf.Clamp((int)(transform.position.x + 9.5f) / 19, 0, 5);
		if (newZone != currentZone) {
			currentZone = newZone;
			gameManager.changeZone (newZone);
		}
	}

	protected void attemptMining() {
		
		Node node = Node.getNode(transform.position, inventory.pickaxe.range);

		if (node != null) {
			if (gameManager.startMinigame (inventory.pickaxe, node)) {
				changeState (State.mining);
			}
		} 
		else {
			finishMining ();
		}
    }

	public void finishMining() {
		changeState (State.idle);
	}

	private void checkPickups() {

		List<OrePickup> picked = new List<OrePickup>();

		foreach (OrePickup OP in OrePickup.oreList) {
			
			Vector2 characterPosition = new Vector2 (transform.position.x, transform.position.y);
			Vector2 orePosition = new Vector2 (OP.transform.position.x, OP.transform.position.y);
			float distance = Vector2.Distance (characterPosition, orePosition);

			if (distance <= pickUpDistance) {
				picked.Add (OP.GetComponent<OrePickup>());
			}
		}

		foreach (OrePickup OP in picked) {
			OP.pickup ();
		}
	}

	protected void pause() {
		changeState (State.locked);
	}

	protected void unpause() {
		restorePreviousState ();
	}
}
