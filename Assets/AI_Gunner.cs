using UnityEngine;
using System.Collections;

public class AI_Gunner : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (player.transform.position);
		gameObject.GetComponent<Weapon> ().isFiring = true;
	}
}
