using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideArrow : MonoBehaviour {

	public GameObject ship;
	public GameObject planet;

	Quaternion rotationOffset = Quaternion.Euler(0, 180, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Testing with up vector
		// transform.rotation = Quaternion.LookRotation(Vector3.up, ship.transform.up) * Quaternion.Euler(0, 180, 0);
		// transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);

		Vector3 towardsTarget = planet.transform.position - ship.transform.position;
		transform.rotation = Quaternion.LookRotation(towardsTarget, ship.transform.up) * rotationOffset;
		transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);
	}
}
