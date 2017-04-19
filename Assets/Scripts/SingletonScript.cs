using UnityEngine;
using System.Collections;

public class SingletonScript : MonoBehaviour {
	//Makes sure there's only one instance of that gameobject

	private static SingletonScript instance = null;
	public static SingletonScript Instance {
		get { return instance; }
	}

	void Update(){
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		}
		else {
			instance = this;
		}
	}
}
