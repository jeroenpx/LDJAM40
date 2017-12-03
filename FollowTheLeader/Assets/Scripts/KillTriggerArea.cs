using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTriggerArea : MonoBehaviour {

	public bool onGroundOnly = true;

	void OnTriggerStay2D(Collider2D player) {
		if (player.gameObject.CompareTag ("Player")) {
			PersonController c = player.GetComponent <PersonController>();
			if (onGroundOnly) {
				c.GotHitGround ();
			} else {
				c.GotHit ();
			}
		}
	}

	void OnTriggerEnter2D() {
	}
}
