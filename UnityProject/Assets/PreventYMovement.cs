using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventYMovement : MonoBehaviour {

	// Use this for initialization
	Quaternion startRotation;
	public float yPosLock = 41.77f;
	public float xRotLock;
	public float yRotLock;
	public float zRotLock;
	void Start () {

		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, yPosLock, transform.position.z);
		transform.rotation = startRotation;
	}
}
