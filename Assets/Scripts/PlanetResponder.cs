using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlanetResponder : MonoBehaviour {

	private List<StarAttractor> starsOfInfluence = new List<StarAttractor>();
//	private Transform myTransform;

	void Start () {
//		myTransform = transform;
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
		
		for (int i = 0; i < starsOfInfluence.Count; i++) {
			starsOfInfluence[i].Attract(gameObject);
		}
		
	}
}
