using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsFlamePinchedNode : ConditionNode {

	[StoryGraphField] public FlameHandEvent Flame;

    public override string MenuName {get{return "Condition/Is Flame Pinched";}}


    public override void Execute()
    {
        if(Flame.Pinched)
        {
            GoToTrueNode();
        } 
        else
        {
            GoToFalseNode();
        }
    }
}
