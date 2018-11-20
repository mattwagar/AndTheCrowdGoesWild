using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using StoryGraph;

public class OnInteractionButtonClickNode : StoryNode
{

    public bool TurnOffOnExecute = true;
    public InteractionButton Button;


#if UNITY_EDITOR
    public override string MenuName {get{return "Listener/LeapMotion/On Leap Button Click";}}

    public override void SetStyles()
    {
        base.SetStyles();
        nodeHeaderStyle = StoryGraphStyles.NodeEventStyle();
    }
    public override void SetSerializedProperties()
    {
        AddSerializedProperty("TurnOffOnExecute", "Turn Off Listener On Execute", StorySerializedPropertyType.RadioButton);
        AddSerializedProperty("Button");
    }
#endif

    public override void Execute()
    {
        Debug.Log(Id + " is Initialized");
        if (Button != null)
        {
            Button.OnPress += OnListener;
        }
    }

    public void OnListener()
    {
        if (TurnOffOnExecute)
        {
            Button.OnPress -= OnListener;
        }
        GoToNextNode();
    }

    public override void DisableNode(){
        base.DisableNode();
        Button.OnPress -= OnListener;
    }
}
