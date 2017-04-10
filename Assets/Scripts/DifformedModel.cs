using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DifformedModel : MonoBehaviour {            
    public Vector3 starting_position; //valeur exacte a corriger
    public int level;
	public bool isActive;
    private Vector3 mousePosition;
	private Vector3 mouseDiff;
	public bool mousePressed = false;
	public bool shiftPressed = false;
	public bool controlPressed = false;
	public GameObject[] reference; 
	public GameObject compagnon;
	public float yaw;
	public float pitch;

	public Vector3 actualRotation; //verif facultative
	public Vector3 actualPosition;

	public GameObject modal;


	void OnEnable() {                                         // rajouter ici le reset de position
		PlayerPrefs.SetInt ("Last Played Level", level - 1); 
		actualRotation = this.transform.position;
		mousePressed = false;
	}

    void Start () { 
    }
	

	void Update () {
		//                                                         AFFICHAGE DEBUG
		actualRotation = this.transform.eulerAngles;
		actualPosition = this.transform.position;
//		Debug.Log (Quaternion.Dot (this.transform.rotation, reference[0].transform.rotation));

		//                                                         CHECK BONNE REPONSE
		if (!Input.GetMouseButton (0)) { 
			if (level == 3) {
				Debug.Log ("Player =" + (this.transform.position - compagnon.transform.position));
				Debug.Log ("Reference = " + (reference [0].transform.position - reference [1].transform.position));
				if (((Quaternion.Dot (this.transform.rotation, reference [0].transform.rotation) >= 0.95 && Quaternion.Dot (this.transform.rotation, reference [0].transform.rotation) <= 1.05)
					|| (Quaternion.Dot (this.transform.rotation, reference [0].transform.rotation) <= -0.95 && Quaternion.Dot (this.transform.rotation, reference [0].transform.rotation) >= -1.05)) 
					&& ((Quaternion.Dot (compagnon.transform.rotation, reference [1].transform.rotation) >= 0.95 && Quaternion.Dot (compagnon.transform.rotation, reference [1].transform.rotation) <= 1.05)
						|| (Quaternion.Dot (compagnon.transform.rotation, reference [1].transform.rotation) <= -0.95 && Quaternion.Dot (compagnon.transform.rotation, reference [1].transform.rotation) >= -1.05)) ) {
					Debug.Log ("FRANCE");
					if (this.transform.position.ToString("F1") == compagnon.transform.position.ToString("F1")) {   // solution temporaire pour level max
						Debug.Log ("test");
						mousePressed = false;
						modal.SetActive (true);
					}
				} 
			}
			                                //OK pour lvl 1, en comparant avec les objets referents
			// pour level 2 (trouver la rotation a transmettre a la reference pour qu'elles se suivent ? rapport entre deux axes ? comparaison avec un Dot en v2 ?? trouver l'axe a ignorer)
			// pour level 3, trouver le moyen de verifier la position des pieces l'une par rapport a l'autre (raycast ca peut marcher izi, sinon comparaison avec un rapport donne ?)
			else {
				GameObject answer = reference [0];
					if ((Quaternion.Dot (this.transform.rotation, answer.transform.rotation) >= 0.95 && Quaternion.Dot (this.transform.rotation, answer.transform.rotation) <= 1.05)
					   || (Quaternion.Dot (this.transform.rotation, answer.transform.rotation) <= -0.95 && Quaternion.Dot (this.transform.rotation, answer.transform.rotation) >= -1.05)) {
						mousePressed = false;
						Debug.Log ("Trop fort ce type");
						modal.SetActive (true);
						// joue son ?
				}
			}
		}


		//                                                             CONTROLES (apparemment, tout est OK, peut etre gerer la superposition)


		if (Input.GetMouseButtonDown(0)){ 
			if (level > 2 && isActive == false && mousePressed == false) {  //Selection de la piece voulue si il y en a deux : OK
				RaycastHit hit;
				Ray objectDetection = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast (objectDetection, out hit, Mathf.Infinity)) { 
					if (hit.collider.transform.parent.gameObject == this.gameObject) { 
						isActive = true;
						compagnon.GetComponent<DifformedModel>().isActive = false; 
					}
				}
			}
			mousePressed = true;                              // Prise en compte du clic pour les rotations simples : OK
            mousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
			mousePressed = false;

		if (level  > 1) {                                     //Prise en compte de shift pour les rotations sur un autre axe : OK
			if (Input.GetButtonDown ("Fire3")) {
				shiftPressed = true;							
				mousePosition = Input.mousePosition;
			}
			else if (Input.GetButtonUp ("Fire3"))
				shiftPressed = false;
		}

		if (level  > 2) {                                    // Prise en compte de ctrl pour les deplacements : OK
			if (Input.GetButtonDown ("Fire1")) {
				controlPressed = true;
				mousePosition = Input.mousePosition;
			}
			else if (Input.GetButtonUp ("Fire1"))
				controlPressed = false;
		}


		if (isActive == true || level < 3) {
			if (mousePressed && !controlPressed) {
				mouseDiff = Input.mousePosition - mousePosition;
				if (shiftPressed)
					pitch = (mouseDiff.y - mouseDiff.x); // Rotation sur le deuxieme axe : OK   ---> doit etre "verticale", a checker //
				else
					yaw = (mouseDiff.y - mouseDiff.x); // Rotation sur le premier axe : OK   ---> doit etre "horizontale" , a checker //
				this.transform.Rotate (yaw, pitch, 0);
				mousePosition = Input.mousePosition;
				yaw = 0f;
				pitch = 0f;
			}
			else if (mousePressed && controlPressed) { 
				mouseDiff = (Input.mousePosition - mousePosition) / 100;
				transform.position = new Vector3 (transform.position.x + mouseDiff.x, transform.position.y + mouseDiff.y, transform.position.z);  // Deplacement de la piece active


				// Limite les mouvements des pieces au champ de la camera -> a modifier pour rester dans la lumiere


				if (transform.position.x <= -2.3f)                                                                  
					transform.position = new Vector3 (-2.3f, transform.position.y, transform.position.z);
				else if (transform.position.x >= 10.0f)
					transform.position = new Vector3 (10.0f, transform.position.y, transform.position.z);
				if (transform.position.y <= -1.0f)
					transform.position = new Vector3 (transform.position.x, -1.0f, transform.position.z);
				else if (transform.position.y >= 2.3f)
					transform.position = new Vector3 (transform.position.x, 2.3f, transform.position.z); 

				mousePosition = Input.mousePosition;
			}
		}
			
    }

 
    }


