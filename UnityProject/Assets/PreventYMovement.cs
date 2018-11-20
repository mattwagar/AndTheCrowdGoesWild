using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventYMovement : MonoBehaviour {

	// Use this for initialization
	Transform cloudTransform;
	public float yPosLock = 41.77f;
	void Start () {
		cloudTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		cloudTransform.position = new Vector3(cloudTransform.position.x, yPosLock, cloudTransform.position.z);
	}
}
