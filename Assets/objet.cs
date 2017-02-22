using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objet : MonoBehaviour {
    public Vector3 correct_answer;
    public Vector3 starting_position;
    public int level;
    public int mousePressed = 0;
    public Vector3 mousePosition;
    public Vector3 diff;
    public Vector3 test;
	public Vector3 test2;

    // Use this for initialization
    void Start () {
        this.transform.eulerAngles = starting_position;
        test = Vector3.zero;
		test2 = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
		test2 = this.transform.eulerAngles;
        if (correct_answer == this.transform.eulerAngles)
            Debug.Log("bite");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("cul");
                 mousePressed = 1;
                mousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
            mousePressed = 0;
        if (mousePressed == 1)
        {
            diff = Input.mousePosition - mousePosition;
            test.x = diff.y - diff.x;
            this.transform.Rotate(test);
            mousePosition = Input.mousePosition;
        }
    }

 
    }


