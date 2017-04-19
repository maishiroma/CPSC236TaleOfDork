using UnityEngine;
using System.Collections;

public class HPSPUpItem : MonoBehaviour {
	//This item gives the player a defense boost or reduce the cost of special moves.

	public bool HealthUpgrade;				//True for health, false for special
	public float upgradeAmount;				//How much is the buff?

	void OnTriggerEnter2D(Collider2D other){	//Player collects it and uses it immediatly. 
		if(other.tag == "Player"){
			if(HealthUpgrade){
				if(other.GetComponent<CharacterHealth>()){
					other.GetComponent<CharacterHealth>().IncreaseDefense((int)upgradeAmount);
					Destroy(this.gameObject);
				}
			}
			else{
				if(other.GetComponent<PlayerActions>()){
					other.GetComponent<PlayerActions>().IncreaseSpecialDecrease((int)upgradeAmount);
					Destroy(this.gameObject);
				}
			}
		}
	}
	

}
