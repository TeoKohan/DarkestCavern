using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	protected static List<Node> nodeList = new List<Node>();

	public HPBar hpBar;
	public Transform hotspot;
	public GameObject ore;
	public int oreQuantity;

	public int maxhp;
	public int armor;

	public int hp { get; private set;}
	public bool active { get; private set;}

	public static Node getNode(Vector3 position, float range) {
		
		foreach (Node N in Node.nodeList) {
			if (N.active) {
				float distance = Vector3.Distance (position, N.hotspot.position);
				if (distance <= range) {
					return N;
				}
			}
		}

		return null;
	}

	void Start() {
		nodeList.Add (this);
		active = true;
	}

	public void reset() {
		hp = maxhp;
		activate ();
	}

	public void activate() {
		active = true;
	}

	public void deactivate() {
		active = false;
	}

	public void damage (int damage) {
		if (active) {
			hp -= damage;
			hp = Mathf.Clamp (hp, 0, maxhp);
			hpBar.updatePercentage ((float)hp / (float)maxhp * 100f);
			checkDeath ();
		}
	}

	protected void checkDeath() {
		if (hp <= 0) {
			yieldResources ();
			GameManager.instance.minigameManager.nodeDestroyed ();
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

		Material material = this.GetComponent<Renderer> ().material;
		float startTime = Time.time;
		while (Time.time - startTime < seconds) {
			material.SetFloat ("_Desaturation", Mathf.Clamp01((Time.time - startTime) / seconds));
			yield return null;
		}
		material.SetFloat ("_Desaturation", 1f);
	}
}
