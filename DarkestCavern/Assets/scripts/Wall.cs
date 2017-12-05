using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Node {

	const int walls = 4;

	public static Wall[] wallList = new Wall[walls];
	public static int currentWall = 0;

	public int number;

	public override void Start() {
		material = this.GetComponent<Renderer> ().material;
		wallList [number] = this;
		nodeList.Add (this);
		reset ();
	}

	public static float checkCollision(float character) {
		return Mathf.Clamp (character, -7f, wallList [currentWall].hotspot.position.x);
	}

	protected override void yieldResources() {
		GameManager.instance.character.inventory.bag.addOre (ore, oreQuantity);
		active = false;
		gameObject.SetActive (false);

		currentWall++;
	}

}
