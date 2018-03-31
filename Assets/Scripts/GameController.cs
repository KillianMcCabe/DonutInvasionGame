using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public GameObject planet;
	public GameObject ship;
	public Slider planetHealthSlider;
	public GameObject gameOverPanel;

	public GameObject donutPrefab;

	const float donutSpawnRadius = 500f;
	const float maxPlanetHealth = 100;
	const float donutImpactDamage = 20;
	
	float planetHealth;

	public struct Difficulty {

		public float nextDifficultyUnlockTime;
		public float donutSpawnRate;

		public Difficulty(float _donutSpawnRate, float _nextDifficultyUnlockTime) {
			donutSpawnRate = _donutSpawnRate;
			nextDifficultyUnlockTime = _nextDifficultyUnlockTime;
		}
	}

	// const float donutSpawnRate = 4.5f;
	float timeSinceLastDonut = 0f;

	Difficulty[] difficultys = {
		new Difficulty(5f, 30f),
		new Difficulty(4f, 60f),
		new Difficulty(3f, 90f)
	};
	int currentDifficultyIndex = 0;

	// Use this for initialization
	void Start () {
		if (instance != null) {
			Destroy(this);
		} else {
			instance = this;
		}

		planetHealth = maxPlanetHealth;
		UpdatePlanetHealthSlider();
	}
	
	// Update is called once per frame
	void Update () {
		if (timeSinceLastDonut > difficultys[currentDifficultyIndex].donutSpawnRate) {
			Vector3 position = Random.rotation * Vector3.up * 500f;
			Instantiate(donutPrefab, position, Random.rotation);
			timeSinceLastDonut = 0;
		}

		timeSinceLastDonut += Time.deltaTime;

		if (Time.timeSinceLevelLoad > difficultys[currentDifficultyIndex].nextDifficultyUnlockTime && currentDifficultyIndex < difficultys.Length-1) {
			currentDifficultyIndex += 1;
		}

		// DebugPanel.instance.DisplayText(JsonUtility.ToJson(difficultys[currentDifficultyIndex], true));
	}

	void UpdatePlanetHealthSlider() {
		planetHealthSlider.value = planetHealth/maxPlanetHealth;
		if (planetHealthSlider.value < 0.3) {
			planetHealthSlider.targetGraphic.color = Color.red;
		} else if (planetHealthSlider.value < 0.7) {
			planetHealthSlider.targetGraphic.color = Color.yellow;
		} else {
			planetHealthSlider.targetGraphic.color = Color.green;
		}
		

		if (planetHealth <= 0)
			gameOverPanel.SetActive(true);
	}

	public void DonutImpact() {
		planetHealth -= donutImpactDamage;
		UpdatePlanetHealthSlider();
	}
}
