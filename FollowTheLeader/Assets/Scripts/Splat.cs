using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour {

	private Rigidbody2D splatRigid;

	private float startTime;
	public float inairtime = 0.4f;
	public float splatHeight = 3f;
	public float spreadSize = 3f;
	private Transform child;
	private Vector3 spread;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		child = transform.Find ("Graphics");
		spread = Random.insideUnitCircle*spreadSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (startTime > Time.time - inairtime) {
			// we are in the air!
			float t = (Time.time - startTime) / inairtime;
			float tsquared = t * t;
			child.localPosition = spread * t + new Vector3 (0f, -4 * splatHeight * tsquared + 4 * splatHeight * t);
		} else {
			StopMove ();
		}
	}

	void StopMove() {
		Rigidbody2D r = GetComponent<Rigidbody2D> ();
		Collider2D c = GetComponent<Collider2D> ();
		GameObject.Destroy (r);
		GameObject.Destroy (c);
		startTime = -1000;
		if (startTime > Time.time - inairtime) {
			float t = (Time.time - startTime) / inairtime;
			child.localPosition = spread;
		}
		// Remove script
		Destroy (this);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		transform.parent = coll.gameObject.transform;
		StopMove();
	}
}
