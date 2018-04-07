using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutImpactGlob : MonoBehaviour {

	public GameObject impactPrefab;

	private GameObject impactGoo;

	// Use this for initialization
	void Start () {
		impactGoo = Instantiate(impactPrefab);
		impactGoo.transform.LookAt(transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
