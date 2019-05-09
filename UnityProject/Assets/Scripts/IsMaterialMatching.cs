using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsMaterialMatching : ConditionNode
{
    [StoryGraphField] public SkinnedMeshRenderer handRenderer;
    [StoryGraphField] public Material material;
 
    public override string MenuName {get{return "Condition/Is Material Matching";}}


    public override void Execute()
    {

        if(handRenderer.material == material)
        {
            GoToTrueNode();
        } 
        else
        {
            GoToFalseNode();
        }
    }

}
