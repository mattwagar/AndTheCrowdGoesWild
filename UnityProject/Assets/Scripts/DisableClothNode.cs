using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class DisableClothNode : StoryNode
{

    public Cloth cloth;

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Disable Cloth";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("cloth", StoryDrawer.NoLabelPropertyField);
    }
    #endif


    public override void Execute()
    {
        cloth.enabled = false;
        GoToNextNode();
    }

}
