using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSystemManager : MonoBehaviour {

	public GameObject HandFlameParent;
	public ParticleSystem sparkSystem;
	public ParticleSystem fireworkSystem;

	public GhostManager ghostManager;

	public AudioSource audioSource;
	public AudioSource fireworkAudioSource;
	public AudioClip fireAudio;
	public AudioClip sparklerAudio;
	public AudioClip fireworkAudio;

	private bool handFlameToggle;
	private bool isFlameAudioPlaying = true;

	public enum FireState {none, flame, sparks, firework}
	public FireState state = FireState.none;

	public float flamesVolume = 0.5f;
	public float sparksVolume = 0.2f;

	// public void ToggleHandFlame()
	// {
	// 	if(handFlameToggle)
	// 	{
	// 		HandFlameParent.SetActive(false);
	// 		audioSource.loop = true;
	// 		audioSource.clip = fireAudio;
	// 		audioSource.Play();
	// 		handFlameToggle = false;
	// 	}
	// 	else
	// 	{
	// 		HandFlameParent.SetActive(true);
	// 		audioSource.Stop();
	// 		handFlameToggle = true;
	// 	}
	// }

	void OnEnable()
	{
		state = FireState.none;
	}

	public void ActivateSparks(bool sparkToggle)
	{
		if (state == FireState.firework){
				fireworkAudioSource.Play();
		} 
		
		if(sparkToggle)
		{
			sparkSystem.Play();


			if(state != FireState.sparks)
			{
				audioSource.clip = sparklerAudio;
				audioSource.loop = true;
				audioSource.volume = sparksVolume;
				audioSource.Play();
				state = FireState.sparks;
			}


		}
		else
		{
			sparkSystem.Stop();


			if(state == FireState.sparks)
			{
				state = FireState.firework;
			} 
			else if (state != FireState.flame)
			{
				audioSource.volume = flamesVolume;
				audioSource.clip = fireAudio;
				audioSource.loop = true;
				audioSource.Play();
				state = FireState.flame;
			}
		}
	}

	public void ActivateFirework()
	{
		fireworkSystem.Play();
		// audioSource.loop = false;
		// audioSource.clip = fireworkAudio;
		// audioSource.Play();

		ghostManager.Clap();
		
	}
}
