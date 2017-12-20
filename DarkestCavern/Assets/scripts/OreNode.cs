using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreNode : Node {

	public GameObject ores;
	public Transform hotspot;
	public Ore ore;
	public int oreQuantity;

	public override void reset() {
        base.reset();
		ores.SetActive (true);
    }

    public override void damage(int damage, Character character) {

        if (active) {
            hp -= damage - armor;
            hp = Mathf.Clamp(hp, 0, maxhp);
            hpBar.updatePercentage((float)hp / maxhp);

            Debug.Log(hp);
            if (dead) {
                die(character);
            }
        }
    }

    protected override void die(Character character) {
		character.inventory.bag.addOre (ore, oreQuantity);
		active = false;
		ores.SetActive (false);
		StartCoroutine (fade (3));
	}

    public override float getPosition () {

        return hotspot.position.x;
    }

	IEnumerator fade(int seconds) {
		
		float startTime = Time.time;
		while (Time.time - startTime < seconds) {
			material.SetFloat ("_Desaturation", Mathf.Clamp01((Time.time - startTime) / seconds));
			yield return null;
		}
		material.SetFloat ("_Desaturation", 1f);
	}
}
