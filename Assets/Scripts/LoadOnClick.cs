using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour {

	public GameObject[] tab1; //boutons du menu
	public GameObject[] tab2; //boutons du selecteur de niveau

	public GameObject MenuElements;
	public GameObject LevelSelector;

	public GameObject[] levels;

	public GameObject[] spots;

	//ajouter une coroutine a chaque clic de bouton : fermeture du spot puis reouverture
	//trouver le moyen de mettre plus de pénombre
	//allumer les spots de niveau l'un après l'autre
	//gerer prefab d'elements de niveau ?

	public void HideButtons (int ButtonType) // remplacer les boutons par des objets IG invisibles pour empecher les pb de resize ?
	{
//		LightManagement (0);
		if (ButtonType == 0) {			
			MenuElements.SetActive (false);
			LevelSelector.SetActive (true);
			foreach (GameObject bouton in tab1)
				bouton.SetActive (false);
			if (PlayerPrefs.GetInt ("modetest") == 0) {
				int i = 0;
				Debug.Log ("test");
				while (i <= PlayerPrefs.GetInt ("progression")) {
					tab2 [i].SetActive (true);
					i++;
				}
				tab2 [3].SetActive (true);
			} else {
				foreach (GameObject bouton in tab2)
					bouton.SetActive (true);
			}
//			LightManagement (2);
		} else if (ButtonType == 1) { //si on clique sur un niveau
			LevelSelector.SetActive (false);
			foreach (GameObject bouton in tab2)
				bouton.SetActive (false);
			tab2 [3].SetActive (true);
		} else if (ButtonType == 2) { // si on clique sur retour
			if (LevelSelector.activeInHierarchy) { // dans le menu
				LevelSelector.SetActive (false);
				MenuElements.SetActive (true);
				foreach (GameObject bouton in tab1)
					bouton.SetActive (true);
				foreach (GameObject bouton in tab2)
					bouton.SetActive (false);
//				LightManagement (1);
			} else { //pendant qu'on joue
				LevelSelector.SetActive (true);
				if (PlayerPrefs.GetInt ("modetest") == 0) {
					int i = 0;
					Debug.Log ("test");
					while (i <= PlayerPrefs.GetInt ("progression")) {
						tab2 [i].SetActive (true);
						i++;
					}
					tab2 [3].SetActive (true);
				} else {
					foreach (GameObject bouton in tab2)
						bouton.SetActive (true);
				}
				foreach (GameObject level in levels)
						level.SetActive (false);
				
			}
		}
	}


			
/*	public void LightManagement(int lights){
		if (lights == 0)
			//close spot
		else 
			//open spot
			if (lights == 2)
				// allume les 3 spots dans l'ordre
	}
*/
	public void LoadScene(int level){
//		GameObject LevelElements = levels [level];
//		LevelElements.SetActive (true);
		levels [level].SetActive (true);

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
