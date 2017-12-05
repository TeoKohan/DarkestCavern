using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	protected static List<Node> nodeList = new List<Node>();

	public GameObject ores;
	public Bar hpBar;
	public Transform hotspot;
	public Ore ore;
	public int oreQuantity;

	public int maxhp;
	public int armor;

	public int hp { get; protected set;}
	public bool active { get; protected set;}

	protected Material material;

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

	public static void refresh() {
		foreach (Node N in Node.nodeList) {
			N.reset ();
		}
	}

	public virtual void Start() {
		material = this.GetComponent<Renderer> ().material;
		nodeList.Add (this);
		reset ();
	}

	public virtual void reset() {
		hp = maxhp;
		hpBar.updatePercentage ((float)hp / (float)maxhp * 100f);
		material.SetFloat ("_Desaturation", 0);
		activate ();
		ores.SetActive (true);
	}

	public void activate() {
		active = true;
	}

	public void deactivate() {
		active = false;
	}

	public bool damage (int damage) {
		if (active) {
			hp -= damage - armor;
			hp = Mathf.Clamp (hp, 0, maxhp);
			hpBar.updatePercentage ((float)hp / (float)maxhp * 100f);

			return checkDeath ();
		}

		return false;
	}

	protected bool checkDeath() {
		if (hp <= 0) {
			yieldResources ();
			return true;
		}

		return false;
	}

	protected virtual void yieldResources() {
		GameManager.instance.character.inventory.bag.addOre (ore, oreQuantity);
		active = false;
		ores.SetActive (false);
		StartCoroutine (fade (1));
	}

	IEnumerator fade(int seconds) {
		
		float startTime = Time.time;
		while (Time.time - startTime < seconds) {
			material.SetFloat ("_Desaturation", Mathf.Clamp01((Time.time - startTime) / seconds));
			yield return null;
		}
		material.SetFloat ("_Desaturation", 1f);
	}
}
