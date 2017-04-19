using UnityEngine;
using System.Collections;

public class BowBehavior : MonoBehaviour {
	/*	This class is dedicated in how the Bow works in the game
		The bow shoots a fast arrow in front of the player that travels the entire screen to do 15 damage.
		It consumes a decent amount of the special meter though. (0.2 SP)
	*/
	public Transform arrow;

	public void FireShot(int direc, GameObject player){	//Shoots a arrow straight ahead.
		float tempX = 1f;
		float tempY = 1f;
		var v = Instantiate(arrow) as Transform;
		v.position = this.gameObject.transform.position;
		switch(direc){
		case 0:
			tempY = 0;
			v.Rotate(0,0,90);
			break;
		case 90:
			tempX = 0;
			break;
		case 180:
			tempX = tempX * -1f;
			tempY = 0;
			v.Rotate(0,0,-90);
			break;
		case 270:
			tempY = tempY * - 1f;	
			tempX = 0;
			v.Rotate(0,0,180);
			break;
		}
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
