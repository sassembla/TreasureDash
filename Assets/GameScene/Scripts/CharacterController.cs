﻿using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < 0) {
			var a = new int[2];
			a[2] = 1;
		}
	}
}
