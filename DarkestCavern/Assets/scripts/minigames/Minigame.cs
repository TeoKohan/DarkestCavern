using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame {

	public abstract void handleInput (PickaxeAction pa);

	public abstract void start (int d, Character c, Node n);

	public abstract bool finish();
}