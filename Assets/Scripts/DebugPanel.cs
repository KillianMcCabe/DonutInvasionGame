using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {

	public static DebugPanel instance;

	GameObject panel;
	Text text;

	// Use this for initialization
	void Awake () {
		if (instance != null) {
			Destroy(this);
		} else {
			instance = this;
		}

		panel = transform.Find("Panel").gameObject;
		text = GetComponentInChildren<Text>(true);
		panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DisplayText(string s) {
		panel.SetActive(true);
		text.text = s;
	}
}
