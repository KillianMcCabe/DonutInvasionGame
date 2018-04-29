using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	const float speed = 360;

	float lifetime = 0;
	Rigidbody rigidbody;

	public GameObject impactPrefab;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = transform.forward * speed;
	}
	
	void Update() {
		lifetime += Time.deltaTime;
		if (lifetime > 5) {
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		// maybe spherecast between positions?
	}

	void OnCollisionEnter(Collision other)
	{
		if (lifetime <= .1) {
			return;
		}
		// if (other.name == "PlanetGlob") { // TODO: this
			
		// }
		print("hit " + other.gameObject.name);

		Instantiate(impactPrefab, other.contacts[0].point, Quaternion.identity);
		Destroy(gameObject);
	}
}
