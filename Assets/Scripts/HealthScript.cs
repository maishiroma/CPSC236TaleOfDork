using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public float barDisplay;								// displays current progress
	public Vector2 posit = new Vector2(20, 40);				// can change position in Inspector
	public Vector2 size = new Vector2(60, 20);				// can change size in Inspector
	public GUIStyle progress_empty, progress_full;
	public Texture2D emptyTexture;
	public Texture2D fullTexture;
	
	public float currHealthViewed;		//Displays the current health of the player
	
	void Start(){
		barDisplay = currHealthViewed;
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
	
	void Update () {
		barDisplay = currHealthViewed;
	}

}
