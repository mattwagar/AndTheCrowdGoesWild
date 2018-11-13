using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

namespace StoryGraph
{
    public class OnPlayableDirectorFinished : StoryNode
    {
        public bool TurnOffOnExecute = true;
        // public OnAudioFinishedListener Listener;
        public PlayableDirector playableDirector;


        #if UNITY_EDITOR    
        public override string MenuName {get{return "Listener/Timeline/On Playable Director Finished";}}

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
            storyGraph.StartCoroutine(AudioFinished());
        }


        public IEnumerator AudioFinished()
        {
            if(TurnOffOnExecute){
                while (playableDirector.time != playableDirector.duration)
                {
                    yield return null;
                }
                GoToNextNode();
            } 
            // else this would run on every frame... which we don't want.
            // {
            //     while (true)
            //     {
            //         if(playableDirector.time != playableDirector.duration) GoToNextNode(); 
            //         yield return null;
            //     }
            // }
            yield return null;
        }
    }
}
