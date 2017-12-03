using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSplat : MonoBehaviour {

	public Transform splat;
	public int count = 10;

	// When dying...
	void FinalizeDeath () {
		for (int i = 0; i < count; i++) {
			Instantiate (splat, transform.position, Quaternion.identity);
		}
	}
}
