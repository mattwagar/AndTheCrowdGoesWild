using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsVideoPlaying : ConditionNode {

	[StoryGraphField] public VideoStateManager vid;

    public override string MenuName {get{return "Condition/Is Video Playing";}}


    public override void Execute()
    {
        if(vid.isPlaying)
        {
            GoToTrueNode();
        } 
        else
        {
            GoToFalseNode();
        }
    }
}
