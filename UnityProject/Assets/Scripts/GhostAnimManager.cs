using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimManager : MonoBehaviour {

	private GameObject[] ghosties;

	public bool excited = false;

	// Use this for initialization
	void Start () {
		ghosties = GameObject.FindGameObjectsWithTag("Ghosty");
		BeChill();
		if (excited)
		{
			GetExcited();
		}
	}

	public void GetExcited() 
	{
		foreach(GameObject g in ghosties)
		{
			float rand = Random.Range(0f,1f);
			if (rand < 0.25f)
			{
				g.GetComponent<Animator>().Play("ExciteAnim1Speed1");
			}
			else if (rand < 0.5f && rand > 0.25f)
			{
				g.GetComponent<Animator>().Play("ExciteAnim2Speed1");
			}
			else if (rand < 0.75f && rand > 0.5f)
			{
				g.GetComponent<Animator>().Play("ExciteAnim1Speed2");
			}
			else
			{
				g.GetComponent<Animator>().Play("ExciteAnim2Speed2");
			}
		}
	}

	public void BeChill()
	{
		foreach(GameObject g in ghosties)
		{
			float rand = Random.Range(0f,1f);
			if (rand < 0.25f)
			{
				g.GetComponent<Animator>().Play("IdleAnimSpeed1");
			}
			else if (rand < 0.5f && rand > 0.25f)
			{
				g.GetComponent<Animator>().Play("IdleAnimSpeed2");
			}
			else if (rand < 0.75f && rand > 0.5f)
			{
				g.GetComponent<Animator>().Play("IdleAnimSpeed3");
			}
			else
			{
				g.GetComponent<Animator>().Play("IdleAnimSpeed4");
			}
		}
	}
}
