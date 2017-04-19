using UnityEngine;
using System.Collections;

public class FireRodBehavior : MonoBehaviour {
	/*T	his class is dedicated in how the Fire Rod works in the game.
		This weapon shoots a straight fireball that's a  bit slower and doesn't have the range of the bow, 
		but does a lot of damage (30 HP). Uses a lot of the special meter (0.3 SP)
	*/

	public Transform shot;

	void OnTriggerEnter2D(Collider2D other){	//if enemy touches the rod when it's wipped out
		if(other.tag == "Enemy"){
			int d = this.gameObject.GetComponent<SpecialWeaponDefaults>().damageAmount;
			other.gameObject.GetComponent<CharacterHealth>().TakeDamage(d,other.gameObject);
		}
	}

	public void FireShot(int direc, GameObject player){	//Shoots a fireball straight ahead.
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
		var v = Instantiate(shot) as Transform;
		v.position = this.gameObject.transform.position;
		ProjectileAttack s = v.GetComponent<ProjectileAttack>();
		if(s != null){
			s.directionOfShot = new Vector3(tempX,tempY,0);
			s.speedOfShot = this.gameObject.GetComponent<SpecialWeaponDefaults>().shootSpeed;
			s.transform.parent = player.transform;
			s.powerOfShot = this.gameObject.GetComponent<SpecialWeaponDefaults>().damageAmount;
		}
		this.gameObject.GetComponent<SpecialWeaponDefaults>().Invoke("Hide",0.5f);
	}
}
