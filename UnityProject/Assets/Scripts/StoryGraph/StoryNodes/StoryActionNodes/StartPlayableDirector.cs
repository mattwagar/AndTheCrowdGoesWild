using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Timeline;
using UnityEngine.Playables;
using StoryGraph;

public class StartPlayableDirector : StoryNode {

	public PlayableDirector PlayableDirector;
    public string TextField;

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Start Playable Director";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("PlayableDirector");
    }
    #endif


    public override void Execute()
    {
        PlayableDirector.Play();
        GoToNextNode();
    }

}
