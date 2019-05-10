using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using StoryGraph;

public class WaitForSecondsVideoNode : CoroutineNode
{
    [StoryGraphField] public VideoStateManager videoState;

    public override string MenuName { get { return "Wait For Video Seconds"; } }

    public override void Execute()
    {
        storyGraph.StartCoroutine(WaitForSeconds());
    }
    public IEnumerator WaitForSeconds()
    {
        VideoClip video = videoState.CurrentState();
        yield return new WaitForSeconds((float)video.length);
        GoToNextNode();
    }
}




