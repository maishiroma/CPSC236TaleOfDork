using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {
	/*	Controls the amount of hits the entity can take. Also does the invincibility too.
		also handles healing player
	*/

	public int maxHealth; 			//the max HP this character has.
	public int defensePoints;		//The amount that is used to factor in how much damage is resisted
	public bool isEnemy = true; 	//This is false if the character is the player. 
	public Transform healthBar;		//ONLY for player; displays the player health in pictures.

	private int currHealth;				//Current health the character has.
	private bool inviniFrames = false;	//Controls invinible frames
	private int counterForInvini = 60;	//Determines how long the character is invincbile after getting hit

	public bool HasInvincibilityFrames{get {return inviniFrames;}}
	public int CurrentHealth{get{return currHealth;}}
	
	void Start () {	//sets the currHealth to the maxHealth when spawned
		currHealth = maxHealth;
		if(healthBar != null){
			float m = currHealth / 100;
			healthBar.GetComponent<HealthScript>().currHealthViewed = m;
		}
	}

	void Update () {
		if(inviniFrames == true){
			counterForInvini -=1;
			if(counterForInvini < 0){
				counterForInvini = 60;
				Color c = this.gameObject.GetComponent<Renderer>().material.color;
				c.a = 1.0f;
				this.gameObject.GetComponent<Renderer>().material.color = c;
				inviniFrames = false;
			}
		}
	}

	void DetermineIfDead(GameObject currObj){
		if(currHealth <= 0){
			if(isEnemy){
				GetComponent<DropHealth>().RandomDrop(this.gameObject);
				Destroy(this.gameObject);
			}
			else{
				//game over screen
				Destroy(this.gameObject);
				if(GameObject.FindGameObjectWithTag("Music")){
					Destroy(GameObject.FindGameObjectWithTag("Music"));
				}
				if(GameObject.FindGameObjectWithTag("GUI")){
					Destroy (GameObject.FindGameObjectWithTag("GUI"));
				}
				Application.LoadLevel("GameOver");
			}
		}
		else{
			inviniFrames = true;
			Knockback(this.gameObject);
		}
	}

	public void Knockback(GameObject currObj){
		if(isEnemy){
			Rigidbody2D s = currObj.GetComponent<Rigidbody2D>();
			PlayerMovement p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
			float newX = s.position.x;
			float newY = s.position.y;
			switch(p.direction){
				case 0:
					newX += 2f;
					break;
				case 90:
					newY += 2f;
					break;
				case 180:
					newX -= 2f;
					break;
				case 270:
					newY -= 2f;
					break;
			}
			Vector2 v = new Vector2(newX,newY);
			if(Vector3.Distance(v,GameObject.FindGameObjectWithTag("MainCamera").transform.position) <= 5f){
				s.position = v;
			}
		}
		else{
			Rigidbody2D s = currObj.GetComponent<Rigidbody2D>();
			PlayerMovement p = currObj.GetComponent<PlayerMovement>();
			int sign = (int)Mathf.Sign(p.direction - 180);
			if(p.direction == 270 || p.direction == 90){
				Vector2 n = new Vector2(s.position.x, s.position.y + 1.5f*sign);
				if(Vector3.Distance(n,GameObject.FindGameObjectWithTag("MainCamera").transform.position) < 4f){
					s.position = n;				
				}
			}
			else if(p.direction == 0 || p.direction == 180){
				Vector2 n = new Vector2(s.position.x + 1.5f*sign, s.position.y);
				if(Vector3.Distance(n,GameObject.FindGameObjectWithTag("MainCamera").transform.position) < 8f){
					s.position = n;				
				}
			}
		}
	}
	

	public void TakeDamage(int damageDealt, GameObject currObj){
		if(inviniFrames == false){
			if(currObj.tag == "Player"){
				if(damageDealt-(5*defensePoints) > 0){
					currHealth = currHealth - (damageDealt-(5*defensePoints));
				}
				float d = currHealth / 100f;
				healthBar.GetComponent<HealthScript>().currHealthViewed = d;
			}
			else{
				currHealth = currHealth - damageDealt;
			}
			inviniFrames = true;
			Color c = this.gameObject.GetComponent<Renderer>().material.color;
			c.a -= 0.5f;
			this.gameObject.GetComponent<Renderer>().material.color = c;
			DetermineIfDead(currObj);
		}
	}
	
	public void HealPlayer(int amountHealed){
		if(isEnemy == false){
			if(currHealth + amountHealed > maxHealth){
				currHealth = maxHealth;
				float d = maxHealth / 100f;
				healthBar.GetComponent<HealthScript>().currHealthViewed = d;
			}
			else{
				currHealth = currHealth + amountHealed;
				float d = amountHealed / 100f;
				healthBar.GetComponent<HealthScript>().currHealthViewed += d;
			}
		}
	}

	public void IncreaseDefense(int buff){
		defensePoints += buff;
		HealPlayer(maxHealth);
	}
	
	public bool CheckIfFullHealth(){	//returns true if player is at full health
		return currHealth == maxHealth;
	}

	public bool CheckIfDead(){	//return true if the character is dead
		return currHealth <= 0;
	}
}
