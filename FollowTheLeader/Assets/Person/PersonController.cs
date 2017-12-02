using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour {

	// Settings
	[SerializeField]
	private float speed = 1f;

	[SerializeField]
	private float inairtime = 1f;

	[SerializeField]
	private float jumpheight = 1f;

	// What to do next frame?
	private Vector2 moveDir;
	private bool jump;
	private float jumpStartTime=-100f;

	// Private components
	private Rigidbody2D rigidBody;
	private Transform child;
	private Vector3 childDelta;
	private bool jumping;

	public float t;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D> ();
		child = transform.Find ("Graphics");
		childDelta = child.localPosition;
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
	[ContextMenu("Jump")]
	public void Jump() {
		jump = true;
	}

	void FixedUpdate() {
		// Currently just set the velocity
		rigidBody.velocity = moveDir*speed;

		if (jump) {
			if (jumping && jumpStartTime < Time.time - inairtime) {
				// end of jump
				jump = false;
				jumping = false;
			} else {
				if (!jumping) {
					// start jumping
					jumping = true;
					jumpStartTime = Time.time;
				}
			}
			if (jumping) {
				// we are in the air!
				t = (Time.time-jumpStartTime)/inairtime;
				float tsquared = t*t;
				child.localPosition = childDelta + new Vector3(0f, -4*jumpheight*tsquared+4*jumpheight*t);
			}
		}

		// Reset move dir
		moveDir = new Vector2 ();
	}


}
