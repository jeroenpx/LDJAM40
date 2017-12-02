using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

	[SerializeField]
	private float followRadius;

	[SerializeField]
	private LayerMask findCommander;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D[] commanders = Physics2D.OverlapCircleAll (transform.position, followRadius, findCommander);
		if (commanders.Length == 0) {
			return;
		}

		Vector3 myPos = transform.position;
		Collider2D closestCommander = null;
		float closestCommanderDist = float.MaxValue;
		foreach(Collider2D commander in commanders) {
			Vector3 commanderpos = commander.transform.position;
			float distance = Vector3.Distance (myPos, commanderpos);
			if (distance < closestCommanderDist) {
				closestCommander = commander;
				closestCommanderDist = distance;
			}
		}

		// Go to closest commander
		Vector2 dir = (closestCommander.transform.position-myPos).normalized;
		SendMessage("Move", dir);
	}
}
