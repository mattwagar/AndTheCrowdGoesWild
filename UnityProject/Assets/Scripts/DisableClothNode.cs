using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class DisableClothNode : ActionNode
{

    [StoryGraphField] public Cloth cloth;
  
    public override string MenuName {get{return "Action/Disable Cloth";}}



    public override void Execute()
    {
        cloth.enabled = false;
        GoToNextNode();
    }

}
