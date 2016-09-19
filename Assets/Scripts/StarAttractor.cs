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
		Vector3 gravityUp = (planet.transform.position - transform.position).normalized;
		planet.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
	}

	void OnMouseDown() {
		gameObject.SetActive(false);

		foreach(PlanetResponder p in influencedPlanets) {
			p.UpdateCenterOfMass(this);
		}
	}

	public void RemoveAttractedPlanet(PlanetResponder planet) {
		influencedPlanets.Remove(planet);
	}
}
