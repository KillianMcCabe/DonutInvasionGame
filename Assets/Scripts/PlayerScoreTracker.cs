using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreTracker : MonoBehaviour {
	public static PlayerScoreTracker instance;

	public Text scoreText;
	public Text streakText;
	public GameObject scoreChangePrefab;
	public GameObject scoreMultiplierPrefab;

	const int baseDonutScore = 50;
	const int scoreMultiplier = 50;
	const float donutStreakTimeReq = 4;

	int score;
	int donutStreak;

	float timeSinceLastDonut = 0;
	
	public Color[] streakColors;

	// Use this for initialization
	void Start () {
		if (instance != null) {
			Destroy(this);
		} else {
			instance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (timeSinceLastDonut > donutStreakTimeReq) {
			donutStreak = 0;
			streakText.text = "";
		}
		timeSinceLastDonut += Time.deltaTime;
	}

	public void ScoreDonut() {
		donutStreak ++;
		int scoreChange = baseDonutScore + (donutStreak * scoreMultiplier);
		score += scoreChange;
		scoreText.text = "Score: " + score;
		streakText.text = "Streak: " + donutStreak;
		timeSinceLastDonut = 0;

		GameObject go = Instantiate(scoreChangePrefab, transform.parent, false);
		go.GetComponentInChildren<Text>().text = "+" + scoreChange;
		// go.transform.position += new Vector3(100, -100, 0);

		if (donutStreak >= 2) {
			GameObject go2 = Instantiate(scoreMultiplierPrefab, GameController.instance.ship.transform, false);
			TextMesh tm = go2.GetComponentInChildren<TextMesh>();
			tm.text = "X" + donutStreak;
			tm.fontSize = 40 + donutStreak * 4;

			// change color
			int i = Mathf.Min(donutStreak-2, streakColors.Length-1);
			tm.color = streakColors[i];
			tm.fontSize = 40 + i * 5;
		}
	}
}
