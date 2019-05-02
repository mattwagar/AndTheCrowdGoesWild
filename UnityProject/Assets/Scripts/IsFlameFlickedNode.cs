using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsFlameFlickedNode : ConditionNode {

	[StoryGraphField] public FlameHandEvent Flame;
  
    public override string MenuName {get{return "Condition/Is Flame Flicked";}}


    public override void Execute()
    {
        if(Flame.Flicked)
        {
            GoToTrueNode();
        } 
        else
        {
            GoToFalseNode();
        }
    }
}
