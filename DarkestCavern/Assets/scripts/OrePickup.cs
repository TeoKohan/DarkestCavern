using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrePickup : MonoBehaviour {

	public Ore type;

	public float friction = 0.01f;
	public float gravity = 2f;
	public float maxHeight = 5f;
	public int bounces = 3;
	public float bounceStrength = 0.75f;
	public float radius = 1f;

	public float[] walls;

	private Vector3 motion;
	private bool active = false;

	void Start () {

		GameManager.instance.ores.Add (this);

		motion = new Vector3 (Random.Range(-maxHeight/4f, maxHeight/4f), Random.Range(maxHeight/2f, maxHeight), 0f);
		Invoke ("activate", 1f);
	}

	void Update() {
		
		applyFriction ();
		applyGravity ();
		float x = checkCollisions ();
		float y;

		if (bounces > 0) {
			y = checkBounces ();
		} 

		else {
			y = radius;
		}

		transform.position = new Vector3 (x, y, 0f);
	}

	public void pickup() {
		if (active) {
			GameManager.instance.currentCharacter.bag.addOre (type, 1);
			GameManager.instance.ores.Remove (this);
			Destroy (gameObject);
		}
	}
		
	private void activate() {
		active = true;
	}

	private void applyFriction() {
		motion *= (1 - friction);
	}

	private void applyGravity() {
		motion -= new Vector3 (0f, gravity * Time.deltaTime, 0f);
	}

	private float checkBounces() {
		if (transform.position.y + motion.y * Time.deltaTime - radius < 0) {
			float y = Mathf.Abs(motion.y * Time.deltaTime + (transform.position.y + radius)) * bounceStrength;
			motion = new Vector3(motion.x, Mathf.Abs(motion.y) * bounceStrength, 0);
			bounces--;
			return Mathf.Max(y, radius);
		}
		return transform.position.y + motion.y * Time.deltaTime;
	}

	private float checkCollisions() {
		foreach (float F in walls) {
			float prevPos = transform.position.x;
			float pos = (transform.position.x + motion.x * Time.deltaTime);
			if ((prevPos <= F && pos >= F) || (prevPos >= F && pos <= F)) {
				float x = transform.position.x + (F - (motion.x * Time.deltaTime + transform.position.x)) * -1 * bounceStrength;
				motion = new Vector3 (motion.x * -1 * bounceStrength, motion.y, 0);
				return x;
			}
		}
		return transform.position.x + motion.x * Time.deltaTime;
	}
}
