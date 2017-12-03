using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

	public Image frame;
	public Image backdrop;
	public Image healthbar;

	const int size = 10;

	void Start() {
		updatePercentage(100);
		hide();
	}

	public void updatePercentage(float percentage) {
		healthbar.rectTransform.sizeDelta = new Vector2(percentage / 100f, 0.25f);
		if (percentage >= 100f || percentage <= 0f) {
			hide ();
		} 
		else {
			show ();
		}
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
