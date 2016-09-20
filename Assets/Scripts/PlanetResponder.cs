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
			wormPos.z = wormPos.z - closestObject.transform.localScale.z - 2f;


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

	public void calculateNearbyStars() {
		starsOfInfluence.Clear();
		StarAttractor[] allStars = FindObjectsOfType(typeof(StarAttractor)) as StarAttractor[];
		Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, 10.0f);

		//sort the stars on distance
		List<StarAttractor> l = new List<StarAttractor>();
		for (int i = 0; i < allStars.Length; i++) {
			l.Add(allStars[i]);
		}
		l.Sort(delegate(StarAttractor c1, StarAttractor c2){
			return Vector3.Distance(this.transform.position, c1.transform.position).CompareTo
				((Vector3.Distance(this.transform.position, c2.transform.position)));   
		}); //end of sorting


		//sort the stars on size
		List<StarAttractor> sizeList = new List<StarAttractor>();
		for (int i = 0; i < allStars.Length; i++) {
			sizeList.Add(allStars[i]);
		}
		sizeList.Sort(delegate(StarAttractor c1, StarAttractor c2){
			return (c1.transform.localScale.z).CompareTo
				(c2.transform.localScale.z);   
		});
			
		for (int i = 0; i < Mathf.Round(sizeList.Count * 0.2f); i++) {
			starsOfInfluence.Add(sizeList[i]);
		}

		//-------------------------------//

		//ADD ALL THE STARS
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject.GetComponent<StarAttractor>() != null) {
				starsOfInfluence.Add(colliders[i].gameObject.GetComponent<StarAttractor>());
			} 
		}

		//IF LESS THAN 4 ADD THE 3 CLOSEST ONES
		if (starsOfInfluence.Count < 3) {
			int numToAdd = (l.Count < 4) ? l.Count : 3;
			for (int i = 0; i < numToAdd; i++) {
				starsOfInfluence.Add(l[i]);
			}
		}

	}

//	public void UpdateCenterOfMass(StarAttractor star) {
////		starsOfInfluence.Remove(star);
//		calculateNearbyStars();
//	}

	public void MoveToWormholeTrigger(GameObject closest) {
		rb.isKinematic = true;

		closestObject = closest;
		Debug.Log("closest: " + closestObject.name);

		//remove the planet from each STAR's array
//		foreach (StarAttractor star in starsOfInfluence) {
//			star.RemoveAttractedPlanet(this);
//		}
			
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

	void OnMouseDown() {
		foreach(StarAttractor p in starsOfInfluence) {
			Debug.Log("star: " + p.name);
		}
//		Debug.Log("ang vel: "+ rb.angularVelocity);
		Debug.Log("-------");
	}
}
