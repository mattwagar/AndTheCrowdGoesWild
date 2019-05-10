using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventYMovement : MonoBehaviour {

	// Use this for initialization
	Quaternion startRotation;
	Quaternion childStartRotation;
	float childStartYAxis;
	public Transform ChildShadow;
	public float yPosLock = 41.77f;
	public float xRotLock;
	public float yRotLock;
	public float zRotLock;
	void Start () {

		startRotation = transform.rotation;
		childStartRotation = ChildShadow.rotation;
		childStartYAxis = ChildShadow.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, yPosLock, transform.position.z);
		transform.rotation = startRotation;

		ChildShadow.position = new Vector3(ChildShadow.position.x, childStartYAxis, ChildShadow.position.z);
		ChildShadow.rotation = childStartRotation;
	}
}
