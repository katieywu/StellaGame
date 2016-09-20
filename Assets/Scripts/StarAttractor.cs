using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarAttractor : MonoBehaviour {

	public float gravity = -10;
	private HashSet<PlanetResponder> influencedPlanets = new HashSet<PlanetResponder>();

	public void Start() {
		GetComponent<Rigidbody>().useGravity = false;
	}

	public void Attract(GameObject planet) {
		influencedPlanets.Add(planet.GetComponent<PlanetResponder>());
		Vector3 gravityDir = (planet.transform.position - transform.position).normalized;

//		float distance = (planet.transform.position - transform.position).magnitude;
//
//		float mass1 = Mathf.Pow(planet.transform.localScale.z, 1f);
//		float mass2 = Mathf.Pow(this.transform.localScale.z, 1f);
//
//		float calcGrav = (mass1 * mass2)/(distance * distance);
//		planet.GetComponent<Rigidbody>().AddForce(gravityDir * calcGrav * gravity);
		planet.GetComponent<Rigidbody>().AddForce(gravityDir * gravity);
	}

	void OnMouseOver() {
		if (Input.GetMouseButton(1)) {
			gravity *= 1.1f;
			transform.localScale += new Vector3(0.05F, 0.05F, 0.05F);
		} else if (Input.GetMouseButton(0)) {
			gameObject.SetActive(false);
		}

		PlanetResponder[] allPlanets = FindObjectsOfType(typeof(PlanetResponder)) as PlanetResponder[];
		for (int i = 0; i < allPlanets.Length; i++) {
			allPlanets[i].calculateNearbyStars();
		}
//		foreach(PlanetResponder p in influencedPlanets) {
//			p.UpdateCenterOfMass(this);
//		}
	}

	public void RemoveAttractedPlanet(PlanetResponder planet) {
		influencedPlanets.Remove(planet);
	}
}
