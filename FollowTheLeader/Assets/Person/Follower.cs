using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

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
	}

	/**
	 * Go to the closes guy
	 */
	void GoToClosesGuy() {
		if (closestOtherGuy != null) {
			Vector2 difference = (closestOtherGuy.transform.position - transform.position);
			float magnitude = difference.magnitude;
			Vector2 dir = difference.normalized;
			if (magnitude > keepDistance) {
				SendMessage ("Move", dir);
			}
			if(myLengthFromCommander==1 && magnitude < keepDistanceCommander){
				SendMessage ("Move", -dir);
			}
		}
	}

	/**
	 * Jump if the other guy jumps
	 */
	void MirrorJump() {
		if (closestOtherGuyController) {
			if (closestOtherGuyController.IsJumping ()) {
				SendMessage ("Jump");
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
		}
	}
}
