using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp {

	public float endTime;
	private float startTime;

	protected float _duration;
	protected float _radius;

	public float duration {
		
		get {
			return _duration;
		}

		set {
			_duration = Mathf.Clamp (value, 10f, 300f);
			GameManager.instance.uiManager.lamp.setSize (new Vector2 (300f * (duration/30f), 50f));
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

	public void setEndTime() {
		startTime = Time.time;
		endTime = startTime + duration;
	}

	public float getLightPercentage() {
		return (endTime - (Time.time - startTime)) / duration;
	}

}
