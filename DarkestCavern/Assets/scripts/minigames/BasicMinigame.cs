using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMinigame : Minigame {

	protected List<PickaxeAction> availableKeys;
	protected PickaxeAction[] keyList;
	protected PickaxeAction currentKey;

	public override void start (int keys) {
		keyList = new PickaxeAction[keys];
		for (int i = 0; i < keys; i++) {
			keyList [i] = (PickaxeAction)(Random.Range (1, System.Enum.GetNames (typeof(PickaxeAction)).Length));
		}
	}
	
	public override bool finish() {
		//GameManager.instance;
		return true;
	}
}
