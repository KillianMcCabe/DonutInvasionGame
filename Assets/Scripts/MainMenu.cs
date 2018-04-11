using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Button playButton;
	public Button exitButton;

	// Use this for initialization
	void Start () {
		playButton.onClick.AddListener(Play);
		exitButton.onClick.AddListener(Exit);
	}
	
	void Play() {
		SceneManager.LoadScene("Game");
	}

	void Exit() {
		Application.Quit();
	}
}
