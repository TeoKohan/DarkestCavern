using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UIManager : MonoBehaviour {

    private Canvas canvas;
    private Transform promptT;

    private List<GameObject> prompts;

    public void initialize () {

        canvas = GetComponent<Canvas>();
        promptT = new GameObject("prompts", typeof(RectTransform)).transform;
        promptT.SetParent(canvas.transform, false);
        prompts = new List<GameObject>();
    }

    public void showPrompt (PromptKey PK) {

        GameObject prompt = new GameObject();
        prompt.transform.parent = promptT;
        Image promptImage = prompt.AddComponent <Image>();
        promptImage.sprite = Resources.Load("spacebar") as Sprite;

        if (Resources.Load("spacebar") != null) {
            Debug.Log("loaded");
        }
    }
}
