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
			StartCoroutine(Die());
		}
		lifetime += Time.deltaTime;
	}

	IEnumerator Die() {
		float t = 0;
		float fadeTime = 1;
		Vector3 startingScale = rigidbodies[0].transform.localScale;
		Vector3 newScale = new Vector3(0, 0, 0);
		while (t < fadeTime)
		{
			var s = Vector3.Lerp(startingScale, newScale, (t / fadeTime));
			foreach (Rigidbody rb in rigidbodies) {
				rb.transform.localScale = s;
			}
			t += Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}
}
