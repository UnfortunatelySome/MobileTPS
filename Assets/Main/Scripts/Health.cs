using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public float maxHealth;
	public float health;
	public bool indestructible;
	public bool solid;
	public Transform healthPlayer;
	private float catchedY;
	private float catchedZ;
	private float catchedX;
	private float changeX;
	public GameObject greenBar;
	public int team; //team 0 is for environment objects with no team affiliation, team -1 is for free for all matches

	void Start(){
		health = maxHealth;

		//greenBar.transform.position = new Vector3 (catchedX,catchedY);

	}
	//public void doDamage(float damage){
	//	health -= damage;
	//	if (greenBar != null&&health>=0) {
		//	updateHealthBar ();
	//	}
	//}

	void Update (){
		catchedY = healthPlayer.position.y;
		catchedZ = healthPlayer.position.z;
		catchedX = healthPlayer.position.x;
		
		changeX = maxHealth - health;

		float DirectCHangeX = changeX / maxHealth;

		print (health);

		greenBar.transform.position = new Vector3 (((-DirectCHangeX)  +  this.transform.position.x) , greenBar.transform.position.y , greenBar.transform.position.z);
	}
	void updateHealthBar(){
		
	}
}