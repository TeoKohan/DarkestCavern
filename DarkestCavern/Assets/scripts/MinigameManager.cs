using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager {

	public struct MineSession {
		public Minigame minigame;
		public Pickaxe pickaxe;
		public Node node;

		public MineSession (Minigame minigame, Pickaxe pickaxe, Node node) {

			this.minigame = minigame;
			this.pickaxe = pickaxe;
			this.node = node;
		}
	}

	private MineSession mineSession;

	enum State {active, inactive}
	private State state;

	private GameManager gameManager;

	public MinigameManager () {

		gameManager = GameManager.instance;
	}

	public void startMinigame(Pickaxe pickaxe, Node node) {

		Minigame minigame = pickaxe.minigame;

		mineSession = new MineSession(minigame, pickaxe, node);

		int keys = Mathf.Clamp(2 + pickaxe.damage / 10 - node.armor, 1, 12);
		float duration = keys * Mathf.Clamp (10 / pickaxe.damage - node.armor, 0.5f, 3f);
		int errors = Mathf.Max(2 + keys / 4 - pickaxe.damage / 25 - node.armor, 1);
		minigame.start (keys, duration, errors);
		displayKeys (minigame.keyList);

		state = State.active;
	}
		
	private void displayKeys(PickaxeAction[] keyList) {
		gameManager.uiManager.showPrompts (keyList);
	}
		
	public void handleInput(PickaxeAction action) {
		
		if (state == State.active) {
			
			Minigame.Response response = mineSession.minigame.handleInput (action);
			switch (response) {

			case Minigame.Response.correct:
				
				gameManager.soundManager.hit ();
				if (mineSession.minigame.currentKey >= mineSession.minigame.keyList.Length) {
					mineSession.node.damage (mineSession.pickaxe.damage);
					finish (mineSession);
				} 
				else {
					gameManager.uiManager.updateButtons (mineSession.minigame.keyList, mineSession.minigame.keystates);
				}
				break;

			case Minigame.Response.incorrect:

				gameManager.soundManager.incorrect ();

				if (mineSession.minigame.errors <= 0) {
					finish (mineSession);
				} 

				else {
					if (mineSession.minigame.currentKey >= mineSession.minigame.keyList.Length) {
						mineSession.node.damage (mineSession.pickaxe.damage);
						finish (mineSession);
					} 
					else {
						gameManager.uiManager.updateButtons (mineSession.minigame.keyList, mineSession.minigame.keystates);
					}
				}
				break;

			default:
				break;
			}
		}
	}

	private void finish(MineSession mineSession) {
		gameManager.endMinigame ();
		state = State.inactive;
	}
}
