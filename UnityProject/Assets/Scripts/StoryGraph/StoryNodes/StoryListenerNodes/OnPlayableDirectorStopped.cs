using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

namespace StoryGraph
{
    public class OnPlayableDirectorStopped : StoryNode
    {
        public bool TurnOffOnExecute = true;
        // public OnAudioStoppedListener Listener;
        public PlayableDirector playableDirector;


        #if UNITY_EDITOR    
        public override string MenuName {get{return "Listener/Timeline/On Playable Director Stopped";}}

		public override void SetStyles()
        {
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeEventStyle();
        }
        public override void SetSerializedProperties()
        {    
            AddSerializedProperty("TurnOffOnExecute", "Turn Off Listener On Execute", StorySerializedPropertyType.RadioButton);
            AddSerializedProperty("playableDirector", StorySerializedPropertyType.NoLabelPropertyField);
        }
        #endif

        public override void Execute()
        {
            Debug.Log(Id + " is Initialized");
            if(playableDirector != null)
            {
                playableDirector.stopped += OnListener;
            }
        }

        public void OnListener(PlayableDirector _playableDirector)
        {

            if(TurnOffOnExecute)
            {
                playableDirector.stopped -= OnListener;
            }
            GoToNextNode();
        }
    }
}
