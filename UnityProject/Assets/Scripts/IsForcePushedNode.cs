using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsForcePushedNode : StoryCondition
{
    public ForcePush ForcePush;


    #if UNITY_EDITOR    
    public override string MenuName {get{return "Condition/Is Force Pushed";}}

    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeConditionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("ForcePush", StorySerializedPropertyType.NoLabelPropertyField);
    }
    #endif

    public override void Execute()
    {

        if(ForcePush.pushed)
        {
            GoToTrueNode();
        } 
        else
        {
            GoToFalseNode();
        }
    }

}
