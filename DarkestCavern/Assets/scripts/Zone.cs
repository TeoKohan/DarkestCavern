using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {
	public Node[] nodes;

	public void activateNodes() {

		foreach (Node N in nodes) {
			N.activate ();
		}
	}
}
