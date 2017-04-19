using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	/*	When activated, this will spawn enemies as long as it has enemies remaining to spawn as 
	 * well as the one it already spawned is dead. 
	*/

	public Transform[] enemiesInventory;	//The number and kinds of enemies this spawner has
	private Transform currEnemy;			//Current enemy that's spawned
	private int currIndex = 0;				//What enemy is it on to spawn
	
	void Start () {
		SpawnEnemy();
	}

	void Update () {
		if(GetComponent<Renderer>().isVisible){
			if(currIndex > enemiesInventory.Length){
				Destroy(this.gameObject);
			}
			else if(currEnemy == null && IsInvoking("SpawnEnemy") == false){
				Invoke("SpawnEnemy",1f);
				currIndex++;
			}
		}
	}

	void SpawnEnemy(){
		if(currIndex < enemiesInventory.Length){
			currEnemy = Instantiate(enemiesInventory[currIndex]) as Transform;
			currEnemy.parent = GameObject.Find("Enemys").transform;
			enemiesInventory[currIndex] = null;
			currEnemy.position = this.transform.position;
		}
	}

	bool CheckIfEmpty(){
		for(int i = 0; i < enemiesInventory.Length; i++){
			if(enemiesInventory[i] != null){
				return false;
			}
		}
		return true;
	}
}
