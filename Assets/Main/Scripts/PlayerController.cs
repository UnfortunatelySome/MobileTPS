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
		direction = Vector2.Angle (new Vector2 (0,1), new Vector2 (right, up));
		if (Mathf.Pow (up, 2) + Mathf.Pow (right, 2)>=moveThreshold) {
			moving = true;
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
}
