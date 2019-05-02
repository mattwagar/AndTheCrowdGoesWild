using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsRightHandCloserToMarble : ConditionNode {

	[StoryGraphField] public GameObject marble;
    [StoryGraphField] public GameObject leftHand;
    [StoryGraphField] public GameObject rightHand;

    public override string MenuName {get{return "Condition/Is Right Hand Closer To Marble";}}

    public override void Execute()
    {
        if(Vector3.Distance(rightHand.transform.position, marble.transform.position) <= Vector3.Distance(leftHand.transform.position, marble.transform.position))
        {
            GoToTrueNode();
        } 
        else
        {
            GoToFalseNode();
        }
    }
}
