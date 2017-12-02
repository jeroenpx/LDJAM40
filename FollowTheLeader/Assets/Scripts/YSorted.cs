using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSorted : MonoBehaviour {
	void Update() {
		Vector3 pos = transform.position;
		transform.position = new Vector3 (pos.x, pos.y, (pos.y/100f)+0.5f);
	}
}
