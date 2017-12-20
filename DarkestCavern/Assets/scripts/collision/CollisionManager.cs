using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager {

    public float checkWalls (float from, float to) {

        foreach (WallNode W in WallNode.wallList) {
            if (W.checkCollision(from, to)) {
                Debug.Log("collision");
                return W.getBound(from);
            }
        }

        return to;
    }
}
