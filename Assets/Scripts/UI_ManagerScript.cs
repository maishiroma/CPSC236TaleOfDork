using UnityEngine;
using System.Collections;

public class UI_ManagerScript : MonoBehaviour {

	public void gameStart()
	{ 
		Destroy(GameObject.FindGameObjectWithTag("Music"));
		Application.LoadLevel ("Level1"); 
	}

	public void howToPlay()
	{ Application.LoadLevel ("HowToPlay"); }

	public void gameOver()
	{ Application.LoadLevel ("GameOver"); }

	public void loadMenu()
	{ 
		if(Application.loadedLevelName == "GameOver"){
			Destroy (GameObject.FindGameObjectWithTag("Music"));
		}
		Application.LoadLevel ("MainMenu"); 
	}

	public void quitGame()
	{ Application.Quit(); }

}
