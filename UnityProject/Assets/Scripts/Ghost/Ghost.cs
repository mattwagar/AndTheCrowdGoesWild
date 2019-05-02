using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

	[HideInInspector]
	public Animator animator;
	public Material material;

	void Start(){
		animator = GetComponent<Animator>();
		material = GetComponent<Material>();
	}
}
