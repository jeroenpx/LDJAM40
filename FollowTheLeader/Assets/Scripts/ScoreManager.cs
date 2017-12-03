using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public UnityEngine.UI.Text uiScore;
	public UnityEngine.UI.Text uiMax;
	public UnityEngine.UI.Text uiRip;
	public string textScore = "Party Size: ";
	public string textMax = "HIGH SCORE: ";
	public string textRip = "R.I.P.: ";
	int maxScore = 0;
	int deathCount = 0;

	private List<int> score;

	public void Awake () {
		score = new List<int> ();
		score.Add (0);
		score.Add (0);
		score.Add (0);
		score.Add (0);
		uiScore.text = textScore + 0;
		uiMax.text = textMax + 0;
		uiRip.text = textRip + 0;
	}

	public void IncreaseDeath() {
		deathCount++;
		uiRip.text = textRip + deathCount;
	}

	public void UpdateScore(int i, int hisscore) {
		this.score[i] = hisscore;
		string text = textScore;
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
		uiScore.text = text;
		if (total > maxScore) {
			maxScore = total;
			uiMax.text = textMax + maxScore;
		}
	}


}
