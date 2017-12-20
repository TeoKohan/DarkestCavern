using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputManager {

	public abstract float getMovement ();

	public abstract List<EntityState> getState();

}
