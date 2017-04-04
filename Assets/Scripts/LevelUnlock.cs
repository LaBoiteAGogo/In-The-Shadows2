using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour {

	public GameObject[] levelButtons;

	// Use this for initialization
	void Start () {
		Debug.Log (PlayerPrefs.GetInt ("progression"));
		Debug.Log (PlayerPrefs.GetInt ("modetest"));
		if (PlayerPrefs.GetInt ("modetest") == 0) {
			int i = 0;
			Debug.Log ("test");
					while (i < levelButtons.Length) {
						if (PlayerPrefs.GetInt ("progression") < i){
							Debug.Log (levelButtons [i]);
							Button actualButton = levelButtons[i].GetComponent<Button>();
							actualButton.interactable = false;
						}
					i++;
					}
			}			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
