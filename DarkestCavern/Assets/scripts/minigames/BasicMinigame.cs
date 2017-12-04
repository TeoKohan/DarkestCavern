using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMinigame : Minigame {

	protected List<PickaxeAction> availableKeys;
	protected PickaxeAction[] keyList;
	protected int currentKey;

	protected Character character;
	protected Node node;

	protected float time;
	protected int errors;

	public BasicMinigame () {
		availableKeys = new List<PickaxeAction> ();
		availableKeys.Add (PickaxeAction.left_arrow);
		availableKeys.Add (PickaxeAction.up_arrow);
		availableKeys.Add (PickaxeAction.right_arrow);
		availableKeys.Add (PickaxeAction.down_arrow);
		availableKeys.Add (PickaxeAction.spacebar);
	}

	public override void start (int keys, Character character, Node node) {

		this.character = character;
		this.node = node;

		keyList = new PickaxeAction[keys];
		for (int i = 0; i < keys; i++) {
			keyList [i] = availableKeys [(Random.Range (0, availableKeys.Count))];
		}
		currentKey = 0;
		displayKeys();
	}
		
	protected void displayKeys() {
		GameManager.instance.uiManager.showButtons (keyList);
	}

	public override void handleInput(PickaxeAction action) {
		if (action != PickaxeAction.idle) {
			if (action == keyList [currentKey]) {
				currentKey++;
				node.damage(character.pickaxe.damage - node.armor);
				if (currentKey >= keyList.Length) {
					node.damage(character.pickaxe.damage * 2);
					finish ();
				}
			}
		} 
	}

	public override bool finish() {
		//GameManager.instance;
		//DAMAGE NODE
		//UNLOCK CHARACTER
		character.finishMining();
		GameManager.instance.uiManager.hideButtons ();
		return true;
	}
}
