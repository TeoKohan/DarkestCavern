using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag {

	protected int _size;
	protected int[] ores;

	public int size {

		get {
			return _size;
		}

		set {
			_size = Mathf.Clamp (value, 1, 100);
		}
	}

	public Bag () {

		ores = new int[System.Enum.GetNames (typeof(Ore)).Length];
		size = 5;
	}

	public int getOreQuantity(Ore ore) {
		return ores [(int)ore];
	}

	public bool addOre(Ore ore, int n){
		int result = ores [(int)ore] + n;
		if (result <= size) {
			ores [(int)ore] = result;
			return true;
		} 

		else {
			return false;
		}
	}

	public bool subtractOre(Ore ore, int n){
		int result = ores [(int)ore] - n;
		if (result >= 0) {
			ores [(int)ore] = result;
			return true;
		} 

		else {
			return false;
		}
	}
}
