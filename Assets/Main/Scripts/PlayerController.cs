using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	public float up;
	public float right;
	public float moveSpeed;
	public float direction;
	public float moveThreshold;
	public bool moving;
	public GameObject playerBody;
	void Update () {
		float speed;
		up = CrossPlatformInputManager.GetAxis ("Vertical");
		right = CrossPlatformInputManager.GetAxis ("Horizontal");
<<<<<<< HEAD
		direction = Vector2.Angle (new Vector2 (0,1), new Vector2 (right, up));
		if (Mathf.Pow (up, 2) + Mathf.Pow (right, 2)>=moveThreshold) {
			moving = true;
=======
		transform.Translate (Vector3.right * moveSpeed * right);
		isGrounded ();
		if (up >= upDownThreshold && grounded == true &&canJump) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * verticalSpeed);
		}
		/**RaycastHit hit;
		if (Physics.Raycast (gameObject.transform.position, -Vector3.up, out hit, 1.5f)) {
			if (hit.collider.gameObject.tag == "Platform") {
				grounded = true;
			} else {
				grounded = false;
			}
		} else {
			grounded  = false;
		}*/
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag.Equals("Platform")) {
			grounded = true;
>>>>>>> origin/master
		} else {
			moving = false;
		}
		if (right < 0) {
			direction = -direction;
		}
		direction = -direction;
		if (moving) {
			playerBody.transform.localEulerAngles = new Vector3 (playerBody.transform.localEulerAngles.x, playerBody.transform.localEulerAngles.y, direction);
			speed = Mathf.Pow (up, 2) + Mathf.Pow (right, 2);
			transform.Translate (moveSpeed * new Vector2 (right, up));
		}
	}
	void isGrounded(){
		//hit.collider.gameObject.tag.Equals ("Platform")

	//	RaycastHit2D hit = Physics2D.Raycast (feet.position, Vector2.down, 0.2f);
	//	if (GameObject.) {
	//		grounded = true;
	//	} else {
	//		grounded = false;
	//	}
	// }
}
}