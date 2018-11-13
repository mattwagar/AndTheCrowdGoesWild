using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph {
	public class NorLogicGate : StoryNode {

    #if UNITY_EDITOR    

        public override string MenuName {get{return "Logic/Nor";}}
        public override void SetStyles()
        {
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeLogicStyle();
        }
    #endif
        public override void Execute()
        {
			if(! storyGraph.PassesOrGate(this))
			{
                GoToNextNode();
			}   
        }
	}
}