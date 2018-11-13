using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class EnableGameObjectAction : StoryNode
{
    public GameObject go;

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Enable Game Object";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("go", StorySerializedPropertyType.NoLabelPropertyField);
    }
    #endif


    public override void Execute()
    {
        go.SetActive(true);
        GoToNextNode();
    }

}
