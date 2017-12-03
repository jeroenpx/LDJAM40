using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {

	private Transform[] players;
	private Vector3 currentVelocity;

	private float totalDeadTime = 0f;

	// Update is called once per frame
	void Update () {
		if (players == null) {
			players = new Transform[4];
			Commander[] commanders = GameObject.FindObjectsOfType<Commander> ();
			for (int i = 0; i < commanders.Length; i++) {
				players [i] = commanders [i].transform;
			}
		}
		float xmin = float.PositiveInfinity;
		float xmax = float.NegativeInfinity;
		float ymin = float.PositiveInfinity;
		float ymax = float.NegativeInfinity;
		for (int i = 0; i < players.Length; i++) {
			if (players [i] != null) {
				float x= players [i].position.x;
				float y = players [i].position.y;
				xmin = Mathf.Min (x, xmin);
				xmax = Mathf.Max (x, xmax);
				ymin = Mathf.Min (y, ymin);
				ymax = Mathf.Max (y, ymax);
			}
		}
		if (!float.IsInfinity (xmin)) {
			Vector3 target = new Vector3 (xmin + (xmax - xmin) / 2, ymin + (ymax - ymin) / 2, transform.position.z);
			transform.position = Vector3.SmoothDamp (transform.position, target, ref currentVelocity, 0.5f);
		} else {
			// Everyone died
			Time.timeScale = 0f;
			totalDeadTime += Time.unscaledDeltaTime;
			if (totalDeadTime > 5) {
				SceneManager.LoadScene(0);
			}
		}
	}
}
