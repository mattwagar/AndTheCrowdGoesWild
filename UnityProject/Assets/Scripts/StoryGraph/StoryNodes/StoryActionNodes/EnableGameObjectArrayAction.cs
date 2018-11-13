using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class EnableGameObjectArrayAction : StoryNode
{

    public GameObject[] gos;

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Enable Game Object Array";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("gos", StorySerializedPropertyType.Array);
    }
    #endif


    public override void Execute()
    {
        for(int i = 0; i < gos.Length; i++){
            gos[i].SetActive(true);
        }
        GoToNextNode();
    }

}
