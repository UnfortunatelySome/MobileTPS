using UnityEngine;
using System.Collections;

public class BulletInfo : MonoBehaviour {
	public float damage;
	public float piercing;
	public int team;
	 
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.GetComponent<Health>() != null) {
			Health hit = col.gameObject.GetComponent<Health> ();
			if (hit.team != this.team) {
				hit.doDamage (this.damage);
				if (hit.solid) {
					Destroy (gameObject, 0.1f);
				}
			}
		}
	}
}
