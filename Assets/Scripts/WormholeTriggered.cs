using UnityEngine;
using System.Collections;

public class WormholeTriggered : MonoBehaviour {

	void Start() {
		
	}
	void OnTriggerEnter(Collider other) {
		PlanetResponder planet = other.gameObject.GetComponent<PlanetResponder>();
		if (planet != null) {
			Debug.Log("PLANET HIT WORMHOLE TRIGGER");
			planet.MoveToWormholeTrigger();
		}
	}
}
