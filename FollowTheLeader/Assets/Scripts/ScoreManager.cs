using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private UnityEngine.UI.Text text;

	private int score;

	public void Awake () {
		text = GetComponent<UnityEngine.UI.Text> ();
	}

	public void UpdateScore(int score) {
		this.score = score;
		text.text = "Party Size: " + score;
	}


}
