using System.Collections;
using System.Collections.Generic;
using StoryGraph;
using UnityEngine;

public class DisableTwoGameObjectsNode : StoryNode {
    
	public GameObject go;
	public GameObject go2;

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Disable Two Game Object";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("go", StorySerializedPropertyType.NoLabelPropertyField);
        AddSerializedProperty("go2", StorySerializedPropertyType.PropertyField);
    }
    #endif


    public override void Execute()
    {
        go.SetActive(false);
        go2.SetActive(false);
        GoToNextNode();
    }

}
