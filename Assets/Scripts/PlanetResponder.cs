using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlanetResponder : MonoBehaviour {

	private HashSet<StarAttractor> starsOfInfluence = new HashSet<StarAttractor>();
	public GameObject closestObject;

	private Rigidbody rb;
	private float movementSpeed = 10f;

	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;

		calculateNearbyStars();
	}
	
	// FixedUpdate is called once per frame
	void FixedUpdate () {

		//ANIMATE IT TO THE WORMHOLE
		if (rb.isKinematic) {
			Vector3 wormPos = closestObject.transform.position;
			wormPos.z = wormPos.z - closestObject.transform.localScale.z;


			if (Vector3.Distance(wormPos, rb.position) > 0.3) {
				Vector3 direction = (wormPos - transform.position).normalized;
				rb.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);
//				Debug.Log("target pos: "+ wormPos);
//				Debug.Log("planet pos: "+ rb.position);
			}
		} else { //this means the planet is NOT in the trigger, so use physics forces on it
			foreach (StarAttractor star in starsOfInfluence) {
				star.Attract(gameObject);
			}
		}

		//CENTROID BASED GRAVITY POSITION
//		transform.position = calculateCentroid();

	}

	void calculateNearbyStars() {
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

	public void UpdateCenterOfMass(StarAttractor star) {
		starsOfInfluence.Remove(star);
	}

	public void MoveToWormholeTrigger(GameObject closest) {
		rb.isKinematic = true;

		closestObject = closest;
		Debug.Log("closest: " + closestObject.name);

		//remove the planet from each STAR's array
		foreach (StarAttractor star in starsOfInfluence) {
			star.RemoveAttractedPlanet(this);
		}
			
		starsOfInfluence.Clear();
		GetComponent<Collider>().enabled = false;
	}

	//-----------------LESS IMPORTANT/NOT REALLY USED FUNCTIONS BELOW------------------//
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
}
