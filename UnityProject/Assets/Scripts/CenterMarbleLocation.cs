using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterMarbleLocation : MonoBehaviour {
	
	public Transform ThumbPoint;
    public Transform IndexPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(ThumbPoint.position, IndexPoint.position, 0.5f);
	}
}
