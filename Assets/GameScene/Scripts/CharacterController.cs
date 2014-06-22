using UnityEngine;
using System.Collections;


public class CharacterController : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		(0 < transform.position.y).Assert("character dropped!! :" + transform.position.y);
	}

}

