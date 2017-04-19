using UnityEngine;
using System.Collections;

public class SpecialWeaponDefaults : MonoBehaviour {
	//This will contain all of the defauts that all of the special weapons will share. 
	
	public int damageAmount;			//How much damage this weapon does to enemies
	public float consumeSMeter;			//Consumes the special meter. 
	public int shootSpeed;				//How fast this weapon moves either itself or its projectiles

	private bool hasObtained = false;			//This changes if the player actually got the weapon in game

	void OnTriggerEnter2D(Collider2D other){	//picking up the weapon for the first time
		if(other.tag == "Player"){
			if(hasObtained == false){
				hasObtained = true;
				other.GetComponent<PlayerActions>().AddSpecialItem(this.gameObject);
				this.gameObject.transform.parent = other.transform;
			}
		}
	}

	public void Hide(){	//Hides the weapon so it looks like it's "put away"
		this.gameObject.transform.position = new Vector3(
			this.gameObject.transform.position.x,
			this.gameObject.transform.position.y,
			-10);

	}
}