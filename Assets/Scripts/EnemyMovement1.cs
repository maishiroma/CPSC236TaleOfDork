using UnityEngine;
using System.Collections;

public class EnemyMovement1 : MonoBehaviour {
	/*	This Enemy will move towards the player if the player is in the circle collision of the enemy. Else,
		this enemy will simply wait in the area. 
	 */

	public int moveSpeed;				//How fast the enemy is in moving
	public int closestItGetsToPlayer;	//The closest it will be to the player.

	void OnTriggerStay2D(Collider2D other){
		if(this.gameObject.transform.parent.GetComponent<Renderer>().isVisible){
			if(other.tag == "Player" && GetComponentInParent<CharacterHealth>().HasInvincibilityFrames == false){
				if(Vector3.Distance(other.transform.position,transform.parent.position) >= closestItGetsToPlayer){
					Vector3 direc = other.transform.position - transform.parent.position;
					direc.Normalize();
					transform.parent.position += direc * moveSpeed * Time.deltaTime;
				}
			}
		}
	}

}
