using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public bool isFiring;
	public float range;
	public float rof;
	public float weight;
	public float recoil;
	public float accuracy;
	public float bulletSpeed;
	public float reloadTime;
	public float magSize;
	public float bulletsLoaded;
	public Transform shotPosition;
	public float timeSinceLastShot = 0;
	public float timeOfLastShot = 0;
	public Rigidbody2D projectile;
	void Update () {
		if (isFiring) {
			if (timeSinceLastShot >= rof) {
				fire ();
				timeOfLastShot = Time.time;
				timeSinceLastShot = 0;
			} else {
				timeSinceLastShot = Time.time - timeOfLastShot;
			}
		} else {
			timeSinceLastShot = Time.time - timeOfLastShot;
		}
	}
	public void fire(){
		Rigidbody2D bullet;
		bullet = Rigidbody2D.Instantiate (projectile, shotPosition.position, shotPosition.rotation) as Rigidbody2D;
		bullet.AddForce (shotPosition.up * bulletSpeed);
		int team = gameObject.GetComponentInParent<Health> ().team;
		bullet.gameObject.GetComponent<BulletInfo> ().team = team;
	}
}
