﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe {

	public Minigame minigame { get; protected set; }
	public int damage { get; protected set; }
	public float range { get; protected set; }

	public Pickaxe () {
		this.damage = 1;
		this.range = 2f;
		this.minigame = new Minigame ();
	}

	public Pickaxe (int damage, Minigame minigame) {
		this.damage = damage;
		this.minigame = minigame;
	}
}
