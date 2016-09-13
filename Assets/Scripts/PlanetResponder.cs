using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlanetResponder : MonoBehaviour {

	private HashSet<StarAttractor> starsOfInfluence = new HashSet<StarAttractor>();
//	private List<StarAttractor> starsOfInfluence = new List<StarAttractor>();

	void Start () {
		GetComponent<Rigidbody>().useGravity = false;

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
	void Update () {

		foreach (StarAttractor star in starsOfInfluence) {
			star.Attract(gameObject);
		}


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
		centerOfMass += GetComponent<Rigidbody>().worldCenterOfMass * GetComponent<Rigidbody>().mass;
		c += GetComponent<Rigidbody>().mass;

		centerOfMass /= c;
		return centerOfMass;
	}

	public void UpdateCenterOfMass(StarAttractor star) {
		starsOfInfluence.Remove(star);
	}
}
