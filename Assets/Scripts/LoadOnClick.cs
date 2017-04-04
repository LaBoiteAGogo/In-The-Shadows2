using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {


	public void LoadScene(int level){
		Application.LoadLevel (level);

	}

	public void ModeChanger(int tester_mode){
		if (tester_mode == 0) {
			if (PlayerPrefs.HasKey ("progression") == false)
				PlayerPrefs.SetInt ("progression", 0);
			PlayerPrefs.SetInt ("modetest", 0);
		}
		else if (tester_mode == 1)
			PlayerPrefs.SetInt ("modetest", 1);
		PlayerPrefs.Save ();
	}
}
