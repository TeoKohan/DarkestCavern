using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningNode : MonoBehaviour {

	public GameObject ore;

	public int maxhp { get; private set;}
	public int hp { get; private set;}
	public int oreQuantity { get; private set;}

	public MiningNode (int hp) {
		maxhp = hp;
		this.hp = hp;
	}

	public void damage (int damage) {
		hp -= damage;
		hp = Mathf.Clamp (hp, 0, maxhp);
		checkDeath ();
		Debug.Log (hp);
	}

	private void checkDeath() {
		if (hp <= 0) {
			yieldResources ();
		}
	}

	private void yieldResources() {
		for (int i = 0; i < oreQuantity; i++) {
			Instantiate (ore, transform.position, Quaternion.identity);
		}
		StartCoroutine (fade (1));
	}

	IEnumerator fade(int seconds) {

		float startTime = Time.time;
		while (Time.time - startTime < seconds) {
			setDesaturation (Mathf.Clamp01((Time.time - startTime) / seconds));
			Debug.Log (Mathf.Clamp01((Time.time - startTime) / seconds));
			yield return null;
		}
		setDesaturation (1f);
	}

	private void setDesaturation (float f) {
		this.GetComponent<Renderer> ().material.SetFloat ("_Desaturation", f);
	}
}
