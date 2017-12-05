using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet {

	public float duration { get; set; }
	public float radius { get; set; }

	public Helmet () {
		this.duration = 30f;
		this.radius = 1f;
	}

	public Helmet (float duration, float radius) {
		this.duration = duration;
		this.radius = radius;
	}
}
