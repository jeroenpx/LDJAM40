using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

	private float MAXSINGLEFRAMEDIST = 0.2f;

	[SerializeField]
	private float followRadius;

	[SerializeField]
	private LayerMask findOthers;
	[SerializeField]
	private LayerMask findCommander;

	[SerializeField]
	private int updateProbablyTimesPerSecond = 1;

	[SerializeField]
	private int updateJumpTimesPerSecond = 6;

	[SerializeField]
	private int maxlength = 6;

	[SerializeField]
	private float keepDistance = 0.5f;

	[SerializeField]
	private float keepDistanceCommander = 1f;

	public int myLengthFromCommander = 100;

	// What I am doing
	Collider2D closestOtherGuy;
	PersonController closestOtherGuyController;
	float jumpDelay = 0;

	// Myself
	public PersonController myController;

	void Awake() {
		myController = GetComponent<PersonController> ();
		closestOtherGuy = null;
		closestOtherGuyController = null;
		jumpDelay = 0;
	}

	/**
	 * MESSAGE: Closest guy died!
	 */
	public void ClosestGuyDied() {
		closestOtherGuy = null;
		closestOtherGuyController = null;
	}

	public void FinalizeDeath() {
		if (closestOtherGuy) {
			closestOtherGuyController.areFollowingMe.Remove (this);
		}
		GameObject.FindWithTag ("ScoreMgr").GetComponent<ScoreManager> ().IncreaseDeath ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.value < updateProbablyTimesPerSecond*Time.deltaTime) {
			UpdateWhoToFollow ();
		}

		if (Random.value < updateJumpTimesPerSecond * Time.deltaTime) {
			MirrorJump ();
		}

		// Go to closest other guy
		GoToClosesGuy();

		// Show who we are following
		if (closestOtherGuy != null) {
			Vector3 zoffset = new Vector3 (0, 0, -1f);
			Debug.DrawLine (transform.position + zoffset, closestOtherGuy.transform.position + zoffset);
		}

		// Clone self if we are following someone
		if (closestOtherGuy != null) {
			if (Input.GetKeyDown (KeyCode.F2)) {
				GameObject newGo = Instantiate (gameObject);
				newGo.transform.position = newGo.transform.position+ new Vector3 (0.13f, 0f, 0f);
				transform.position = transform.position + new Vector3 (-0.13f, 0f, 0f);
			}
		}
	}

	/**
	 * Go to the closes guy
	 */
	void GoToClosesGuy() {
		if (closestOtherGuy != null) {
			Vector2 difference = (closestOtherGuy.transform.position - transform.position);
			float magnitude = difference.magnitude;
			Vector2 dir = difference.normalized;

			if (myLengthFromCommander == 1 && magnitude < keepDistanceCommander) {
				SendMessage ("Move", -dir);
			} else if (myLengthFromCommander == 1 && magnitude < keepDistanceCommander+MAXSINGLEFRAMEDIST) {
				// Do nothing to avoid twitching
			} else if (magnitude > keepDistance) {
					SendMessage ("Move", dir);
				}
		}
	}

	/**
	 * Jump if the other guy jumps
	 */
	void MirrorJump() {
		if (closestOtherGuyController) {
			if (closestOtherGuyController.IsJumping () && !myController.IsJumping()) {
				jumpDelay += Time.deltaTime;
				float distance = Vector3.Distance (closestOtherGuy.transform.position, transform.position);
				float delay = distance / myController.speed;
				if (jumpDelay > delay) {
					SendMessage ("Jump");
					jumpDelay = 0;
				}
			}
		}
	}

	/**
	 * Find the closest person to follow!
	 */
	void UpdateWhoToFollow() {
		Collider2D[] others = Physics2D.OverlapCircleAll (transform.position, followRadius, findOthers);
		if (others.Length == 0) {
			return;
		}

		Vector3 myPos = transform.position;
		if (closestOtherGuy) {
			closestOtherGuyController.areFollowingMe.Remove (this);
		}
		closestOtherGuy = null;
		closestOtherGuyController = null;
		int lengthToCommander = maxlength;
		float closestCommanderDist = float.MaxValue;
		foreach(Collider2D other in others) {
			Vector3 commanderpos = other.transform.position;
			float distance = Vector3.Distance (myPos, commanderpos);

			int hisLengthToCommander;
			Follower follow = other.GetComponent<Follower> ();
			if (follow == this) {
				// Skip myself!
				continue;
			} else if (follow!=null) {
				hisLengthToCommander = follow.myLengthFromCommander;
			} else {
				hisLengthToCommander = 0;
			}
			if (hisLengthToCommander + 1 < lengthToCommander) {
				// We found one close to commander!
				lengthToCommander = hisLengthToCommander + 1;
				closestOtherGuy = other;
				closestCommanderDist = distance;
			} else if (hisLengthToCommander + 1 == lengthToCommander) {
				// Same length to commander
				if (distance < closestCommanderDist) {
					// But shorter distance
					lengthToCommander = hisLengthToCommander + 1;
					closestOtherGuy = other;
					closestCommanderDist = distance;
				}
			}
		}
		myLengthFromCommander = lengthToCommander;
		if (myLengthFromCommander > maxlength) {
			myLengthFromCommander = maxlength;
		}
		if (closestOtherGuy) {
			closestOtherGuyController = closestOtherGuy.GetComponent<PersonController> ();
			closestOtherGuyController.areFollowingMe.Add (this);
		}
	}
}
