using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float maxHealth;
	public float health;
	public bool indestructible;
	public bool solid;
	public GameObject greenBar;
	public int team; //team 0 is for environment objects with no team affiliation, team -1 is for free for all matches
	void Start(){
		health = maxHealth;
	}
	public void doDamage(float damage){
		health -= damage;
		if (greenBar != null&&health>=0) {
			updateHealthBar ();
		}
	}
	void updateHealthBar(){
		greenBar.transform.localScale = new Vector3 (health / maxHealth, greenBar.transform.localScale.y, greenBar.transform.localScale.z);
	}
}