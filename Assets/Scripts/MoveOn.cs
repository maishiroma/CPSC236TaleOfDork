using UnityEngine;
using System.Collections;

public class MoveOn : MonoBehaviour {
	/* This warps the player to the next level upon touching it. If it's the end of the game, the results
	 * screen will show up.
	 */
	
	public string nextLevel;				//name of area to warp to.

	void OnTriggerEnter2D(Collider2D other){	//if Player touches this
		if(other.tag == "Player"){
			if (GameObject.FindGameObjectWithTag("Music")) {
				Destroy (GameObject.FindGameObjectWithTag("Music"));
			}
			other.GetComponent<PlayerActions>().ResetKeys();
			Application.LoadLevel(nextLevel);
		}
	}
}
