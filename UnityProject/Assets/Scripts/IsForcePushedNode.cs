using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsForcePushedNode : ConditionNode
{
    [StoryGraphField] public ForcePush ForcePush;
 
    public override string MenuName {get{return "Condition/Is Force Pushed";}}


    public override void Execute()
    {
        Debug.Log("IT MADE IT HERE THREE");

        if(ForcePush.pushed)
        {
            Debug.Log("IT MADE IT HERE TOO");
            GoToTrueNode();
        } 
        else
        {
            GoToFalseNode();
        }
    }

}
