using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

	public struct VisualState {
	}

	public SpriteSequence idle;
	public SpriteSequence walk;
	public SpriteSequence mine;

	protected SpriteRenderer sprite;

	public void initialize() {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}

	protected void update() {

		switch (state) {

		case State.idle:
			sprite.sprite = idle.sprites [(int)(Time.time * 1 % idle.sprites.Length)];
			break;
		case State.walking:
			sprite.sprite = walk.sprites [(int)(Time.time * 6 % walk.sprites.Length)];
			setFacing (facing);
			break;
		case State.mining:
			sprite.sprite = mine.sprites[(int)(Time.time * 3 % mine.sprites.Length)];
			break;
		}
	}
}
