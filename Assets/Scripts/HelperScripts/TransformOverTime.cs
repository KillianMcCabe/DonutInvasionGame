using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOverTime : MonoBehaviour {

	public Vector3 scale = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += scale * Time.deltaTime;
	}

}
