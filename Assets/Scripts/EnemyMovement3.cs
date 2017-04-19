using UnityEngine;
using System.Collections;

public class EnemyMovement3 : MonoBehaviour {
	//This enemy will move towards the player when the camera is viewing it.

	public int moveSpeed = 1;

	void Update(){
		if(this.gameObject.transform.parent.GetComponent<Renderer>().isVisible){
			GameObject p = GameObject.FindGameObjectWithTag("Player");
			if(p != null){
				if(Vector3.Distance(p.transform.position,transform.parent.position) > 0 && GetComponentInParent<CharacterHealth>().HasInvincibilityFrames == false){
					Vector3 direc = p.transform.position - transform.parent.position;
					direc.Normalize();
					transform.parent.position += direc * moveSpeed * Time.deltaTime;
				}
			}
		}
	}
}
