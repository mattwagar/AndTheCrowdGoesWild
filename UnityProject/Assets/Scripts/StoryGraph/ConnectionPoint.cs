using System;
using UnityEngine;
using UnityEngine.Events;

namespace StoryGraph
{
    public enum ConnectionPointType { In, Out }

    // [System.Serializable]
    public class ConnectionPoint : ScriptableObject
    {
        public string Id;
        public string NodeId;

        public ConnectionPointType type;

        public StoryGraph storyGraph;

        #if UNITY_EDITOR        
        public Rect rect;
        public GUIStyle style;

        public void Initialize(string NodeId, ConnectionPointType type, GUIStyle style, StoryGraph _storyGraph)
        {
            this.Id = "ConnectionPoint_" + System.Guid.NewGuid().ToString();;
            this.NodeId = NodeId;
            this.type = type;
            this.style = style;
            rect = new Rect(0, 0, 10f, 20f);
            storyGraph = _storyGraph;
        }

        public void UpdateLocation(float nodeRectX, float nodeRectY, float nodeRectWidth, float nodeRectHeight)
        {
            rect.y = nodeRectY + (nodeRectHeight * 0.5f) - rect.height * 0.5f;

            switch (type)
            {
                case ConnectionPointType.In:
                    rect.x = nodeRectX - rect.width + 8f;
                    break;

                case ConnectionPointType.Out:
                    rect.x = nodeRectX + nodeRectWidth - 8f;
                    break;
            }
        }

        public void Draw()
        {
            if (GUI.Button(rect, "", style))
            {
                if (type == ConnectionPointType.In)
                {
                    storyGraph.ClickConnectionInPoint(this);
                }
                else if (type == ConnectionPointType.Out)
                {
                    storyGraph.ClickConnectionOutPoint(this);
                }
            }
        }

        #endif

        public void GoToNextNode()
        {
            if(type == ConnectionPointType.Out)
            {
                storyGraph.TraverseConnection(Id);
            }
        }
    }
}