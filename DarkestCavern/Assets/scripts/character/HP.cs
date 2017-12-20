using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP {

	const int defaultMaxHP = 100;

	public int maxHP { get; private set;}
	public int currentHP { get; private set;}

	public HP() {
		maxHP = defaultMaxHP;
		currentHP = maxHP;
	}

	public HP(int maxHP) {
		this.maxHP = maxHP;
		currentHP = maxHP;
	}

	public HP(int maxHP, int currentHP) {
		this.maxHP = maxHP;
		this.currentHP = currentHP;
		clampHP ();
	}

	public void changeMaxHP (int changeAmount) {
		maxHP += changeAmount;
		clampHP();
	}

	public void damage(int damage) {
		currentHP -= damage;
		clampHP ();
	}

	public void heal(int heal) {
		currentHP += heal;
		clampHP ();
	}

	private void clampHP() {
		currentHP = Mathf.Clamp (currentHP, 0, maxHP);
	}
}
