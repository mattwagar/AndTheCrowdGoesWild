using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsMarblePoppedNode : StoryNode
{
    public SphereAppear Marble;


    #if UNITY_EDITOR    
    public override string MenuName {get{return "Condition/Is Marble Popped";}}

    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeConditionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("Marble", StorySerializedPropertyType.NoLabelPropertyField);
    }
    #endif

    public override void Execute()
    {
        if(!Marble.Popped)
        {
            GoToNextNode();
        }
    }

}
