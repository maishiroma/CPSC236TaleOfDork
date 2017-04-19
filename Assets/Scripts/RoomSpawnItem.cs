using UnityEngine;
using System.Collections;

public class RoomSpawnItem : MonoBehaviour {
	/* This script is designed for spawning a item choosen prehand after beating all of the enemies in the room
		The number of enemies can vary. The item will spawn where this gameobject's placed. 
	*/

	public Transform itemToSpawn;	//This is the reward the player will get upon clearing the room

	private GameObject[] enemiesInRoom;		//All of the enemies in the room
	
	void Update () {	//Checks if all of the enemies in the room are defeate
		if(this.gameObject.GetComponent<Renderer>().isVisible){
			enemiesInRoom = GameObject.FindGameObjectsWithTag("Enemy");
			CleanArray();
			if(CheckIfAllNull()){
				var item = Instantiate(itemToSpawn) as Transform;
				item.position = this.gameObject.transform.position;
				item.parent = GameObject.Find("RoomItems").transform;
				Destroy (this.gameObject);
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
}
