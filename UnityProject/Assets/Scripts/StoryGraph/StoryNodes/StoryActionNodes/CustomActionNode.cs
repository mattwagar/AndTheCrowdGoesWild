using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StoryGraph
{
    public class CustomActionNode : StoryNode
    {

        public UnityEvent onEvent;

    #if UNITY_EDITOR    
        public override string MenuName {get{return "Action/Custom Action Node";}}
        public override void SetStyles()
        {
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
        }

        public override void SetSerializedProperties()
        {    
            AddSerializedProperty("onEvent", StorySerializedPropertyType.UnityEvent);
        }
    #endif

        public override void Execute()
        {
            onEvent.Invoke();
            GoToNextNode();
        }

    }
}
