using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag {

	const int baseSize = 10;

	public int size { get; protected set;}
	protected Dictionary<Ore, int> ores;

	public Bag () {

		ores = new Dictionary<Ore, int> ();
		size = baseSize;
	}

	public int getOre (Ore ore) {
		return ores [ore];
	}

	public bool addOre(Ore ore, int number){
		
		int result = ores [ore] + number;

		if (result <= size) {
			ores [ore] = result;
			return true;
		} 

		else {
			return false;
		}
	}

	public bool subtractOre(Ore ore, int n){
		int result = ores [ore] - n;
		if (result >= 0) {
			ores [ore] = result;
			return true;
		} 

		else {
			return false;
		}
	}
}
