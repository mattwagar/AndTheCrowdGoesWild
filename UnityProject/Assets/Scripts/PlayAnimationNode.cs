using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class PlayAnimationNode : ActionNode
    {

        [StoryGraphField] public Animation animation;

        public override string MenuName {get{return "Action/Play Animation";}}

        public override void Execute()
        {
            animation.Play();
            GoToNextNode();
        }
    }
}
