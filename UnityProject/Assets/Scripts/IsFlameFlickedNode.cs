using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsFlameFlickedNode : StoryCondition {

	public FlameHandEvent Flame;


    #if UNITY_EDITOR    
    public override string MenuName {get{return "Condition/Is Flame Flicked";}}

    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeConditionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("Flame", StoryDrawer.NoLabelPropertyField);
    }
    #endif

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
