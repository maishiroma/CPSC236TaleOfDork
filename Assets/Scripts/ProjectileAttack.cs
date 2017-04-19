using UnityEngine;
using System.Collections;

public class ProjectileAttack : MonoBehaviour {
	/* Handles the projectiles that appear in the game. 
	 * This is often called when the projectle is spawned.
	 */
	
	public bool isEnemyShot = true;		//Who fired it? An Enemy, or the player?
	public int powerOfShot;				//How strong is the shot?
	public float selfDestruct;			//How long will the shot be on screen before fizzing out?
	public int speedOfShot;				//How fast will the shot be?
	public Vector2 directionOfShot;		//Where is the shot moving?

	void Start(){
		Destroy(this.gameObject,selfDestruct);
	}

	void Update(){
		if(!GetComponent<Renderer>().isVisible){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(isEnemyShot == true){
			if(other.gameObject.tag == "Player"){
				other.gameObject.GetComponent<CharacterHealth>().TakeDamage(powerOfShot,other.gameObject);
				Destroy(this.gameObject);
			}
			else if(other.tag == "Wall" || GetComponent<Renderer>().isVisible == false){
				Destroy(this.gameObject);
			}
		}
		else{
			if(other.tag == "Enemy"){
				other.gameObject.GetComponent<CharacterHealth>().TakeDamage(powerOfShot,other.gameObject);
				Destroy(this.gameObject);
			}
			else if(other.tag == "Wall" || GetComponent<Renderer>().isVisible == false){
				Destroy(this.gameObject);
			}
		}
	}

	void FixedUpdate(){
		GetComponent<Rigidbody2D>().velocity = directionOfShot * speedOfShot;
	}
}
