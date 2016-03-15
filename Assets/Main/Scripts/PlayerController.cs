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
	public static int health;
	public static int healthcounter;

	void Start(){
		health = 100;
		healthcounter = 1;
	}

	void Update () {
		up = CrossPlatformInputManager.GetAxis ("Vertical");
		right = CrossPlatformInputManager.GetAxis ("Horizontal");
		transform.Translate (Vector3.right * moveSpeed * right);
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
	void OnCollisionExit2D(Collision2D col){
		if (col.collider.tag == "Platform") {
			grounded = false;
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "Platform") {
			grounded = true;
		}
	}
	void OnCollisionStay2D(Collision2D col){
		if (col.collider.tag == "Platform") {
			grounded = true;
		}
	}
}
