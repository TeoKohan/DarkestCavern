using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe {
	
	public Minigame minigame;
	protected int _damage;
	protected int _range;

	public int damage {

		get {
			return _damage;
		}

		set {
			_damage = Mathf.Clamp (value, 1, 100);
		}
	}

	public int range {

		get {
			return _range;
		}

		set {
			_range = Mathf.Clamp (value, 5, 50);
		}
	}


	public Pickaxe () {
		this.damage = 10;
		this.range = 10;
		this.minigame = new BasicMinigame ();
	}

	public Pickaxe (int damage, Minigame minigame) {
		this.damage = damage;
		this.minigame = minigame;
	}

	public void startMinigame(Character c, Node n) {
		minigame.start (4 + damage / 10, c, n);
	}
}
