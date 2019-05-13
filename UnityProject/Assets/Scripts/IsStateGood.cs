using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsStateGood : ConditionNode {

	[StoryGraphField] public VideoStateManager vid;

    public override string MenuName {get{return "Condition/IsStateGood";}}


    public override void Execute()
    {
        if(vid.marbleManager.musicState != MusicState.none)
        {
            GoToTrueNode();
        } 
        else
        {
            GoToFalseNode();
        }
    }
}
