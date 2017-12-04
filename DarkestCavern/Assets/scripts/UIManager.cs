using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject UI;

	private Transform lightBulb;
	private Material shadowPlane;

	public Image[] prompts;
	public Sprite[] buttons;

	public Image[] ores;
	public Text[] oreQuantity;

	public Image lampIcon;
	public Sprite[] lampStates;
	public HPBar lamp;

	private GameManager gameManager;
	private Dictionary<PickaxeAction, Sprite> sprites;

	void Start() {

		gameManager = GameManager.instance;

		sprites = new Dictionary<PickaxeAction, Sprite> ();
		sprites.Add (PickaxeAction.left_arrow, Resources.Load("left_arrow", typeof(Sprite)) as Sprite);
		sprites.Add (PickaxeAction.up_arrow, Resources.Load("up_arrow", typeof(Sprite)) as Sprite);
		sprites.Add (PickaxeAction.right_arrow, Resources.Load("right_arrow", typeof(Sprite)) as Sprite);
		sprites.Add (PickaxeAction.down_arrow, Resources.Load("down_arrow", typeof(Sprite)) as Sprite);
		sprites.Add (PickaxeAction.spacebar, Resources.Load("spacebar", typeof(Sprite)) as Sprite);
		hideButtons();
	}

	public void updateUI(Inventory inventory) {

		float lightLevel = gameManager.lightLevel;

		if (lightBulb == null) {
			lightBulb = GameObject.Find ("lightBulb").transform;
		}

		if (shadowPlane == null) {
			shadowPlane = GameObject.Find ("shadowPlane").GetComponent<Renderer> ().material;
		}

		Vector3 lightPosition = Camera.main.WorldToScreenPoint (lightBulb.position);
		lightPosition = Camera.main.ScreenToWorldPoint(new Vector3(lightPosition.x, lightPosition.y, 4.5f));
		shadowPlane.SetVector ("_LightPosition", lightPosition);
		shadowPlane.SetFloat ("_Power", lightLevel);
	}

	public void camp() {
		hideButtons ();
		hideLight ();
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
