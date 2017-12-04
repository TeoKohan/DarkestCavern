using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject UI;

	public Material screenBlack;
	public Transform helmet;

	public Image[] prompts;
	public Sprite[] buttons;

	public Image[] ores;
	public Text[] oreQuantity;

	public Image lampIcon;
	public Sprite[] lampStates;
	public HPBar lamp;

	private GameManager gameManager;
	private Dictionary<PickaxeAction, Sprite> sprites;

	void Start () {
		gameManager = GameManager.instance;
	}

	public void updateUI(Inventory inventory) {
		for (int i = 0; i < oreQuantity.Length; i++) {
			oreQuantity [i].text = data.ores [i].ToString() + " / " + GameManager.instance.currentCharacter.bag.size;
			if (data.ores [i] <= 0) {
				ores [i].gameObject.SetActive (false);
				oreQuantity [i].gameObject.SetActive (false);
			} 
			else {
				ores [i].gameObject.SetActive (true);
				oreQuantity [i].gameObject.SetActive (true);
			}
		}

		float p = gameManager.currentCharacter.lamp.getLightPercentage ();
		if (p <= 0f) {
			GameManager.instance.blackout ();
		}

		else {
			if (p > 0.666f) {
				lampIcon.sprite = lampStates [0];
			} 
			else if (p > 0.333f) {
				lampIcon.sprite = lampStates [1];
			}
			else {
				lampIcon.sprite = lampStates [2];
			}
			lamp.updatePercentage (p * 100);
		}

		Vector3 h = Camera.main.WorldToScreenPoint (helmet.position);
		h = Camera.main.ScreenToWorldPoint(new Vector3(h.x, h.y, 4.5f));
		screenBlack.SetVector ("_LightPosition", h);
		screenBlack.SetFloat ("_Power", p);
	}

	public void camp() {
		hideButtons ();
		hideLight ();
	}

	public void Start() {

		DontDestroyOnLoad (UI);

		sprites = new Dictionary<PickaxeAction, Sprite> ();
		sprites.Add (PickaxeAction.left_arrow, buttons[0]);
		sprites.Add (PickaxeAction.up_arrow, buttons[1]);
		sprites.Add (PickaxeAction.right_arrow, buttons[2]);
		sprites.Add (PickaxeAction.down_arrow, buttons[3]);
		sprites.Add (PickaxeAction.spacebar, buttons[4]);
		hideButtons();
	}

	public void hideLight() {
		lamp.hide ();
		lampIcon.gameObject.SetActive (false);
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

		float w = Screen.width;
		float spacing = Mathf.Clamp(w / n, 100f, 150f);

		if (n <= 6) {
			for (int i = 0; i < n; i++) {
				prompts [i].rectTransform.anchoredPosition = new Vector3 ((i - (float)n / 2f + 0.5f) * spacing, 0);
			}
		}

		else if (n % 2 == 0) {
			for (int i = 0; i < n / 2; i++) {
				prompts [i].rectTransform.anchoredPosition = new Vector3 ((i - (float)n / 4f + 0.5f) * spacing, spacing);
			}
			for (int i = n / 2; i < n; i++) {
						prompts [i].rectTransform.anchoredPosition = new Vector3 ((i - (float)n * 3f / 4f + 0.5f) * spacing, 0);
			}
		}

		else {
			for (int i = 0; i < n / 2; i++) {
				prompts [i].rectTransform.anchoredPosition = new Vector3 ( (i - (float)(n / 2) / 2f+ 0.5f) * spacing, spacing);
			}
			for (int i = n / 2; i < n; i++) {
				prompts [i].rectTransform.anchoredPosition = new Vector3 ((i - n / 2 - (float)(n / 2) / 2f) * spacing, 0);
			}
		}
	}

	public void hideButtons (){
		foreach (Image I in prompts) {
			I.gameObject.SetActive (false);
		}
	}
}
