using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoStateManager : MonoBehaviour {

	public MarbleMusicManager marbleManager;
	public VideoPlayer videoPlayer;
	public VideoClip cloudVideo;
	public VideoClip fireVideo;
	public VideoClip crystalVideo;
	public bool isPlaying = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void stopPlaying()
	{
		isPlaying = false;
	}

	public void Click()
	{
		switch(marbleManager.musicState)
		{
			case MusicState.cloud:
			videoPlayer.clip = cloudVideo;
			isPlaying = true;
			break;
			case MusicState.fire:
			videoPlayer.clip = fireVideo;
			isPlaying = true;
			break;
			case MusicState.crystal:
			videoPlayer.clip = crystalVideo;
			isPlaying = true;
			break;
		}
	}

	public VideoClip CurrentState()
	{
		switch(marbleManager.musicState)
		{
			case MusicState.cloud:
			return cloudVideo;
			case MusicState.fire:
			return cloudVideo;
			case MusicState.crystal:
			return cloudVideo;
		}

		return null;
	}
}
