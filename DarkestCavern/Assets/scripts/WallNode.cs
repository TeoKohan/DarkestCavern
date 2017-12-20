using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNode : Node {

    public float width = 1f;
	public static List<WallNode> wallList = new List<WallNode>();

	protected override void initialize() {

        wallList.Add(this);
        base.initialize();
	}

	public bool checkCollision(float from, float to) {

        if (!active) {
            return false;
        }
            
        float xPos = transform.position.x;
		if ((from <= xPos - width && to >= xPos - width) || (from >= xPos + width && to <= xPos + width))
        {
            return true;
        }

        return false;
	}

    public float getBound (float character) {

        float xPos = transform.position.x;
        if (character < xPos) {
            return xPos - width;
        }

        return xPos + width;
    }
}
