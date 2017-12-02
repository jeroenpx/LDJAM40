using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour {

	// Settings
	[SerializeField]
	private float speed = 1f;

	[SerializeField]
	private float inairtime;

	[SerializeField]
	private float gravity = 10f;

	// What to do next frame?
	private Vector2 moveDir;

	// Private components
	private Rigidbody2D rigidBody;
	private Transform child;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D> ();
		child = transform.Find ("Graphics");
	}

	/**
	 * MESSAGE: Move in a certain direction
	 */
	public void Move(Vector2 dir) {
		moveDir = dir;
	}

	/**
	 * MESSAGE: Jump!
	 */
	public void Jump() {
		// TODO
	}

	void FixedUpdate() {
		// Currently just set the velocity
		rigidBody.velocity = moveDir*speed;

		// Reset move dir
		moveDir = new Vector2 ();
	}


}
