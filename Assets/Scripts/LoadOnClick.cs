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
	public GameObject mainLight;

	private IEnumerator coroutine;

	bool isRunning = false;

	//ajouter une coroutine a chaque clic de bouton : fermeture du spot puis reouverture -> le spot ne se rouvre que sur le menu principal
	//allumer les spots de niveau l'un après l'autre


	void Start () {
		PlayerPrefs.DeleteAll();
		Debug.Log(PlayerPrefs.GetInt ("progression"));
	}



	public void HideButtons (int ButtonType) // remplacer les boutons par des objets IG invisibles pour empecher les pb de resize ?
	{
		StartCoroutine (Launcher (0));
	//	if (isRunning) {
			Debug.Log (ButtonType);
			
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
					foreach (GameObject spotlight in spots) {
						Light spot = spotlight.GetComponent<Light> ();
						spot.color = Color.green;
					}
				}
				Debug.Log ("ouveture");
/*			if (PlayerPrefs.GetInt ("progression") > 0) {
				if (PlayerPrefs.GetInt ("progression") > 2) {
					Light Spot3 = spots [3].GetComponent<Light> ();
					Spot3.color = Color.green;
				}
				Light Spot1 = spots [1].GetComponent<Light> ();

			}*/
//			StartCoroutine(Spots(2));   // allume aussi spots niveaux 
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
//				LightManager(1);
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
	//	}
	}


	IEnumerator Launcher(int i){
		yield return StartCoroutine(Spots(i));

		if (i == 0)
			mainLight.SetActive (false);			
	}

	IEnumerator Spots(int i) {
		if (i == 0) {
			for (float f = 1f; f >= 0.1; f -= 0.1f) {
				Light c = mainLight.GetComponent<Light> ();
				c.spotAngle = c.spotAngle * f;
				yield return new WaitForSeconds (0.03f);
			}
		isRunning = true;
		//	mainLight.SetActive (false);
		} else {
			mainLight.SetActive (true);
			Light c = mainLight.GetComponent<Light> ();
			while (c.spotAngle < 50.0f) { 
				Debug.Log (c.spotAngle);
				c.spotAngle *= 1.1f;
				yield return new WaitForSeconds (0.00005f);
			}

		}
		yield return null;
			Debug.Log ("huhu");

	}
	


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
