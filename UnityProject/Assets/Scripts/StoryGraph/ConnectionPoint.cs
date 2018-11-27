using System;
using System.Collections;
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
        float nodeCount = 1;  
        float nodeIndex = 1;  
        string label = "";
        public Rect rect;
        public GUIStyle style;
        bool appendBottom = false;

        public void Initialize(string NodeId, ConnectionPointType type, GUIStyle style, StoryGraph _storyGraph, bool _appendBottom = false, float _nodeIndex = 1f, float _nodeCount = 1f)
        {
            this.Id = "ConnectionPoint_" + System.Guid.NewGuid().ToString();;
            this.NodeId = NodeId;
            this.type = type;
            this.style = style;
            rect = new Rect(0, 0, 10f, 20f);
            storyGraph = _storyGraph;
            appendBottom = _appendBottom;
            nodeIndex = _nodeIndex;
            // nodeCount = _nodeCount;
        }

        public void setAppendBottom(bool _appendBottom){appendBottom = _appendBottom;}
        public void setNodeIndex(float _nodeIndex){nodeIndex = _nodeIndex;}

        public void UpdateLocation(float nodeRectX, float nodeRectY, float nodeRectWidth, float nodeRectHeight)
        {
            if(appendBottom){
                rect.y = nodeRectY + nodeRectHeight - rect.height - 12;
            }else{
                // rect.y = nodeRectY + (nodeRectHeight * (nodeIndex/(nodeCount+1)) - rect.height * 0.5f);
                rect.y = nodeRectY + 22 + (nodeIndex*28);
            }

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

        public void GoToNextNode(string loopId)
        {
            if(type == ConnectionPointType.Out)
            {
                storyGraph.TraverseConnection(Id, loopId);
            }
        }
    }
}