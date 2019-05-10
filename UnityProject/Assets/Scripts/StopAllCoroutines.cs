using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class StopAllCoroutines : ActionNode
    {


        public override string MenuName {get{return "Action/StopAllCoroutines";}}

        public override void Execute()
        {
            StopAllCoroutines();
            GoToNextNode();
        }
    }
}
