﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierTracker : MonoBehaviour {

	float t = 0;
	public float s;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t > s) {
			Destroy(gameObject);
		}
	}

	public void Score() {
		t = 0;
	}
}
