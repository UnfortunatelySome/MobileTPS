using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public RectTransform healthTransform;
	private float catchedY;
	private float minXValue;
	private float maxXvalue;
	private float currentHealth;
	private float screenSize;
	private float catchedX;

	public static int amoutDecrease;
	public Image visualHealth;
	public RectTransform MainCanvas;

	// Use this for initialization
	void Start () {
		catchedY = healthTransform.position.y;
		maxXvalue = healthTransform.position.x;
		minXValue = healthTransform.position.x - healthTransform.rect.width;
		currentHealth = PlayerController.health;
		screenSize = MainCanvas.rect.width;
		catchedX = healthTransform.position.x;

		print (catchedY);
	}

	// Update is called once per frame
	void Update () {
		amoutDecrease = (PlayerController.healthcounter * 100) - PlayerController.health;

		if ((PlayerController.healthcounter * 100) <= 200) {
			amoutDecrease *= 200 / (PlayerController.healthcounter * 100) ;
		//} else if ((PlayerController.healthcounter * 100)  > 200) {
			//amoutDecrease /= (PlayerController.healthcounter * 100) / 200;
		//}
		float currentXValue = catchedX - amoutDecrease;

		healthTransform.position = new Vector3 (currentXValue, catchedY);
	}
}
}