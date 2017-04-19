using UnityEngine;
using System.Collections;

public class DropHealth : MonoBehaviour {
	/* This class randomely lets a defeated enemy drop health
	*/

	public Transform healthItem;	//Health item to drop

	public void RandomDrop(GameObject enemy){	//Drops the item at the enemy's position when defeated
		int r = (int)Random.Range(1,10);
		if(r > 5){
			var v = Instantiate(healthItem) as Transform;
			v.position = enemy.transform.position;
			v.parent = GameObject.Find("RoomItems").transform;
		}
	}
}
