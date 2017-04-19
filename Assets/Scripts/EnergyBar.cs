using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour {

	public float barDisplay;								// displays current progress
	public Vector2 posit = new Vector2(20, 40);				// can change position in Inspector
	public Vector2 size = new Vector2(60, 20);				// can change size in Inspector
	public GUIStyle progress_empty, progress_full;
	public Texture2D emptyTexture;
	public Texture2D fullTexture;

	public float currSpecialMeterValue;				//How much the meter is filled?
	public float maxSpecialMeterValue;				//What's the maximum capacity of the meter?
	private int restoreTimer = 150;					//Length of auto heal
	private int originalRestoreTimer;				//Stores original value of auto healing

	void Start(){
		maxSpecialMeterValue = currSpecialMeterValue;
		originalRestoreTimer = restoreTimer;
		barDisplay = currSpecialMeterValue;
	}

	void OnGUI(){
		// this draws background
		GUI.BeginGroup (new Rect (posit.x, posit.y, size.x, size.y));
			GUI.Box (new Rect (0, 0, size.x, size.y), emptyTexture, progress_empty);

			// draw filled bar
			GUI.BeginGroup (new Rect (0, 0, size.x * barDisplay, size.y));
				GUI.Box (new Rect (0, 0, size.x, size.y), fullTexture, progress_full);
			GUI.EndGroup();
		GUI.EndGroup ();
	}
	
	void Update () {	//Shows and auto heals the special meter
		if(currSpecialMeterValue < maxSpecialMeterValue){
			restoreTimer--;
			if(restoreTimer < 0){
				currSpecialMeterValue+= 0.05f;
				restoreTimer = originalRestoreTimer;
			}
		}
		barDisplay = currSpecialMeterValue;
	}

}
