using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour {

	public GameObject[] tab1; //boutons du menu
	public GameObject[] tab2; //boutons du selecteur de niveau

	public GameObject MenuElements;
	public GameObject LevelSelector;

	public void HideButtons (int ButtonType){
		if (ButtonType == 0) {
			MenuElements.SetActive (false);
			LevelSelector.SetActive (true);
			foreach (GameObject bouton in tab1)
				bouton.SetActive (false);
			if (PlayerPrefs.GetInt ("modetest") == 0) {
				int i = 0;
				Debug.Log ("test");
				while (i <= PlayerPrefs.GetInt ("progression")) {
					Debug.Log (tab2 [i]);
					GameObject actualButton = tab2 [i];
					actualButton.SetActive (true);
					i++;
				}
				GameObject backButton = tab2 [3];
				backButton.SetActive (true);
			} else {
				foreach (GameObject bouton in tab2)
					bouton.SetActive (true);
			}
		} else {
			foreach (GameObject bouton in tab2)
				bouton.SetActive (false);
			if (ButtonType == 2) {
				MenuElements.SetActive (true);
				LevelSelector.SetActive (false);
				foreach (GameObject bouton in tab1)
					bouton.SetActive (true);
			}
		}
	}

			


	public void LoadScene(int level){
		Application.LoadLevel (level);

	}

	public void ModeChanger(int tester_mode){
		if (tester_mode == 0) {
			if (PlayerPrefs.HasKey ("progression") == false)
				PlayerPrefs.SetInt ("progression", 0);
			PlayerPrefs.SetInt ("modetest", 0);
		} else if (tester_mode == 1)
			PlayerPrefs.SetInt ("modetest", 1);
		else 
			Application.Quit ();
		PlayerPrefs.Save ();
	}

}
