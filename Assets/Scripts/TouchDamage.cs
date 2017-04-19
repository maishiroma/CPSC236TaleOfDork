using UnityEngine;
using System.Collections;

public class TouchDamage : MonoBehaviour {
	//Handles touch damage from enemies.

	public int damageAmount;

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.GetComponent<CharacterHealth>() != null){
			if(other.gameObject.GetComponent<CharacterHealth>().isEnemy == false){
				if(this.gameObject.GetComponentInParent<CharacterHealth>().HasInvincibilityFrames == false){
					other.gameObject.GetComponent<CharacterHealth>().TakeDamage(damageAmount,other.gameObject);
				}
			}
		}
	}
}
