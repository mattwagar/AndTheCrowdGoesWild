using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StoryGraph
{
    public class GenericStoryListenerNode : StoryNode
    {
        public bool TurnOffOnExecute = true;
        public StoryListener Listener;


        #if UNITY_EDITOR    
        public override string MenuName {get{return "Listener/Generic Listener";}}

		public override void SetStyles()
        {
            
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeEventStyle();
        }
        public override void SetSerializedProperties()
        {    
            AddSerializedProperty("TurnOffOnExecute", "Turn Off Listener On Execute", StorySerializedPropertyType.RadioButton);
            AddSerializedProperty("Listener");
        }
        #endif

        public override void Execute()
        {
            Debug.Log(Id + " is Initialized");
            if(Listener != null)
            {
                Listener.StoryListenerAction += OnListener;
                Listener.IsListenerSet = true;
            }
        }

        public void OnListener()
        {
            if(TurnOffOnExecute)
            {
                Listener.StoryListenerAction -= OnListener;
            }
            GoToNextNode();
        }

        public override void DisableNode(){
            base.DisableNode();
            Listener.StoryListenerAction -= OnListener;
        }
    }
}
