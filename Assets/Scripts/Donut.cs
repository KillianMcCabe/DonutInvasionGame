using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour {

	public GameObject fracturedPrefab;
	public GameObject donutImpactPrefab;

	Rigidbody rigidbody;

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
			Instantiate(fracturedPrefab, transform.position, transform.rotation);
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
		if (other.gameObject.tag == "Planet") {
			Instantiate(fracturedPrefab, transform.position, transform.rotation);
			GameObject go = Instantiate(donutImpactPrefab);
			go.transform.LookAt(transform);
			Destroy(gameObject);
			GameController.instance.DonutImpact();
		}
	}
}
