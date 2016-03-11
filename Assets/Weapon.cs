using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public bool isFiring;
	public float range;
	public float rof;
	public float weight;
	public float recoil;
	public float accuracy;
	float timeSinceLastShot = 0;
	float timeOfLastShot = 0;
	public GameObject projectile;
	void Update () {
		if (isFiring) {
			if (timeSinceLastShot >= rof) {
				fire ();
				timeOfLastShot = Time.time;
			}
		} else {
			timeSinceLastShot = Time.time - timeOfLastShot;
		}
	}
	public void fire(){
	}
}
