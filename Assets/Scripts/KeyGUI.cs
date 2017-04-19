using UnityEngine;
using System.Collections;

public class KeyGUI : MonoBehaviour {
	/*	This class is dedicated to how the number of keys the player currently has. This object will have
	 *  the image of the key and the number of keys right next to it. Pretty simple...
	 */
	private Vector3 v = new Vector3(0.05f,0.93f,1f);
	private Vector3 resolution;
	private float x;
	private float y;	

	void Start(){
		transform.position = Camera.main.ViewportToWorldPoint(v);
		resolution = new Vector2(Screen.width,Screen.height);
		x = Screen.width/720f;
		y = Screen.height/450f;
	}
	
	void OnGUI(){
		if(GameObject.FindGameObjectWithTag("Player") != null){
			int p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().GetKeys;
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.yellow;
			style.fontSize = 25;
			if(Screen.width!=resolution.x || Screen.height!=resolution.y)
			{
				resolution=new Vector2(Screen.width, Screen.height);
				x=resolution.x/720.0f;
				y=resolution.y/450.0f;
				transform.localScale = Camera.main.ViewportToWorldPoint(v);
			}
			GUI.Label(new Rect(46.42f * x,13.4f * y ,0,0),"= " + p ,style);
		}
	}
}
