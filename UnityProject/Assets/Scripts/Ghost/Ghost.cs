using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

	public Animator animator;

	void Start(){
		animator = GetComponent<Animator>();
	}
}
