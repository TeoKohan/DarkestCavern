using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameManager instance { get; private set;}
	public InputManager inputManager { get; private set;}
	public UIManager uiManager { get; private set;}
	public Character currentCharacter { get; private set;}

	public int[] ores { get; private set;}

	void Awake () {
		instance = this;
		inputManager = new InputManager ();
		uiManager = new UIManager ();

		int oreTypes = System.Enum.GetNames (typeof(Ore)).Length;
		ores = new int[oreTypes];
	}

	void Update() {
		inputManager.handleInput ();
		uiManager.updateUI (new UIData());
	}

	public void pickupOre(Ore ore) {
		ores [(int)ore]++;
	}
}
