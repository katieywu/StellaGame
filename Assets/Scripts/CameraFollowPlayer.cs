using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

	public GameObject target; //this is the player
	public float distanceY;
	public float distanceZ;
	public float rotationX;
	private Transform targetTransform;

	// Use this for initialization
	void Start () {
		targetTransform = target.transform;
	}

	// Update is called once per frame
	void Update () {
		targetTransform.Rotate(rotationX, 0.0F, 0.0F, Space.Self);
		Vector3 targetPos = targetTransform.position; //player position vector
		Vector3 position = this.gameObject.transform.position; //the camera position

		position.z = targetPos.z - distanceZ;
		position.y = targetPos.y - distanceY;
		position.x = targetPos.x;

		this.gameObject.transform.position = position;
	}
}
