using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsRightHandCloserToMarble : StoryCondition {

	public GameObject marble;
    public GameObject leftHand;
    public GameObject rightHand;


    #if UNITY_EDITOR    
    public override string MenuName {get{return "Condition/Is Right Hand Closer To Marble";}}

    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeConditionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("marble");
        AddSerializedProperty("leftHand");
        AddSerializedProperty("rightHand");
    }
    #endif

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
