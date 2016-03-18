using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float maxHealth;
	public float health;
	public bool indestructible;
	public bool solid;
	private float catchedY;
	private float catchedZ;
	private float changeX;
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

	void Update (){
		catchedY = greenBar.transform.position.y;
		catchedZ = greenBar.transform.position.z;
	}
	void updateHealthBar(){
		changeX = maxHealth - health;

		greenBar.transform.position = new Vector3 (- changeX, catchedY, catchedZ);
	}
}