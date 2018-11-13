using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StoryGraph;

public class SetTextAction : StoryNode
{

    public Text Text;
    public string TextField;

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Set Text";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("Text");
        AddSerializedProperty("TextField", StorySerializedPropertyType.NoLabelPropertyField);
    }
    #endif


    public override void Execute()
    {
        Text.text = TextField;
        GoToNextNode();
    }

}
