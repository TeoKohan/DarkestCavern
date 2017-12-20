using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame {

	public abstract void start();

	public abstract bool update ();

	protected abstract bool finish ();

    public abstract bool win ();
}
