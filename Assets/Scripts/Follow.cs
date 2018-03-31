using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public Ship ship;
    public SphereCollider planetCollidor;

    float distanceKeptFromTarget = 40;
    // float heightAboveTarget = .25f;

    Vector3 towardsTarget;

    // Use this for initialization
    void Start () {
        // towardsTarget = transform.forward;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        towardsTarget = ship.transform.position - transform.position;

        transform.position = ship.transform.position - towardsTarget.normalized * distanceKeptFromTarget; // keep a specific distance from target

        //transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, 0.5f); // lerp into rotation of target
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(towardsTarget), 0.25f); // lerp into looking at target

        Vector3 towardsFutureTargetLocation = (ship.transform.position + (ship.transform.forward * distanceKeptFromTarget)) - transform.position;
        transform.rotation = Quaternion.LookRotation(towardsFutureTargetLocation);// lerp at where target is heading
        transform.rotation = ship.rotation;

        // prevent camera entering planet
        if (transform.position.magnitude < planetCollidor.bounds.size.x / 2) {
            transform.position = transform.position.normalized * (planetCollidor.bounds.size.x / 2);
        }
    }

}
