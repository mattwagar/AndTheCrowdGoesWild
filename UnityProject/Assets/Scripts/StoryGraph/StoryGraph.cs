using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StoryGraph
{
    public class StoryGraph: MonoBehaviour
    {
         public List<StoryNode> Nodes;

        public int NodesCount
        {
            get { return Nodes.Count; }
        }

        public List<Connection> Connections;


        public Dictionary<string, GUIStyle> Styles;

        private ConnectionPoint selectedInPoint;
        public ConnectionPoint SelectedInPoint
        {
            get { return selectedInPoint; }
            set { selectedInPoint = value; }
        }
        private ConnectionPoint selectedOutPoint;
        public ConnectionPoint SelectedOutPoint
        {
            get { return selectedOutPoint; }
            set { selectedOutPoint = value; }
        }


        void OnEnable()
        {
            StartNodeGraph();
        }

        void OnDisable(){
            for(int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].DisableNode();
            }
        }

        void StartNodeGraph()
        {
            
            List<StoryNode> storyNodes = new List<StoryNode>();
            for (int i = 0; i < Nodes.Count; i++)
            {
                storyNodes.Add(Nodes[i]);
                for (int j = 0; j < Connections.Count; j++)
                {
                    if(Nodes[i].inPoint.Id == Connections[j].inPoint.Id)
                    {
                        storyNodes.Remove(Nodes[i]);
                    }
                }
            }

            for(int i = 0; i < storyNodes.Count; i++)
            {
                storyNodes[i].WakeUpNode();
                storyNodes[i].Execute();
            }
        }

        #if UNITY_EDITOR

        public void RemoveNode(StoryNode node)
        {
            if (node != null && Connections != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();

                for (int i = 0; i < Connections.Count; i++)
                {
                    if (Connections[i].inPoint == node.inPoint || Connections[i].outPoint == node.outPoint)
                    {
                        connectionsToRemove.Add(Connections[i]);
                    }
                }

                for (int i = 0; i < connectionsToRemove.Count; i++)
                {
                    Connections.Remove(connectionsToRemove[i]);
                }

                connectionsToRemove = null;
            }
            Nodes.Remove(node);
        }

        public void RemoveConnection(Connection connection)
        {
            Connections.Remove(connection);

        }

        public void ClickConnectionInPoint(ConnectionPoint inPoint)
        {

            selectedInPoint = inPoint;

            if (selectedOutPoint != null)
            {
                if (selectedOutPoint.NodeId != selectedInPoint.NodeId)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        public void ClickConnectionOutPoint(ConnectionPoint outPoint)
        {

            selectedOutPoint = outPoint;

            if (selectedInPoint != null)
            {
                if (selectedOutPoint.NodeId != selectedInPoint.NodeId)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        public void CreateConnection()
        {
            Connection connection = (Connection) ScriptableObject.CreateInstance(typeof(Connection));

            connection.Initialize(selectedInPoint, selectedOutPoint, this);

            if (Connections == null)
            {
                Connections = new List<Connection>();
            }

            Connections.Add(connection);

        }

        public void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }

        #endif

        public StoryNode GetNodeById(string ConnectionId)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Id == ConnectionId)
                {
                    return Nodes[i];
                }
            }
            return null;
        }

        public Connection GetConnectionById(string ConnectionId)
        {
            for (int i = 0; i < Connections.Count; i++)
            {
                if (Connections[i].Id == ConnectionId)
                {
                    return Connections[i];
                }
            }
            return null;
        }

        public void TraverseConnection(string outPointId)
        {
            for (int i = 0; i < Connections.Count; i++) //for each connection
            {
                if (Connections[i].outPoint != null && Connections[i].outPoint.Id == outPointId) //if that connection's outPoint matches the ConnectionPoint Id
                {
                    Connections[i].IsDone = true;

                    ExecuteNode(Connections[i].inPoint.NodeId);
                }
            }
        }

        public void ExecuteNode(string NodeId)
        {
            StoryNode storyNode = GetNodeById(NodeId);
            if(storyNode != null)
            {
                storyNode.WakeUpNode();
                storyNode.Execute();
            }
        }

        public bool PassesOrGate(StoryNode storyNode)
        {
            string inPointId = storyNode.inPoint.Id;

            for(int i = 0; i < Connections.Count; i++)
            {
                if(Connections[i].inPoint.Id == inPointId && Connections[i].IsDone == true)
                {
                    return true;
                }
            }
            return false;
        }

        public bool PassesAndGate(StoryNode storyNode)
        {
            string inPointId = storyNode.inPoint.Id;

            bool isAnd = true;

            for(int i = 0; i < Connections.Count; i++)
            {
                if(Connections[i].inPoint.Id == inPointId && Connections[i].IsDone != true)
                {
                    isAnd = false;
                }
            }
            return isAnd;
        }

    }
}