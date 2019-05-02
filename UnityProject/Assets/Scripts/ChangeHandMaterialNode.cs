using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class ChangeHandMaterialNode : ActionNode
{
    [StoryGraphField] public SkinnedMeshRenderer handRenderer;
    [StoryGraphField] public Material material;

  
    public override string MenuName {get{return "Action/Change Hand Material";}}


    public override void Execute()
    {
        handRenderer.material = material;
        GoToNextNode();
    }

}
