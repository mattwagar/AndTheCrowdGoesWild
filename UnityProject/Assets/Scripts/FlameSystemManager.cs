using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSystemManager : MonoBehaviour {

	public GameObject HandFlameParent;
	public ParticleSystem sparkSystem;
	public ParticleSystem fireworkSystem;

	public AudioSource audioSource;
	public AudioClip fireAudio;
	public AudioClip sparklerAudio;
	public AudioClip fireworkAudio;

	private bool handFlameToggle;
	private bool sparkSystemToggle;
	private bool isSparkAudioPlaying = false;

	public void ToggleHandFlame()
	{
		if(handFlameToggle)
		{
			HandFlameParent.SetActive(false);
			audioSource.loop = true;
			audioSource.clip = fireAudio;
			audioSource.Play();
			handFlameToggle = false;
		}
		else
		{
			HandFlameParent.SetActive(true);
			audioSource.Stop();
			handFlameToggle = true;
		}
	}

	public void ActivateSparks(bool sparkToggle)
	{
		if(sparkToggle)
		{
			sparkSystem.Play();
			audioSource.loop = true;
			audioSource.clip = sparklerAudio;
			if(isSparkAudioPlaying == false)
			{
				audioSource.Play();
			}
			isSparkAudioPlaying = true;
			sparkSystemToggle = true;
		}
		else
		{
			sparkSystem.Stop();
			isSparkAudioPlaying = false;
			audioSource.Stop();
			sparkSystemToggle = false;
		}
	}

	public void ActivateFirework()
	{
		fireworkSystem.Play();
		audioSource.loop = false;
		audioSource.clip = fireworkAudio;
		audioSource.Play();
		
	}
}
