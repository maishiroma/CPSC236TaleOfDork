using UnityEngine;
using System.Collections;

public class EnemyMovement2 : MonoBehaviour {
	/*This enemy has a wide range and will shoot the player from afar if the player is in it's range.
	 * The Shots will go in all 4 diretions.
	*/

	public Transform EnemyShot;		//The projectile that the enemy uses.
	public int firingRate;			//The rate the enemy will attack the player
	public int shotSpeed;

	private int storeFireRate;
	private bool inRange = false;

	void Start(){
		storeFireRate = firingRate;
	}

	void Update(){
		if(this.gameObject.transform.parent.GetComponent<Renderer>().isVisible){
			if(inRange){
				firingRate -=1;
				if(firingRate < 0){
					FireShot();
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && GetComponentInParent<CharacterHealth>().HasInvincibilityFrames == false){
			inRange = true;
			firingRate = 1;
		}
	}

	void FireShot(){
		Vector2 v1 = new Vector2(1,0);
		Vector2 v2 = new Vector2(0,1);
		Vector2 v3 = new Vector2(-1,0);
		Vector2 v4 = new Vector2(0,-1);
		Vector2[] v = {v1,v2,v3,v4};

		for(int i = 0; i < v.Length; i++){
			var shot = Instantiate(EnemyShot) as Transform;
			shot.position = this.transform.parent.position;
			ProjectileAttack s = shot.GetComponent<ProjectileAttack>();
			if(s != null){
				s.directionOfShot = v[i];
				s.speedOfShot = shotSpeed;
				s.isEnemyShot = true;
			}
		}
		firingRate = storeFireRate;
	}

}
