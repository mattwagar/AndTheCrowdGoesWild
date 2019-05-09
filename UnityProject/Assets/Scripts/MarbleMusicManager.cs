using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicState {none, crystal, cloud, fire}
public class MarbleMusicManager : MonoBehaviour
{

    public MusicState musicState = MusicState.none;

    public AudioClip happyMusic;
    public AudioClip excitedMusic;
    public AudioClip terrifiedMusic;

    public AudioSource audioSource;
    public float transitionSpeed = 0.5f;
    public float countdown = 0;
    public float currentVolume = 0;

    private bool isPlaying = false;

    private IEnumerator RaiseVolume()
    {
        currentVolume = 0;
        while(currentVolume < 1)
        {
            currentVolume += Time.deltaTime * transitionSpeed;
            audioSource.volume = currentVolume;
            yield return null;
        }
        currentVolume = 1;
    }

    private IEnumerator LowerVolume()
    {
        currentVolume = 1;
        while(currentVolume > 0)
        {
            currentVolume -= Time.deltaTime * transitionSpeed;
            audioSource.volume -= currentVolume;
            yield return null;
        }
        currentVolume = 0;
    }

    private IEnumerator StateLoop()
    {
        while(true)
        {
            yield return new WaitUntil(() => isPlaying);
            StartCoroutine(RaiseVolume());
            countdown = 10;
            while(countdown != 0)
            {
                countdown -= Time.deltaTime;
                yield return null;
            }
            isPlaying = false;
            yield return StartCoroutine(LowerVolume());
        }
    }

    private IEnumerator ChangeStateRoutine(MusicState newState)
    {
        if(isPlaying)
            yield return StartCoroutine(LowerVolume());
        
        switch (newState)
        {
            case MusicState.crystal:
                audioSource.clip = happyMusic;
                break;
            case MusicState.fire:
                audioSource.clip = excitedMusic;
                break;
            case MusicState.cloud:
                audioSource.clip = terrifiedMusic;
                break;
            case MusicState.none:
                audioSource.clip = null;
                break;
        }
    }

    private void Start() 
    {
        StartCoroutine(StateLoop());
    }

    public void ChangeState(MusicState newState)
    {
        musicState = newState;
        StartCoroutine(ChangeStateRoutine(newState));
    }

    public void PlayMusic()
    {
        isPlaying = true;
        countdown = 10;
    }

}