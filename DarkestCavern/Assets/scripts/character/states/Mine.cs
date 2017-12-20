using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : State {

	private Minigame minigame;
    private Node node;

	public Mine(Character character) : base(character) {

	}

	public override void enter () {

        node = Node.getNode(character.transform.position.x, character.range);

        if (node != null) {
            minigame = character.inventory.pickaxe.minigame;
            minigame.start();
            character.animator.SetBool("Mining", true);
        }
	}

	public override State handleInput() {

        if (node != null) {

            bool finish = minigame.update();

            if (finish) {

                if (minigame.win()) {
                    node.damage(character.inventory.pickaxe.damage - node.armor, character);
                }
                return new Idle(character);
            }

            return null;
        }

        return new Idle(character);
	}

	public override State update() {
		return handleInput ();
	}

	public override void exit () {
		character.animator.SetBool ("Mining", false);
		return;
	}
}
