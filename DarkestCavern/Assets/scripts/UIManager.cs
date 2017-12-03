using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Image[] prompts;
	public Sprite[] buttons;

	private Dictionary<PickaxeAction, Sprite> sprites;

	public void updateUI(UIData data) {
		
	}

	public void Start() {
		
		sprites = new Dictionary<PickaxeAction, Sprite> ();
		sprites.Add (PickaxeAction.left_arrow, buttons[0]);
		sprites.Add (PickaxeAction.up_arrow, buttons[1]);
		sprites.Add (PickaxeAction.right_arrow, buttons[2]);
		sprites.Add (PickaxeAction.down_arrow, buttons[3]);
		hideButtons();
	}


	public void showButtons(PickaxeAction[] actions) {
		int n = Mathf.Clamp (actions.Length, 0, 12);
		for (int i = 0; i < n; i++) {
			Image prompt = prompts [i];
			prompt.gameObject.SetActive (true);
			Sprite sprite;
			sprites.TryGetValue (actions[i], out sprite);
			prompt.sprite = sprite;
			}

		if (n <= 6) {
			for (int i = 0; i < n; i++) {
				prompts [i].rectTransform.position = new Vector3 ((i - n / 2 + 0.5f * n % 2) * 100, 0, 0);
			}
		}
		else if (n % 2 == 0) {
			for (int i = 0; i < n / 2 + 1; i++) {
				prompts [i].rectTransform.position = new Vector3 ((i - n / 2 + 1) * 100, 100, 0);
			}
			for (int i = n / 2 + 1; i < n; i++) {
				prompts [i].rectTransform.position = new Vector3 ((i - n / 2) * 100, 0, 0);
			}
		} 
		else {
			for (int i = 0; i < n / 2; i++) {
				prompts [i].rectTransform.position = new Vector3 ((i - n / 4) * 100, 100, 0);
			}
			for (int i = n / 2; i < n; i++) {
				prompts [i].rectTransform.position = new Vector3 ((i - n / 4) * 100, 0, 0);
			}
		}
	}

	public void hideButtons (){
		foreach (Image I in prompts) {
			I.gameObject.SetActive (false);
		}
	}
}
