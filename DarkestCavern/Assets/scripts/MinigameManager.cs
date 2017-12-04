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

		int keys = 2 + pickaxe.damage / 10 - node.armor;
		float duration = keys * Mathf.Clamp (10 / pickaxe.damage - node.armor, 0.5f, 3f);
		int errors = 5 - pickaxe.damage / 25;
		minigame.start (keys, duration, errors);
		displayKeys (minigame.keyList);

		state = State.active;
	}
		
	private void displayKeys(PickaxeAction[] keyList) {
		gameManager.uiManager.showButtons (keyList);
	}
		
	public void handleInput(PickaxeAction action) {
		
		if (state == State.active) {
			
			Minigame.response response = mineSession.minigame.handleInput (action);
			switch (response) {

			case Minigame.response.correct:
				
				gameManager.soundManager.hit ();
				if (mineSession.minigame.currentKey >= mineSession.minigame.keyList.Length) {
					if (mineSession.node.damage (mineSession.pickaxe.damage)) {
						finish (mineSession);
					}
				}
				break;

			case Minigame.response.incorrect:
				if (mineSession.minigame.errors <= 0) {
					gameManager.soundManager.incorrect ();
					finish (mineSession);
				}
				break;

			default:
				break;
			}
		}
	}

	private void finish(MineSession mineSession) {
		gameManager.uiManager.hideButtons ();
		gameManager.endMinigame ();
		state = State.inactive;
	}
}
