using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public GameObject ore;

	public int maxhp;
	public int hp { get; private set;}
	public int oreQuantity;

	public bool active { get; private set;}

	public void activate() {
		active = true;
	}

	public void damage (int damage) {
		if (active) {
			hp -= damage;
			hp = Mathf.Clamp (hp, 0, maxhp);
			checkDeath ();
			Debug.Log (hp);
		}
	}

	protected void checkDeath() {
		if (hp <= 0) {
			yieldResources ();
		}
	}

	protected void yieldResources() {
		for (int i = 0; i < oreQuantity; i++) {
			Instantiate (ore, transform.position, Quaternion.identity);
		}
		active = false;
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

	protected void setDesaturation (float f) {
		this.GetComponent<Renderer> ().material.SetFloat ("_Desaturation", f);
	}
}
