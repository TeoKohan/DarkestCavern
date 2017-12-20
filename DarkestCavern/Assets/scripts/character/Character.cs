using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(SpriteRenderer))]
public class Character : MonoBehaviour {



	protected GameManager gameManager;

	public InputManager inputManager { get; protected set; }
	public Animator animator { get; protected set; }
	public SpriteRenderer spriteRenderer{ get; protected set; }

	public float movespeed { get; protected set; }
    public float range { get; protected set; }
	public float jumpForce { get; protected set; }
	public float diveForce { get; protected set; }
	public float airMovementMultiplier { get; protected set; }
	public float crouchMovementMultiplier { get; protected set; }

	public HP hp { get; protected set; }
	public Inventory inventory { get; protected set; }

	protected enum Facing {left, right}
	protected Facing facing;

	protected State state;

	protected void Awake() {
		initialize ();
	}

	protected void initialize() {
		gameManager = GameManager.instance;

		this.inputManager = new KeyInputManager ("character");
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();

		this.hp = new HP ();
		this.inventory = new Inventory ();
		facing = Facing.right;	
		state = new Idle (this);
		state.enter ();

		movespeed = 5f;
        range = 1f;
		jumpForce = 7.5f;
		diveForce = jumpForce;
		airMovementMultiplier = 0.75f;
		crouchMovementMultiplier = 0.25f;

		gameManager.addCharacter (this);
	}

	public void update () {

		State newState = state.update ();
		if (newState != null) {
			state.exit ();
			state = newState;
			state.enter ();
		}
	}

    public void ground () {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

	public void move(float xMovement, float yMovement) {

		Facing facing;
		if (xMovement != 0) {
			if (xMovement > 0) {
				facing = Facing.right;
			} 
			else {
				facing = Facing.left;
			}
				
			if (this.facing != facing) {
				switchFacing ();
			}
		}

		xMovement *= movespeed * Time.deltaTime;	
		yMovement *= Time.deltaTime;

        float xPos = gameManager.collisionManager.checkWalls(transform.position.x, transform.position.x + xMovement);

        transform.position = new Vector3(xPos, transform.position.y + yMovement, transform.position.z);
	}

	protected void switchFacing() {

		facing = facing == Facing.right ? Facing.left : Facing.right;
		transform.localScale = Vector3.Scale(transform.localScale, new Vector3 (-1f, 1f, 1f));
	}
}
