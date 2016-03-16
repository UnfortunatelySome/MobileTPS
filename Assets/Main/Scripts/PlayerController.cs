using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	public float up;
	public float right;
	public bool grounded;
	public bool canFly;
	public bool canJump;
	public float moveSpeed;
	public float verticalSpeed;
	public float upDownThreshold;
	public Transform feet;
	void Update () {
		up = CrossPlatformInputManager.GetAxis ("Vertical");
		right = CrossPlatformInputManager.GetAxis ("Horizontal");
		transform.Translate (Vector3.right * moveSpeed * right);
		isGrounded ();
		if (up >= upDownThreshold && grounded == true&&canJump) {
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
	void isGrounded(){
		RaycastHit2D hit = Physics2D.Raycast (feet.position, Vector2.down, 0.2f);
		if (hit.collider.gameObject.tag == "Platform") {
			grounded = true;
		} else {
			grounded = false;
		}
	}
}
