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

	public GameObject modal;

	private IEnumerator coroutine;

//	public int progression_test = 0;

	//bool isRunning = false;

	//ajouter une coroutine a chaque clic de bouton : fermeture du spot puis reouverture -> le spot ne se rouvre que sur le menu principal
	//allumer les spots de niveau l'un après l'autre


	void Start () {
		PlayerPrefs.DeleteAll();                                // A RETIRER A TERME
//		Debug.Log(PlayerPrefs.GetInt ("progression"));
	}



	public void HideButtons (int ButtonType) // remplacer les boutons par des objets IG invisibles pour empecher les pb de resize ?
	{
//		StartCoroutine (Launcher (0));
//		StartCoroutine (SpotClose());
	//	if (isRunning) {
			Debug.Log (ButtonType);
			
			if (ButtonType == 0) {			
				MenuElements.SetActive (false);
				LevelSelector.SetActive (true);
				foreach (GameObject bouton in tab1)
					bouton.SetActive (false);
				if (PlayerPrefs.GetInt ("modetest") == 0) { // rajouter ici gestion des spots selon la progression
					int i = 0;
				Debug.Log ("Progression" + PlayerPrefs.GetInt ("progression"));
					while (i <= PlayerPrefs.GetInt ("progression")) {
						Level_Spots (i);
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
//			StartCoroutine (Launcher (1));
//			StartCoroutine(SpotOpen());   // allume aussi spots niveaux 
			} else if (ButtonType == 1) { //si on clique sur un niveau
				LevelSelector.SetActive (false);
				foreach (GameObject bouton in tab2)
					bouton.SetActive (false);
				tab2 [3].SetActive (true);
			} else if (ButtonType >= 2) { // si on clique sur retour
			if (LevelSelector.activeInHierarchy && ButtonType != 3) { // dans le menu
					LevelSelector.SetActive (false);
					MenuElements.SetActive (true);
					foreach (GameObject bouton in tab1)
						bouton.SetActive (true);
					foreach (GameObject bouton in tab2)
						bouton.SetActive (false);
//				StartCoroutine (Launcher (1));
	//			StartCoroutine (SpotOpen ());
				} else { //pendant qu'on joue
				if (ButtonType == 3)
					modal.SetActive (false);
					LevelSelector.SetActive (true);
					if (PlayerPrefs.GetInt ("modetest") == 0) {
						int i = 0;
						Debug.Log ("test");
						while (i <= PlayerPrefs.GetInt ("progression")) {
						Level_Spots (i);
							tab2 [i].SetActive (true);
							i++;
						}
						tab2 [3].SetActive (true);
					} else {
						foreach (GameObject bouton in tab2)
							bouton.SetActive (true);
					}
				Debug.Log ("mmmmh");
					foreach (GameObject level in levels)
						level.SetActive (false);
				
				}
			}
	//	}
	}

	public void Progression_Manager()
	{
		if (PlayerPrefs.GetInt ("modetest") == 0 && PlayerPrefs.GetInt ("progression") == PlayerPrefs.GetInt ("Last Played Level")) {
			PlayerPrefs.SetInt ("progression", (PlayerPrefs.GetInt ("progression") + 1));
		}
	}

	void Level_Spots(int i)
	{
		if (i == 3) {                                             // JE
			Light spot = spots[2].GetComponent<Light> ();         // CODE
			spot.color = Color.green;                             // AVEC
		} 
		if (i == 2) {
			Light spot = spots[1].GetComponent<Light> ();         // LE
			spot.color = Color.green;
			Light spot2 = spots [2].GetComponent<Light> ();       // CUL
			spot2.color = Color.blue;
		}
		if (i == 1) {
			Light spot = spots[0].GetComponent<Light> ();         // LALALA
			spot.color = Color.green;
			Light spot2 = spots [1].GetComponent<Light> ();      // LALA
			spot2.color = Color.blue;
		}
		if (i == 0) {
			Debug.Log ("ouiiii");
			Light spot = spots [0].GetComponent<Light> ();      // LEEEREEUH
			spot.color = Color.blue;
			Light spot1 = spots [1].GetComponent<Light> ();      // LEEEREEUH
			spot1.color = Color.red;
			Light spot2 = spots [2].GetComponent<Light> ();      // LEEEREEUH
			spot2.color = Color.red;
		}
	}

	IEnumerator Launcher(int i){

		if (i == 0) {
			yield return StartCoroutine (SpotClose ());
			mainLight.SetActive (false);
	//		StopCoroutine (SpotClose ());
		} else if (i == 1) {
			mainLight.SetActive (true);
			yield return StartCoroutine (SpotOpen ());
	//		StopCoroutine (SpotOpen ());
		}
			
	} 

	IEnumerator SpotClose() {
	//	if (i == 0) {
			for (float f = 1f; f >= 0.1; f -= 0.1f) {
				Light c = mainLight.GetComponent<Light> ();
				c.spotAngle = c.spotAngle * f;
			yield return new WaitForSeconds (0.03f);
			//	yield return new WaitForSeconds (0.03f);
	//		}
		//isRunning = true;
		//	mainLight.SetActive (false);
		}
	//	yield return null;
	}


//} else {
	IEnumerator SpotOpen(){
//			mainLight.SetActive (true);
			Light c = mainLight.GetComponent<Light> ();
			while (c.spotAngle < 50.0f) { 
				c.spotAngle *= 1.1f;
				yield return new WaitForSeconds (0.00005f);
			}

		}
//		yield return null;
//			Debug.Log ("huhu");

//	}


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
