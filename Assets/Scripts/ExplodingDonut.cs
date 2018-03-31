using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingDonut : MonoBehaviour {

	public Rigidbody[] rigidbodies;

	float timeTilDeath = 8;
	float lifetime = 0;

	// Use this for initialization
	void Start () {
		foreach (Rigidbody rb in rigidbodies) {
			rb.AddExplosionForce(400f, transform.position, 100f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (lifetime > timeTilDeath) {
			Destroy(gameObject);
		}
		lifetime += Time.deltaTime;
	}
}
