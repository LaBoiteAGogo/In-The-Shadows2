using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject objet1;
    public int mousePressed = 0;
    public Vector3 mousePosition;
    public Vector3 diff;
    public Vector3 test;
    // Use this for initialization
    void Start() {
        test = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        if (mousePressed == 1) {
            diff = Input.mousePosition - mousePosition;
            test.y = diff.y - diff.x;
            objet1.transform.Rotate(test);
            mousePosition = Input.mousePosition;
        }
    }
        void OnMouseDown()
    {
            mousePressed = 1;
            mousePosition = Input.mousePosition;
        }

        void OnMouseUp()
    {
            // rotating flag
            mousePressed = 0;
        }
    } 