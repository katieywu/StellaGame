using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	
	public float speed;
	
	private Rigidbody rb;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		if (Input.anyKey) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.velocity = (movement * speed);
			rb.angularVelocity = Vector3.zero;
//			rb.AddForce (movement * speed);
		} else {
			rb.velocity = rb.velocity * 0.8F;
			rb.angularVelocity = rb.angularVelocity * 0.8F;
		}

	}
}