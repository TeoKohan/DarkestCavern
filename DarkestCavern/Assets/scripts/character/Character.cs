using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : MonoBehaviour {

	const float movespeed = 5f;
	const float pickUpDistance = 1f;

	public Inventory inventory { get; protected set; }

	protected int currentZone;

	protected enum Facing {left, right}
	protected Facing facing;

	protected enum State {idle, walking, mining, locked}
	protected State state;
	protected State previousState;

	protected SpriteManager spriteManager;
	protected GameManager gameManager;


	public void initialize () {

		this.inventory = new Inventory (new Bag (), new Helmet (), new Pickaxe ());

		spriteManager = new SpriteManager ();
		gameManager = GameManager.instance;


		updateZone ();
		state = State.idle;
	}

	public void initialize (Inventory inventory) {
		
		this.inventory = inventory;
		gameManager = GameManager.instance;

		updateZone ();
		state = State.idle;
	}

	public void update () {
		updateGraphics();
	}

	public void update (CharacterInputData inputData) {
		handleAction (inputData);
		updateZone ();
		updateGraphics();
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

	protected void pause() {
		changeState (State.locked);
	}

	protected void unpause() {
		restorePreviousState ();
	}
}
