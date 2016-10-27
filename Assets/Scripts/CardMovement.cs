﻿using UnityEngine;
using System.Collections;

public class CardMovement : MonoBehaviour {

	// Use this for initialization
	void Start() {
	    
	}
	
	// Update is called once per frame
	void Update() {
	    
	}

    void OnMouseDrag() {
        // Have the card follow the mouse on click
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = newPos;
    }
}
