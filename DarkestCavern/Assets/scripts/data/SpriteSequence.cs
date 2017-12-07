using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Sprite Sequence", menuName = "Sprite/Sequence", order = 1)]
public class SpriteSequence : ScriptableObject {
	public Sprite[] sprites;
}