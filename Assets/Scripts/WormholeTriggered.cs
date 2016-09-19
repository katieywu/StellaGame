using UnityEngine;
using System.Collections;

public class WormholeTriggered : MonoBehaviour {

	public GameObject wormhole;
	private GameObject closestObject;

	void Start() {
		closestObject = wormhole;
	}

	void OnTriggerEnter(Collider other) {
		PlanetResponder planet = other.gameObject.GetComponent<PlanetResponder>();
		if (planet != null) {
			Debug.Log("PLANET HIT WORMHOLE TRIGGER");
			planet.MoveToWormholeTrigger(closestObject);
			closestObject = other.gameObject;
		}
	}
}
