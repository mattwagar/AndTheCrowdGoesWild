using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class PlayAnimationNode : StoryNode
    {

        public Animation animation;

        #if UNITY_EDITOR    
        public override string MenuName {get{return "Action/Play Animation";}}
        public override void SetStyles()
        {
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
        }
        public override void SetSerializedProperties()
        {    
            AddSerializedProperty("animation", StoryDrawer.NoLabelPropertyField);
        }
        #endif


        public override void Execute()
        {
            animation.Play();
            GoToNextNode();
        }
    }
}
