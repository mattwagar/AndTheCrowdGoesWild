using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StoryGraph;

public class ButtonListenerNode : StoryNode
{

    public bool TurnOffOnExecute = true;
    public Button Button;


#if UNITY_EDITOR
    public override string MenuName {get{return "Listener/CanvasUI/On Button Click Listener";}}

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
            Button.onClick.AddListener(OnListener);
        }
    }

    public void OnListener()
    {
        if (TurnOffOnExecute)
        {
            Button.onClick.RemoveListener(OnListener);
        }
        GoToNextNode();
    }

    public override void DisableNode(){
        base.DisableNode();
        Button.onClick.RemoveListener(OnListener);
    }
}
