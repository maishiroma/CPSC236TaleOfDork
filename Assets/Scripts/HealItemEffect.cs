using UnityEngine;
using System.Collections;

public class HealItemEffect : MonoBehaviour {

	public int healEffect;	//how much the item can heal
	private int ticker = 360;
	private bool almostGone = false;

	void Update(){	//After 6 seconds, the item dissapears
		ticker -=1;
		if(ticker < 120 && almostGone == false){
			almostGone = true;
			Color c = this.gameObject.GetComponent<Renderer>().material.color;
			c.a -= 0.5f;
			this.gameObject.GetComponent<Renderer>().material.color = c;
		}
		if(ticker < 0){
			Destroy(this.gameObject);
		}
	}


	void OnTriggerEnter2D(Collider2D other){	//player uses item on the spot
		if(other.tag == "Player"){
			if(other.GetComponent<CharacterHealth>().CurrentHealth < other.GetComponent<CharacterHealth>().maxHealth){
				other.GetComponent<CharacterHealth>().HealPlayer(healEffect-(5*other.GetComponent<CharacterHealth>().defensePoints));
				Destroy(this.gameObject);
			}
		}
	}
}
