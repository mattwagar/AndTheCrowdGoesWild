using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class TriggerStoryListenerNode : StoryNode
{
    public StoryListener listener;

    #if UNITY_EDITOR    
    public override string MenuName {get{return "Action/Trigger Story Listener";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("listener", StorySerializedPropertyType.NoLabelPropertyField);
    }
    #endif


    public override void Execute()
    {
        listener.StoryListenerAction.Invoke();
        GoToNextNode();
    }

}
