using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public bool sound { get; private set;}
	public bool music { get; private set;}

	public void muteUnmute() {
		sound = !sound;
		music = !music;
		//MUTE UNMUTE CODE
	}
}
