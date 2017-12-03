using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : MonoBehaviour {

	public Pickaxe pickaxe { get; protected set; }
	public Lamp lamp { get; protected set; }
	public Bag bag { get; protected set; }
	public int zone { get; protected set; }
	protected float movespeed;

	protected enum State {idle, walking, mining, locked}
	protected State state;
	protected State previousState;


	public void initialize (Pickaxe pickaxe, Lamp lamp, Bag bag, float movespeed) {
		this.pickaxe = pickaxe;
		this.lamp = lamp;
		this.bag = bag;
		this.movespeed = movespeed;

		zone = 0;
		state = State.idle;
	}

	void Start() {
		GameManager.instance.setCharacter (this);
	}

	public void update (CharacterInputData inputData) {
		handleAction (inputData);
	}

	public void handleAction (CharacterInputData inputData) {

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
			pickaxe.minigame.handleInput (pickaxeAction);
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

		float result = GameManager.instance.checkCollision (transform.position.x + xMovement * movespeed * Time.deltaTime);
		transform.position = new Vector3(result, transform.position.y, transform.position.z);
		checkZone ();
	}

	protected void checkZone() {
		int newZone = Mathf.Clamp((int)(transform.position.x + 9.5f) / 19, 0, 5);
		Debug.Log (zone);
		if (zone != newZone) {
			zone = newZone;
			GameManager.instance.changeZone (zone);
		}
	}

	protected void attemptMining() {
		
		Node n;
		n = GameManager.instance.getNode (0);
		if (n != null) {
			pickaxe.startMinigame (this, n);
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
