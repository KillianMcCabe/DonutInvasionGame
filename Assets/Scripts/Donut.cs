using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour {

	public GameObject donutFracturedPrefab;
	public GameObject donutGlobPrefab;

	Rigidbody rigidbody;
	AudioSource audioSource;

	bool isDestroyed = false;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 towardsPlanet = Vector3.Normalize(GameController.instance.planet.transform.position - transform.position);
		rigidbody.AddForce(towardsPlanet, ForceMode.Force);
	}

	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			Instantiate(donutFracturedPrefab, transform.position, transform.rotation);
			Destroy(gameObject);
			PlayerScoreTracker.instance.ScoreDonut();
		}
	}

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Planet" && !isDestroyed) {
			isDestroyed = true;

			ContactPoint contact = other.contacts[0];
			Instantiate(donutGlobPrefab, contact.point, Random.rotation);

			Destroy(gameObject);
			GameController.instance.DonutImpact();
		}
	}
}
