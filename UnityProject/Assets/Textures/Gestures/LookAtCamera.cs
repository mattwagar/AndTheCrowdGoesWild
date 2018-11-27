using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	private Transform camera;

	void Start () {
		camera = Camera.main.transform;
	}
	
	void Update () {
		 transform.LookAt(camera);
	}
}
