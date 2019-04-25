using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class IsGameObjectEnabled : StoryNode
    {
        public GameObject go;


#if UNITY_EDITOR
        public override string MenuName { get { return "Condition/Is GameObject Enabled"; } }

        public override void SetStyles()
        {
            base.SetStyles();
            nodeHeaderStyle = StoryGraphStyles.NodeConditionStyle();
        }
        public override void SetSerializedProperties()
        {
            AddSerializedProperty("go", StoryDrawer.NoLabelPropertyField);
        }
#endif

        public override void Execute()
        {
            if (go.activeSelf)
            {
                GoToNextNode();
            }
        }

    }
}