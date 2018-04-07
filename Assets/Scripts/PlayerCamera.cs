using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    public Ship ship;
    public GameObject planet;
    
    SphereCollider planetCollidor;

    const float distanceKeptFromTarget = 40;
    const float moveAwayFromPlanetSpeed = 5;

    [HideInInspector]
    public bool lookingAtPlanet = false;

    // Use this for initialization
    void Start () {
        planetCollidor = planet.GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (!lookingAtPlanet) {
            Vector3 towardsTarget = ship.transform.position - transform.position;
            transform.position = ship.transform.position - towardsTarget.normalized * distanceKeptFromTarget; // keep a specific distance from target

            transform.rotation = ship.rotation;

            // prevent camera entering planet
            if (transform.position.magnitude < planetCollidor.bounds.size.x / 2) {
                transform.position = transform.position.normalized * (planetCollidor.bounds.size.x / 2);
            }
        } else {
            // transform.LookAt(planet.transform);
            Vector3 awayFromPlanet = transform.position - planet.transform.position;
            awayFromPlanet.Normalize();

            Quaternion to = Quaternion.LookRotation(-awayFromPlanet, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 10f * Time.deltaTime);
            
            transform.position += awayFromPlanet * moveAwayFromPlanetSpeed * Time.deltaTime;
        }
    }

}
