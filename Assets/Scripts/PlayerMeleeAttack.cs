using UnityEngine;
using System.Collections;

public class PlayerMeleeAttack : MonoBehaviour {
	//Controls the player melee attack property

	public float selfDestruct;		//How long does the attack last?
	public int power;				//The power of the weapon

	void Start () {
		Destroy(this.gameObject,selfDestruct);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.GetComponent<CharacterHealth>() != null && other.tag == "Enemy"){
			other.gameObject.GetComponent<CharacterHealth>().TakeDamage(power,other.gameObject);
		}
	}
}
