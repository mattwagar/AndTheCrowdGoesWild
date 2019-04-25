using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class DisableGameObjectAction : ActionNode
    {
        
        [StoryGraphField(StoryDrawer.NoLabelPropertyField)] public GameObject go;

        public override string MenuName { get { return "Game Object/Disable Game Object"; } }
        
        public override void Execute()
        {
            go.SetActive(false);
            GoToNextNode();
        }

    }
}
