using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

	public float border;
	public Image frame;
	public Image backdrop;
	public Image healthbar;

	const int size = 10;

	void Start() {
		updatePercentage(100);
		//hide();
	}

	public void updatePercentage(float percentage) {
		healthbar.rectTransform.localScale = new Vector2(percentage, 1f);

		if (percentage >= 1f || percentage <= 0f) {
			hide ();
		} 
		else {
			show ();
		}
	}

	public void setSize(Vector2 scale) {
		frame.rectTransform.sizeDelta = new Vector2(scale.x, scale.y);
		backdrop.rectTransform.sizeDelta = new Vector2(scale.x - border, scale.y - border);
		healthbar.rectTransform.sizeDelta = new Vector2(scale.x - border, scale.y - border);
	}

	public void hide() {
		frame.gameObject.SetActive (false);
		backdrop.gameObject.SetActive (false);
		healthbar.gameObject.SetActive (false);
	}

	public void show() {
		frame.gameObject.SetActive (true);
		backdrop.gameObject.SetActive (true);
		healthbar.gameObject.SetActive (true);
	}
}
