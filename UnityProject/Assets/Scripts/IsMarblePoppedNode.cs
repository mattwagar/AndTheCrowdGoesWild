using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsMarblePoppedNode : ConditionNode
{
    [StoryGraphField] public PopMarbleEvent Marble;
 
    public override string MenuName {get{return "Condition/Is Marble Popped";}}


    public override void Execute()
    {
        if(Marble.Popped)
        {
            GoToTrueNode();
        } 
        else
        {
            GoToFalseNode();
        }
    }

}
