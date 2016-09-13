using UnityEngine;
using System.Collections;

public class PlanetResponder : MonoBehaviour {

	private ArrayList starsOfInfluence = new ArrayList();

	// Use this for initialization
	void Start () {
		Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, 10.0f);

		int i = 0;
		while (i < colliders.Length) {
			if (colliders[i].gameObject.GetComponent<StarAttractor>() != null) {
//				Debug.Log("stars nearby: "+ colliders[i].name);
				starsOfInfluence.Add(colliders[i].gameObject);
			} else {
//				Debug.Log("other nearby: "+ colliders[i].name);
			}
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
