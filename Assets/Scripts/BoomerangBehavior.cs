using UnityEngine;
using System.Collections;

public class BoomerangBehavior : MonoBehaviour {
	/*	This class is dedicated in how the boomerang works in the game.
		The boomerang goes in a straight line and returns to the player. If it hits an enemy, it does
		5 HP of damage and immediatly heads back to the player.
		It has the lowest range and attack power of the three weapons, but it comsumes the least of the special
		meter. (0.1 SP)
	 */ 

	public int disitanceMax;		//How far does it travel before returning to the player
	public int timer;				//Extra check to prevent it from being spawned for too long

	private bool thrown = false;
	private bool returnTrip = false;
	//Stored from the method Attack
	private GameObject player;
	private int direc;
	private int origTimer;

	void Start(){
		origTimer = timer;
	}

	void Update(){
		if(thrown == true){	//Returns to player
			timer--;
			if(returnTrip == true){
				Vector3 direc = player.transform.position - this.gameObject.transform.position;
				this.gameObject.GetComponent<Rigidbody2D>().velocity = direc * GetComponent<SpecialWeaponDefaults>().shootSpeed;
				if(timer < 0){
					Reset();
				}
			}
			else{
				float tempX = 1f;
				float tempY = 1f;
				switch(direc){
				case 0:
					tempY = 0;
					break;
				case 90:
					tempX = 0;
					break;
				case 180:
					tempX = tempX * -1f;
					tempY = 0;
					break;
				case 270:
					tempY = tempY * - 1f;	
					tempX = 0;
					break;
				}
				Vector3 c = new Vector3(tempX,tempY,0);
				GetComponent<Rigidbody2D>().velocity = c * GetComponent<SpecialWeaponDefaults>().shootSpeed;
				if(Vector3.Distance(player.transform.position,transform.position) > disitanceMax){
					returnTrip = true;
				}
				else if(timer < 0){
					Reset();
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){	//This can pass through walls
		if(other.tag == "Player" && returnTrip == true){
			Reset();
		}
		else if(other.tag == "Enemy"){
			if(other.gameObject.GetComponent<CharacterHealth>() != null){
				other.gameObject.GetComponent<CharacterHealth>().TakeDamage(
					this.gameObject.GetComponent<SpecialWeaponDefaults>().damageAmount,other.gameObject);
				returnTrip = true;
			}
		}
	}

	void Reset(){	//resets boomerang back to normal
		thrown = false;
		returnTrip = false;
		timer = origTimer;
		this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		this.gameObject.GetComponent<SpecialWeaponDefaults>().Hide();
	}

	public void Attack(int dir, GameObject p){	//Activates Update method
		direc = dir;
		player = p;
		thrown = true;
	}

}
