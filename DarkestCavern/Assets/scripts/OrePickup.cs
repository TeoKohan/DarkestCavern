using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrePickup : MonoBehaviour {

	public static List<Transform> oreList = new List<Transform>();

	public Ore type;

	private Vector3 motion;
	private bool active = false;

	void Start () {

		oreList.Add (transform);
		Invoke ("activate", 1f);
	}

	public void pickup() {
		if (active) {
			GameManager.instance.currentCharacter.bag.addOre (type, 1);
			GameManager.instance.ores.Remove (this);
			Destroy (gameObject);
		}
	}
		
	private void activate() {
		active = true;
	}
}
