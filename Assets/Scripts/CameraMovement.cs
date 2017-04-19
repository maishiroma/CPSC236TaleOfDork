using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	/*	The camera will pan to show a new area, depending on where the player touched the bounds of it. 
		The main camera's helpers  will use this.
	*/

	public int moveDisitance; 		//This changes depending on room size. All rooms need to be same size!
	public int whichSide;			//Represents which direction you're going to go in.
	public bool canLeave = true;	//Can the player leave the room?
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			if(canLeave == true){
				float newX = this.gameObject.transform.parent.position.x;
				float newY = this.gameObject.transform.parent.position.y;
				float playerX = other.transform.position.x;
				float playerY = other.transform.position.y;
				if(whichSide == 90){
					newY = this.gameObject.transform.parent.position.y + moveDisitance;
					playerY += 3;
				}
				else if(whichSide == 270){
					newY = this.gameObject.transform.parent.position.y - moveDisitance;
					playerY -= 3;
				}
				else if(whichSide == 0){
					newX = this.gameObject.transform.parent.position.x + moveDisitance;
					playerX += 2;
				}
				else if(whichSide == 180){
					newX = this.gameObject.transform.parent.position.x - moveDisitance;
					playerX -= 2;
				}
				Vector2 p = new Vector2(newX,newY);
				Vector3 p3 = new Vector3(playerX,playerY,5);
				this.gameObject.transform.parent.position = p;
				other.transform.position = p3;
				if(GameObject.FindGameObjectWithTag("Floor")){	//Moves the floor graphic
					Vector3 p2 = new Vector3(p.x,p.y,15);
					GameObject.FindGameObjectWithTag("Floor").transform.position = p2;
				}
				if(GameObject.Find("SpecialWeaponGUI")){		//Moves the Special Weapon GUI
					GameObject g = GameObject.Find("SpecialWeaponGUI");
					newX = g.transform.position.x;
					newY = g.transform.position.y;
					switch(whichSide){
						case 0:
							newX += 20f;
							break;
						case 90:
							newY += 12f;
							break;
						case 180:
							newX -= 20f;
							break;
						case 270:
							newY -= 12f;
							break;
					}
					g.transform.position = new Vector3(newX,newY,1);
				}
			}
			else{	//Knocks the player back into the room
				Rigidbody2D s = other.gameObject.GetComponent<Rigidbody2D>();
				PlayerMovement p = other.gameObject.GetComponent<PlayerMovement>();
				int sign = (int)Mathf.Sign(p.direction - 180);
				if(p.direction == 270 || p.direction == 90){
					Vector2 n = new Vector2(s.position.x, s.position.y + 1.5f*sign);
					s.position = n;
				}
				else if(p.direction == 0 || p.direction == 180){
					Vector2 n = new Vector2(s.position.x + 1.5f*sign, s.position.y);
					s.position = n;
				}
			}

		}
	}

}
