using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class StopAllCoroutines : StoryNode
{


    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Stop All Coroutines";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    #endif


    public override void Execute()
    {
        storyGraph.StopAllCoroutines();
        GoToNextNode();
    }

}
