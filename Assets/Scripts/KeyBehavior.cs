using UnityEngine;
using System.Collections;

public class KeyBehavior : MonoBehaviour {
	//This adds one key to the player when the player collects this. It also handles opening doors.

	private int keyValue = 1;
	public bool isDoor = false; 	//True if this is a door

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			if(isDoor == false){	//Pick up key
				other.GetComponent<PlayerActions>().AddKeyToInventory(keyValue);
				Destroy(this.gameObject);
			}
			else{
				if(other.GetComponent<PlayerActions>().GetKeys > 0){	//Unlock a door
					other.GetComponent<PlayerActions>().DeleteKeyFromInventory();
					Destroy(this.gameObject);
				}
			}
		}
	}
}
