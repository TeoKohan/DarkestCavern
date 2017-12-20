using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    protected static List<Node> nodeList = new List<Node>();

    public float range;

    public Bar hpBar;
    public int maxhp;
    public int armor;

    public int hp { get; protected set; }
    public bool active { get; protected set; }

    protected Material material;

    protected bool dead {

        get {
            return hp <= 0;
        }
        
        set {
        }
    }

    public static Node getNode(float xPosition, float range) {

        foreach (Node N in Node.nodeList) {
            if (N.active) {
                float distance = Mathf.Abs(xPosition - N.getPosition());
                if (distance <= range + N.range) {
                    return N;
                }
            }
        }
        return null;
    }

    public static void resetAll() {
        foreach (Node N in nodeList) {
            N.reset();
        }
    }

    void Start() {
        initialize();
    }

    protected virtual void initialize () {
        nodeList.Add(this);
        material = GetComponent<Renderer>().material;
        reset();
    }

    public virtual void reset() {
        hp = maxhp;
        hpBar.updatePercentage(1f);
        material.SetFloat("_Desaturation", 0);
        activate();
    }

    public void activate() {
        active = true;
    }

    public void deactivate() {
        active = false;
    }

    public virtual void damage(int damage, Character character) {

        Debug.Log("base damage");

        if (active) {
            hp -= damage - armor;
            hp = Mathf.Clamp(hp, 0, maxhp);
            hpBar.updatePercentage((float)hp / (float)maxhp);

            if (dead) die(character);
        }
    }

    protected virtual void die (Character character) {

        return;
    }

    public virtual float getPosition() {

        return transform.position.x;
    }
}
