using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSystemManager : MonoBehaviour {

	public GameObject HandFlameParent;
	public ParticleSystem sparkSystem;
	public ParticleSystem fireworkSystem;

	private bool handFlameToggle;
	private bool sparkSystemToggle;

	public void ToggleHandFlame()
	{
		if(handFlameToggle)
		{
			HandFlameParent.SetActive(false);
			handFlameToggle = false;
		}
		else
		{
			HandFlameParent.SetActive(true);
			handFlameToggle = true;
		}
	}

	public void ToggleSparks()
	{
		if(sparkSystemToggle)
		{
			sparkSystem.Stop();
			sparkSystemToggle = false;
		}
		else
		{
			sparkSystem.Play();
			sparkSystemToggle = true;
		}
	}

	public void ActivateFirework()
	{
		fireworkSystem.Emit(1);
	}
}
