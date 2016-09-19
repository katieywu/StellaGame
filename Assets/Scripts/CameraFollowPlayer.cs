using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraFollowPlayer : MonoBehaviour {

	public GameObject target; //this is the player
	public float distanceY;
	public float distanceZ;
	public float rotationX;
	private Transform targetTransform;


	void OnGUI() {
		//makes a GUI button at coordinates 10, 100, and a size of 200x40
		if(GUI.Button(new Rect (10,10,200,40),"Restart")) {
			//Loads a level
			SceneManager.LoadScene("Level1FrontView");
		}
	}

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
