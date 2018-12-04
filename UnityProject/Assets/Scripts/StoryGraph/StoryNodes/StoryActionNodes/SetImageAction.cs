using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StoryGraph;

public class SetImageAction : StoryNode
{

    public Image Image;
    public Sprite Sprite;

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Set Image";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("Image");
        AddSerializedProperty("Sprite", StorySerializedPropertyType.NoLabelPropertyField);
    }
    #endif


    public override void Execute()
    {
        Image.sprite = Sprite;
        GoToNextNode();
    }

}
