using UnityEngine;
using System.Collections;

public class StarAttractor : MonoBehaviour {

	public float gravity = -10;

	public void Start() {
		GetComponent<Rigidbody>().useGravity = false;
	}
	public void Attract(GameObject planet) {
		Vector3 gravityUp = (planet.transform.position - transform.position).normalized;
		planet.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
		Debug.Log("vec " + (gravityUp));
	}
}
