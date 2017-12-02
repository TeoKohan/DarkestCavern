using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public Pickaxe pickaxe { get; private set; }
	public Lamp lamp { get; private set; }
	private float movespeed;

	enum State {idle, walking, mining, locked}
	State state;

	public void initialize() {
		
	}

	public void update () {
		
	}

	public void handleAction (Action action) {

		//STATES
		switch (state) {

		//IDLE STATE
		case State.idle:
			
			switch (action) {
			case Action.action:
				state = State.mining;
				attemptMining ();
				break;
			case Action.move:
				state = State.walking;
				move ();
				break;
			}
			break;

		//WALKING STATE
		case State.walking:
			
			switch (action) {
			case Action.idle:
				state = State.idle;
				break;
			case Action.action:
				state = State.mining;
				attemptMining ();
				break;
			case Action.move:
				move ();
				break;
			}
			break;

		//MINING STATE
		case State.mining:
			
			switch (action) {
			case Action.finish_action:
				state = State.idle;
				finishMining ();
				break;
			}
			break;

		//LOCKED STATE
		case State.locked:
			break;

		default:
			break;
		
		}
	}

	private void move() {
		
	}

	private void attemptMining() {
		
	}

	private void finishMining() {

	}

	private void pause() {
		
	}

	private void pauseError() {

	}

	private void mute() {

	}
}
