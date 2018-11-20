using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class RepositionGameObjectNode : StoryNode
{
    public GameObject go;
    public Transform RepositionTo;

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Reposition GameObject";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("go", StorySerializedPropertyType.NoLabelPropertyField);
        AddSerializedProperty("RepositionTo");
    }
    #endif


    public override void Execute()
    {
        go.transform.position = RepositionTo.position;
        GoToNextNode();
    }

}
