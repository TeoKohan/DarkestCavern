using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet {

	public float duration { get; set; }
	public float radius { get; set; }

	public Helmet () {
		this.duration = 5f;
		this.radius = 1f;
	}

	public Helmet (float duration, float radius) {
		this.duration = duration;
		this.radius = radius;
	}

	/*public void setEndTime() {
		startTime = Time.time;
		endTime = startTime + duration;
	}

	public float getLightPercentage() {
		return (endTime - (Time.time - startTime)) / duration;
	}*/

}
