using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line {

	public Vector2 from;
	public Vector2 to;
	public float minX;
	public float minY;
	public float maxX;
	public float maxY;
	public float slope;
	public float origin;

	public Line (Vector2 from, Vector2 to) {

		this.from = from;
		this.to = to;

		if (from.x < to.x) {

			minX = from.x;
			maxX = to.x;
		}
		else {
			minX = to.x;
			maxX = from.x;
		}
		if (from.y < to.y) {

			minX = from.y;
			maxX = to.y;
		}
		else {
			minX = to.y;
			maxX = from.y;
		}

		slope = (to.y - from.y) / (to.x - from.x);
		origin = from.y - from.x * slope;
	}

	public float getY(float x) {

		return slope * x + origin;
	}

	public static Vector2 getCollision(Line a, Line b) {
		
		if (a.maxX < b.minX && a.minX > b.maxX) {
			if (a.maxY < b.minY && a.minY > b.maxY) {
				return Vector2.zero;
			}
		}

		if (a.slope != b.slope) {

			//Debug.Log("A slope: " + a.slope + " B slope: " + b.slope);
			float x = -(b.origin - a.origin) / (b.slope - a.slope);
			//Debug.Log("X is: " + x + " Y is: " + a.getY(x));
			return new Vector2(x, a.getY(x));
		}

		return Vector2.zero;
	}
}
