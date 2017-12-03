using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private UnityEngine.UI.Text text;

	private List<int> score;

	public void Awake () {
		text = GetComponent<UnityEngine.UI.Text> ();
		score = new List<int> ();
		score.Add (0);
		score.Add (0);
		score.Add (0);
		score.Add (0);
	}

	public void UpdateScore(int i, int hisscore) {
		this.score[i] = hisscore;
		string text = "Party Size: ";
		string scoresmulti = "";
		int total = 0;
		for (int j = 0; j < score.Count; j++) {
			if (score[j] > 0) {
				if (scoresmulti.Length > 0) {
					scoresmulti += " + ";
				}
				total += score [j] - 1;
				scoresmulti += (score [j] - 1);
			}
		}
		if (scoresmulti.Contains ("+")) {
			scoresmulti += " = " + total;
		}
		text += scoresmulti;
		this.text.text = text;
	}


}
