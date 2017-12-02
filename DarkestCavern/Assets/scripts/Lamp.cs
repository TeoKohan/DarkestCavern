using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp {
	
	protected float _duration;
	protected float _radius;

	public float duration {
		
		get {
			return _duration;
		}

		set {
			_duration = Mathf.Clamp (value, 30f, 300f);
		}
	}

	public float radius {

		get {
			return _radius;
		}

		set {
			_radius = Mathf.Clamp (value, 1f, 3f);
		}
	}

	public Lamp () {
		this.duration = 30f;
		this.radius = 1f;
	}

	public Lamp (float duration, float radius) {
		this.duration = duration;
		this.radius = radius;
	}

}
