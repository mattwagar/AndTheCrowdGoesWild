using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StoryGraph
{
    public class OnScreenTouch : StoryNode
    {
        public bool TurnOffOnExecute = true;


        #if UNITY_EDITOR    
        public override string MenuName {get{return "Listener/On Screen Touch";}}

		public override void SetStyles()
        {
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeEventStyle();
        }
        public override void SetSerializedProperties()
        {    
            AddSerializedProperty("TurnOffOnExecute", "Turn Off Listener On Execute", StorySerializedPropertyType.RadioButton);
        }
        #endif

        public override void Execute()
        {
            storyGraph.StartCoroutine(OnTouch());
        }

    /// <summary>
    /// This doesn't follow the normal StoryListsenerNode Convention because there's no built in touch events. 
    /// </summary>
    /// <returns></returns>
        public IEnumerator OnTouch()
        {
            if(TurnOffOnExecute){
                while (Input.touchCount > 0)
                {
                    yield return null;
                }
                GoToNextNode();
            } else
            {
                while (true)
                {
                    if(Input.touchCount > 0) GoToNextNode(); 
                    yield return null;
                }
            }
            yield return null;
        }
    }
}
