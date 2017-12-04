using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrePickup : MonoBehaviour {

	public static List<OrePickup> oreList = new List<OrePickup>();

	public Ore type;

	private Vector3 motion;
	private bool active = false;

	void Start () {

		oreList.Add (this);
		Invoke ("activate", 1f);
	}

	public void pickup() {
		if (active) {
			GameManager.instance.currentCharacter.inventory.bag.addOre (type, 1);
			oreList.Remove (this);
			Destroy (gameObject);
		}
	}
		
	private void activate() {
		active = true;
	}
}
