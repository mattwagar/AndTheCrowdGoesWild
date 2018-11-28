using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsFlamePinchedNode : StoryCondition {

	public FlameHandEvent Flame;


    #if UNITY_EDITOR    
    public override string MenuName {get{return "Condition/Is Flame Pinched";}}

    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeConditionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("Flame", StorySerializedPropertyType.NoLabelPropertyField);
    }
    #endif

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
