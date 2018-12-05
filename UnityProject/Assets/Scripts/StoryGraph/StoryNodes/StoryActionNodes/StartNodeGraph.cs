using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class StartNodeGraph : StoryNode
{

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Restart Story Graph";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    #endif


    public override void Execute()
    {
		storyGraph.StartNodeGraph();
        GoToNextNode();
    }

}
