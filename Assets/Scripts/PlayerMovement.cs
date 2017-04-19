using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	//Handles the player's controls

	public float moveSpeed = 5.0f;
	public int direction;
	public bool canMove = true;

	private Vector2 currSpeed = new Vector2(0,0);
	private KeyCode currKey;

	void Awake(){	//Allows the player and any other child object of it to remain in the game
		DontDestroyOnLoad(this);
	}

	//Controls the player's 4 directional movement and actions
	void Update () {
		if(canMove){
			if(Input.GetKeyUp(currKey)){
				currSpeed.x = 0;
				currSpeed.y = 0;
				GetComponent<Rigidbody2D>().velocity = currSpeed;
			}
			else{
				if(!IsInvoking("ResetAttack")){
					if(Input.GetKeyDown(KeyCode.UpArrow)){
						currSpeed.x = 0;
						currSpeed.y = moveSpeed;
						currKey = KeyCode.UpArrow;
						direction = 90;
					}
					else if(Input.GetKeyDown(KeyCode.LeftArrow)){
						currSpeed.x = -moveSpeed;
						currSpeed.y = 0;
						currKey = KeyCode.LeftArrow;
						direction = 180;
					}
					else if(Input.GetKeyDown(KeyCode.DownArrow)){
						currSpeed.x = 0;
						currSpeed.y = -moveSpeed;
						currKey = KeyCode.DownArrow;
						direction = 270;
					}
					else if(Input.GetKeyDown(KeyCode.RightArrow)){
						currSpeed.x = moveSpeed;
						currSpeed.y = 0;
						currKey = KeyCode.RightArrow;
						direction = 0;
					}
					else if(Input.GetKeyDown(KeyCode.Z)){	//melee attack
						currSpeed.x = 0;
						currSpeed.y = 0;
						currKey = KeyCode.Z;
						canMove = false;
						GetComponent<Animator>().SetBool("Attack",true);
						Invoke("ResetAttack",0.5f);
						this.gameObject.GetComponent<PlayerActions>().MeleeAttack(direction);
					}
					else if(Input.GetKeyDown(KeyCode.X)){	//use special weapon
						if(this.gameObject.GetComponent<PlayerActions>().currSpecialWeapon != null){
							currSpeed.x = 0;
							currSpeed.y = 0;
							canMove = false;
							this.gameObject.GetComponent<PlayerActions>().UseSpecialWeapon(direction);
						}
					}
					if(Input.GetKeyDown(KeyCode.LeftShift)){	//switch special weapons if have one
						this.gameObject.GetComponent<PlayerActions>().SwitchSpecialWeapon();
					}
					GetComponent<Rigidbody2D>().velocity = currSpeed;
				}
			}
		}
	}

	void FixedUpdate(){	//For animations
		GetComponent<Animator>().SetInteger("Direction",direction);
	}

	void OnTriggerEnter2D(Collider2D other){	//Wall collision
		if(other.tag == "Wall"){
			Rigidbody2D s = GetComponent<Rigidbody2D>();
			float newX = s.position.x;
			float newY = s.position.y;
			int sign = (int)Mathf.Sign(direction - 180);
			if(direction == 270 || direction == 90){
				newY = newY + 0.5f*sign;
			}
			else if(direction == 0 || direction == 180){
				newX = newX + 0.5f*sign;
			}
			s.position = new Vector2(newX,newY);
		}
	}

	void ResetAttack(){	//For animations
		GetComponent<Animator>().SetBool("Attack",false);
	}

	void OnLevelWasLoaded(int level){	//Warp player to the new area
		if(GameObject.Find("PlayerStart")){
			float newX = GameObject.Find("PlayerStart").transform.position.x;
			float newY = GameObject.Find("PlayerStart").transform.position.y;
			transform.position = new Vector3(newX,newY,5);
		}
		else if(Application.loadedLevelName == "victoryScreen"){
			Destroy(this.gameObject);
		}
	}

}
