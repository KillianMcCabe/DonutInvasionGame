using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {

	public float rate = 5;
	public GameObject emit;

	float t = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t > rate) {
			Instantiate(emit, transform.position, transform.rotation);
			t = 0;
		}
	}
}
