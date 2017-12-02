using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe {
	
	public Minigame minigame;
	protected int _damage;

	public int damage {

		get {
			return _damage;
		}

		set {
			_damage = Mathf.Clamp (value, 1, 100);
		}
	}

	public Pickaxe (int damage, Minigame minigame) {
		this.damage = damage;
		this.minigame = minigame;
	}
}
