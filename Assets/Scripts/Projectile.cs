using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	const float speed = 360;

	float lifetime = 0;
	Rigidbody rigidbody;

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

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (lifetime <= .1) {
			return;
		}
		
		if (other.name == "PlanetGlob") { // TODO: this
			
		}
		print("hit " + other.name);
			Destroy(gameObject);
		
	}
}
