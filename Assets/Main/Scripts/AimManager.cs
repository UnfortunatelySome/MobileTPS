using UnityEngine;
using System.Collections;

public class AimManager : MonoBehaviour {
	public Touch currentTouch;
	public Vector2 touchPos;
	public Vector3 wp;
	public Transform player;
	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInParent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		getTouch ();
		touchPos = Input.GetTouch (0).rawPosition;
	}
	void getTouch(){
		foreach (Touch tu in Input.touches) {
			
		}
	}
}
