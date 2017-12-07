using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Animation States", menuName = "Sprite/Animation States", order = 0)]
public class AnimationStates : ScriptableObject {
	public Dictionary<string, SpriteSequence> states;
}