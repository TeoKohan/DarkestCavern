using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public struct oreDisplay {
		public Image image;
		public Text text;

		public oreDisplay(Image image, Text text) {
			this.image = image;
			this.text = text;
		}
	}

	private Transform lightBulb;
	private Material shadowPlane;

	public Button startGameButton;

	public Button upgradePickaxeButton;
	public Button upgradeLampButton;
	public Button upgradeBagButton;
	public Button spelunkButton;

	public Image[] prompts;
	public Image[] oreImages;
	public Text[] oreText;
	public Image lightIcon;
	public Sprite[] lampStates;
	public Bar lightBar;

	private GameManager gameManager;
	private Dictionary<PickaxeAction, Sprite> arrowPrompts;
	private Dictionary<Sprite, Sprite> pressedArrowPrompts;
	private Sprite wrongPrompt;
	private Dictionary<Ore, oreDisplay> ores;

	public void initialize() {

		gameManager = GameManager.instance;

		startGameButton.onClick.AddListener (startGame);

		spelunkButton.onClick.AddListener (spelunk);

		upgradePickaxeButton.onClick.AddListener (upgradePickaxe);
		upgradeLampButton.onClick.AddListener (upgradeLamp);
		upgradeBagButton.onClick.AddListener (upgradeBag);

		arrowPrompts = new Dictionary<PickaxeAction, Sprite> ();
		arrowPrompts.Add (PickaxeAction.left_arrow, Resources.Load("left_arrow", typeof(Sprite)) as Sprite);
		arrowPrompts.Add (PickaxeAction.up_arrow, Resources.Load("up_arrow", typeof(Sprite)) as Sprite);
		arrowPrompts.Add (PickaxeAction.right_arrow, Resources.Load("right_arrow", typeof(Sprite)) as Sprite);
		arrowPrompts.Add (PickaxeAction.down_arrow, Resources.Load("down_arrow", typeof(Sprite)) as Sprite);
		arrowPrompts.Add (PickaxeAction.spacebar, Resources.Load("spacebar", typeof(Sprite)) as Sprite);

		pressedArrowPrompts = new Dictionary<Sprite, Sprite> ();
		Sprite sprite;
		arrowPrompts.TryGetValue (PickaxeAction.left_arrow, out sprite);
		pressedArrowPrompts.Add (sprite, Resources.Load("left_arrow_pressed", typeof(Sprite)) as Sprite);
		arrowPrompts.TryGetValue (PickaxeAction.up_arrow, out sprite);
		pressedArrowPrompts.Add (sprite, Resources.Load("up_arrow_pressed", typeof(Sprite)) as Sprite);
		arrowPrompts.TryGetValue (PickaxeAction.right_arrow, out sprite);
		pressedArrowPrompts.Add (sprite, Resources.Load("right_arrow_pressed", typeof(Sprite)) as Sprite);
		arrowPrompts.TryGetValue (PickaxeAction.down_arrow, out sprite);
		pressedArrowPrompts.Add (sprite, Resources.Load("down_arrow_pressed", typeof(Sprite)) as Sprite);
		arrowPrompts.TryGetValue (PickaxeAction.spacebar, out sprite);
		pressedArrowPrompts.Add (sprite, Resources.Load("spacebar_pressed", typeof(Sprite)) as Sprite);

		wrongPrompt = Resources.Load("wrong_pressed", typeof(Sprite)) as Sprite;

		ores = new Dictionary<Ore, oreDisplay> ();
		ores.Add (Ore.silver, new oreDisplay(oreImages[0], oreText[0]));
		ores.Add (Ore.gold, new oreDisplay(oreImages[1], oreText[1]));
		ores.Add (Ore.ruby, new oreDisplay(oreImages[2], oreText[2]));
		ores.Add (Ore.emerald, new oreDisplay(oreImages[3], oreText[3]));
		ores.Add (Ore.saphire, new oreDisplay(oreImages[4], oreText[4]));
	}

	private void startGame() {
		gameManager.changeState (GameManager.State.camp);
	}
		
	private void spelunk() {
		gameManager.changeState (GameManager.State.mine);
	}

	private void upgradePickaxe() {
		
	}

	private void upgradeLamp() {

	}

	private void upgradeBag() {

	}

	public void setMode (GameManager.State state) {
		switch (state) {
		case GameManager.State.menu:
			hideAll ();
			showMenuButtons ();
			break;
		case GameManager.State.camp:
			hideAll ();
			showCampButtons ();
			showOres ();
			break;
		case GameManager.State.mine:
			hideAll ();
			showLightMeter ();
			break;
		}
	}

	private void hideAll() {
		hideMenuButtons ();
		hideCampButtons ();
		hidePrompts ();
		hideLightMeter ();
		hideOres ();
	}

	private void showAll() {
		showMenuButtons ();
		showCampButtons ();
		showPrompts (new PickaxeAction[12]);
		showLightMeter ();
		showOres ();
	}

	public void updateUI(Inventory inventory, GameManager.State state) {

		switch (state) {
		case GameManager.State.menu:
			break;
		case GameManager.State.camp:
			break;
		case GameManager.State.mine:
			float lightLevel = gameManager.lightLevel;
			updateLightMeter (lightLevel);

			updateOres (inventory.bag.ores, inventory.bag.size);
			break;
		}
	}

	private void showMenuButtons() {
		startGameButton.gameObject.SetActive (true);
	}

	private void hideMenuButtons() {
		startGameButton.gameObject.SetActive (false);
	}

	private void showCampButtons() {
		upgradePickaxeButton.gameObject.SetActive (true);
		upgradeLampButton.gameObject.SetActive (true);
		upgradeBagButton.gameObject.SetActive (true);
		spelunkButton.gameObject.SetActive (true);

	}

	private void hideCampButtons() {
		upgradePickaxeButton.gameObject.SetActive (false);
		upgradeLampButton.gameObject.SetActive (false);
		upgradeBagButton.gameObject.SetActive (false);
		spelunkButton.gameObject.SetActive (false);
	}

	private void updateLightMeter(float lightLevel) {
		
		if (lightBulb == null) {
			lightBulb = GameObject.Find ("lightBulb").transform;
		}

		if (shadowPlane == null) {
			GameObject shadowPlaneGO = GameObject.Find ("shadowPlane");
			if (shadowPlaneGO == null) {
				return;
			} 
			else {
				shadowPlane = shadowPlaneGO.GetComponent<Renderer> ().material;
			}
		}

		Vector3 lightPosition = Camera.main.WorldToScreenPoint (lightBulb.position);
		lightPosition = Camera.main.ScreenToWorldPoint(new Vector3(lightPosition.x, lightPosition.y, 4.5f));
		shadowPlane.SetVector ("_LightPosition", lightPosition);
		shadowPlane.SetFloat ("_Power", lightLevel);
	}

	public void showLightMeter() {
		lightBar.show ();
		lightIcon.gameObject.SetActive (true);
	}

	public void hideLightMeter() {
		lightBar.hide ();
		lightIcon.gameObject.SetActive (false);
	}

	private void updateOres(Dictionary<Ore, int> ores, int size) {
		foreach (Ore O in System.Enum.GetValues(typeof(Ore))) {
			int number = ores [O];
			if (number > 0) {
				this.ores [O].text.text = number.ToString () + "/" + size;
				this.ores [O].image.gameObject.SetActive (true);
				this.ores [O].text.gameObject.SetActive (true);
			} 

			else {
				this.ores [O].image.gameObject.SetActive (false);
				this.ores [O].text.gameObject.SetActive (false);
			}
		}

	}

	private void showOres() {
		foreach (Ore O in System.Enum.GetValues(typeof(Ore))) {
			this.ores [O].image.gameObject.SetActive (true);
			this.ores [O].text.gameObject.SetActive (true);
		}
	}

	private void hideOres() {
		foreach (Ore O in System.Enum.GetValues(typeof(Ore))) {
			this.ores [O].image.gameObject.SetActive (false);
			this.ores [O].text.gameObject.SetActive (false);
		}
	}

	public void updateButtons(Minigame.Response[] keyStates) {

		for (int i = 0; i < keyStates.Length; i++) {
			switch (keyStates [i]) {
			case Minigame.Response.wait:
				break;
			case Minigame.Response.correct:
				prompts[i].sprite = pressedArrowPrompts[prompts[i].sprite];
				break;
			case Minigame.Response.incorrect:
				prompts [i].sprite = wrongPrompt;
				break;
			}
		}
	}

	public void showPrompts(PickaxeAction[] actions) {
			
		int n = Mathf.Clamp (actions.Length, 0, 12);
		for (int i = 0; i < n; i++) {
			Image prompt = prompts [i];
			prompt.gameObject.SetActive (true);
			Sprite sprite;
			arrowPrompts.TryGetValue (actions[i], out sprite);
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

	public void hidePrompts (){
		foreach (Image I in prompts) {
			I.gameObject.SetActive (false);
		}
	}
}
