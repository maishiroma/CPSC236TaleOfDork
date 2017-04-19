using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {
	//Works in conjuction with the PlayerMovement script to allow the player to do various actions.

	public EnergyBar energy;									//The energy bar for special moves
	public Transform meleeWeapon;								//Melee Weapon
	public GameObject currSpecialWeapon;						//Current special weapon the player has
	public GameObject[] specialWeapons = new GameObject[3];		//Where all special weapons are held
	
	private bool inAction = false;								//To prevent the player from spamming moves
	private int keysOnHand = 0;									//Stores all of the keys that the player found
	private int specialIndex = 0;								//Current index in special inventory
	private int specialCostReducer;								//Modifier for mow much special moves get reduced

	public Transform MeleeWeapon{get{return meleeWeapon;}}
	public int GetKeys{get{return keysOnHand;}}

	public void MeleeAttack(int direc){	//Swipes a sword in front of the player.
		if(inAction == false){
			inAction = true;
			var temp = Instantiate(meleeWeapon) as Transform;
			temp.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
			AllignToPlayer(direc,temp,false);
			Invoke ("AllowActionAgain",0.2f);
		}
	}

	public void UseSpecialWeapon(int direc){	//Dynamically decides which weapon the player has and uses it.
		float subFromSM = (currSpecialWeapon.GetComponent<SpecialWeaponDefaults>().consumeSMeter - (0.05f*specialCostReducer));
		if(subFromSM <= 0){
			subFromSM = 0.05f;
		}
		if(energy.currSpecialMeterValue - subFromSM > 0){
			AllignToPlayer(direc,currSpecialWeapon.transform,true);
			if(currSpecialWeapon.tag == "Special1"){	//boomerang
				currSpecialWeapon.GetComponent<BoomerangBehavior>().Attack(direc,this.gameObject);
				Invoke ("AllowActionAgain",0.5f);
			}
			else if(currSpecialWeapon.tag == "Special2"){	//bow
				currSpecialWeapon.GetComponent<BowBehavior>().FireShot(direc,this.gameObject);
				Invoke ("AllowActionAgain",0.5f);
			}
			else if(currSpecialWeapon.tag == "Special3"){	//fire rod
				currSpecialWeapon.GetComponent<FireRodBehavior>().FireShot(direc, this.gameObject);
				Invoke ("AllowActionAgain",0.5f);
			}
			energy.currSpecialMeterValue -= subFromSM;
		}
		else{
			AllowActionAgain();
		}
	}

	public void AddSpecialItem(GameObject special){
		for(int i = 0; i < specialWeapons.Length; i++){
			if(specialWeapons[i] == null){
				specialWeapons[i] = special;
				special.transform.position = new Vector3(special.transform.position.x, special.transform.position.y, -10);
				SwitchSpecialWeapon();
				break;
			}
		}
	}

	public void SwitchSpecialWeapon(){
		if(checkIfSpecialEmpty() == false){
			if(specialIndex == specialWeapons.Length){
				specialIndex = 0;
			}
			else if(specialWeapons[specialIndex] == null){
				specialIndex = 0;
			}
			currSpecialWeapon = specialWeapons[specialIndex];
			if(GameObject.Find("SpecialWeaponGUI")){
				GameObject.Find("SpecialWeaponGUI").GetComponent<SpriteRenderer>().sprite = currSpecialWeapon.GetComponent<SpriteRenderer>().sprite;
			}
			specialIndex++;
		}
	}
	

	public void AddKeyToInventory(int keyValue){
		keysOnHand = keysOnHand + keyValue;
	}

	public void DeleteKeyFromInventory(){
		keysOnHand--;
	}

	public void ResetKeys(){
		keysOnHand = 0;
	}

	public void IncreaseSpecialDecrease(int num){
		specialCostReducer += num;
		energy.currSpecialMeterValue = energy.maxSpecialMeterValue;
	}

	void AllowActionAgain(){	//use Invoke("AllowActionAgain",timeFrame) to allow the actions to be redone
		inAction = false;
		GetComponent<PlayerMovement>().canMove = true;
	}

	bool checkIfSpecialEmpty(){		//Returns true if the special inventory is empty
		for(int i = 0; i < specialWeapons.Length; i++){
			if(specialWeapons[i] != null){
				return false;
			}
		}
		return true;
	}

	void AllignToPlayer(int direc, Transform temp, bool v2){
		float tempX = this.gameObject.transform.position.x;
		float tempY = this.gameObject.transform.position.y;
		float tempZ = this.gameObject.transform.position.z;
		switch(direc){
		case 0:
			tempX += 0.5f;
			if(v2 == false){
				tempY -= 0.5f;
			}
			break;
		case 90:
			tempY += 0.2f;
			break;
		case 180:
			tempX -= 0.5f;
			if(v2 == false){
				tempY -= 0.5f;
			}
			break;
		case 270:
			tempY -= 0.2f;
			break;
		}
		temp.transform.position = new Vector3(tempX,tempY,tempZ);
	}
}
