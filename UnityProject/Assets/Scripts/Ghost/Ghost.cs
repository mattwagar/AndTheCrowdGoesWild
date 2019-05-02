using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

	[HideInInspector]
	public Animator animator;
	public SkinnedMeshRenderer skinnedMeshRenderer;	
	[HideInInspector]
	public Material material;

	void Start(){
		animator = GetComponent<Animator>();
		material = skinnedMeshRenderer.materials[0];
	}
}
