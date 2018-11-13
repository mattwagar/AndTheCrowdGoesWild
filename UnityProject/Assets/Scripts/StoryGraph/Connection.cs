using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace StoryGraph
{
    public class Connection : ScriptableObject
    {
        public string Id;
        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;

        public bool IsDone = false;

        public StoryGraph storyGraph;



#if UNITY_EDITOR
        public Action<Connection> OnClickRemoveConnection;
        public Connection() { }

        public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint)
        {
            this.Id = "Connection_" + System.Guid.NewGuid().ToString(); ;
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickRemoveConnection = OnClickRemoveConnection;
        }

        public void Initialize(ConnectionPoint inPoint, ConnectionPoint outPoint, StoryGraph _storyGraph)
        {
            this.Id = "Connection_" + System.Guid.NewGuid().ToString(); ;
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickRemoveConnection = OnClickRemoveConnection;
            this.storyGraph =_storyGraph;
        }

        public virtual void Draw()
        {

            Handles.DrawBezier(
                inPoint.rect.center,
                outPoint.rect.center,
                inPoint.rect.center + Vector2.left * 50f,
                outPoint.rect.center - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
            {
                storyGraph.RemoveConnection(this);
            }
        }
#endif
    }
}