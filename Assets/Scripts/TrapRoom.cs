using UnityEngine;
using System.Collections;

public class TrapRoom : MonoBehaviour {
	/* When the player enters a room with this, the player can't leave the room until all of the enemies are
	 * all gone. 
	 */

	private GameObject[] enemiesInRoom;		//All of the enemies in the room
	private bool alreadyTrap = false;

	void Update () {
		if(GetComponent<Renderer>().isVisible){
			if(alreadyTrap == false){
				helper();
			}
			if(CheckIfAllNull()){
				Destroy (this.gameObject);
				GameObject m = GameObject.FindGameObjectWithTag("MainCamera");
				for(int i = 0; i < m.transform.childCount -1; i++){
					if(m.transform.GetChild(i).GetComponent<CameraMovement>() != null){
						m.transform.GetChild(i).GetComponent<CameraMovement>().canLeave = true;
					}
				}
			}
		}
	}

	void CleanArray(){		//Only finds enemies that are visible in the room
		for(int i = 0; i < enemiesInRoom.Length; i++){
			if(!enemiesInRoom[i].GetComponent<Renderer>().isVisible){
				enemiesInRoom[i] = null;
			}
		}
	}
	
	bool CheckIfAllNull(){	//Checks if the enemy list is gone, aka null
		for(int i = 0; i < enemiesInRoom.Length; i++){
			if(enemiesInRoom[i] != null){
				return false;
			}
		}
		return true;
	}

	void helper(){	//Makes the update method do slightly less work
		enemiesInRoom = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject m = GameObject.FindGameObjectWithTag("MainCamera");
		for(int i = 0; i < m.transform.childCount -1; i++){
			if(m.transform.GetChild(i).GetComponent<CameraMovement>() != null){
				m.transform.GetChild(i).GetComponent<CameraMovement>().canLeave = false;
			}
		}
		CleanArray();
		alreadyTrap = true;
	}
}
