using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

namespace StoryGraph
{
    public class OnPlayableDirectorStarted : StoryNode
    {
        public bool TurnOffOnExecute = true;
        // public OnAudioStartedListener Listener;
        public PlayableDirector playableDirector;


        #if UNITY_EDITOR    
        public override string MenuName {get{return "Listener/Timeline/On Playable Director Started";}}

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
                playableDirector.played += OnListener;
            }
        }

        public void OnListener(PlayableDirector _playableDirector)
        {

            if(playableDirector.time == 0.0f)
            {
                if(TurnOffOnExecute)
                {
                    playableDirector.played -= OnListener;
                }
                GoToNextNode();
            }
        }
    }
}
