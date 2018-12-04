using UnityEngine;
using UnityEditor;

namespace StoryGraph{
    [CustomEditor(typeof(StoryGraph))]
    public class StoryGraphEditor : Editor
    {

        void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            StoryGraph storyGraph = (StoryGraph)target;
            if(GUILayout.Button("Build Object"))
            {
                storyGraph.BuildObject();
            }

            DrawDefaultInspector ();
        }
    }
}