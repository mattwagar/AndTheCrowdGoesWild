using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class EnableGameObjectAction : ActionNode
    {
        [StoryGraphField] public GameObject go;

        public override string MenuName { get { return "Game Object/Enable Game Object"; } }

        public override void Execute()
        {
            go.SetActive(true);
            GoToNextNode();
        }

    }
}
