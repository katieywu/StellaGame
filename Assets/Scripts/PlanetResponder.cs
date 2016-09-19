using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlanetResponder : MonoBehaviour {

	private HashSet<StarAttractor> starsOfInfluence = new HashSet<StarAttractor>();
	public GameObject wormhole;

	private Rigidbody rb;
	private float movementSpeed = 5f;

	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;

		Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, 10.0f);

		int i = 0;
		while (i < colliders.Length) {
			if (colliders[i].gameObject.GetComponent<StarAttractor>() != null) {
//				Debug.Log("stars nearby: "+ colliders[i].name);
				starsOfInfluence.Add(colliders[i].gameObject.GetComponent<StarAttractor>());
			} else {
//				Debug.Log("other nearby: "+ colliders[i].name);
			}
			i++;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		foreach (StarAttractor star in starsOfInfluence) {
			star.Attract(gameObject);
		}

		if (rb.isKinematic) {
			//calculate the right position to move to
			Vector3 wormPos = wormhole.transform.position;
			wormPos.z = wormPos.z - wormhole.transform.localScale.z;

//			Debug.Log("target pos: "+ wormPos);

			Vector3 direction = (wormPos - transform.position).normalized;
			rb.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);

//			Debug.Log("planet pos: "+ rb.position);
		}

		//CENTROID BASED GRAVITY POSITION
//		transform.position = calculateCentroid();

	}

	Vector3 calculateCentroid() {
		Vector3 centerOfMass = Vector3.zero;
		float c = 0f;

		foreach (StarAttractor star in starsOfInfluence) {
			Rigidbody rigidbody = star.GetComponent<Rigidbody>();
			centerOfMass += rigidbody.worldCenterOfMass * rigidbody.mass;
			c += rigidbody.mass;
		}

		//add yourself to the centroid calculation
		centerOfMass += rb.worldCenterOfMass * GetComponent<Rigidbody>().mass;
		c += rb.mass;

		centerOfMass /= c;
		return centerOfMass;
	}

	public void UpdateCenterOfMass(StarAttractor star) {
		starsOfInfluence.Remove(star);
	}

	public void MoveToWormholeTrigger() {
		Debug.Log("movetowormhol called");
		rb.isKinematic = true;

		//remove the planet from each STAR's array
		foreach (StarAttractor star in starsOfInfluence) {
			star.RemoveAttractedPlanet(this);
		}
			
		starsOfInfluence.Clear();
		GetComponent<Collider>().enabled = false;
	}
}
