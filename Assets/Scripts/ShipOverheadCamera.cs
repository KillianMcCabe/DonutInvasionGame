using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipOverheadCamera : MonoBehaviour {

	public GameObject planet;
	public GameObject target;

	const float distanceFromPlayer = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	void LateUpdate () {

		Vector3 vFromPlanetToTarget = target.transform.position - planet.transform.position;
		transform.position = target.transform.position + (vFromPlanetToTarget.normalized * distanceFromPlayer);
		// transform.position = (vFromPlanetToTarget.normalized * 540f);

		transform.LookAt(planet.transform, target.transform.forward);
	}
}
