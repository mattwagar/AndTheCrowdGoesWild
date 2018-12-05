using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StoryGraph
{
    public class StoryGraph : MonoBehaviour
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


        private StoryNode selectedNode;
        public StoryNode SelectedNode
        {
            get { return selectedNode; }
            set { selectedNode = value; }
        }

        // public string currentLoopId;


        void OnEnable()
        {
            // currentLoopId = "Loop_"+ Time.time +System.Guid.NewGuid().ToString();
            StartNodeGraph();
        }

        void OnDisable()
        {
            setAllNodesAsleep();
        }

        public void setAllNodesAsleep()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].DisableNode();
            }
        }

        public void StartNodeGraph()
        {

            List<StoryNode> storyNodes = new List<StoryNode>();
            for (int i = 0; i < Nodes.Count; i++)
            {
                storyNodes.Add(Nodes[i]);
                for (int j = 0; j < Connections.Count; j++)
                {
                    if (Nodes[i].inPoint.Id == Connections[j].inPoint.Id)
                    {
                        storyNodes.Remove(Nodes[i]);
                    }
                }
            }

            for (int i = 0; i < storyNodes.Count; i++)
            {
                storyNodes[i].WakeUpNode("start");
            }
        }

        // public void updateLoopId(){
        //     currentLoopId = "Loop_"+ Time.time +System.Guid.NewGuid().ToString();
        // }

#if UNITY_EDITOR

        // public Vector2 Scale = new Vector2(1,1);
        public float Zoom = 1.0f;

        public void SetConnectionsSelected(StoryNode storyNode, bool _isSelected)
        {

            string inPointId = storyNode.inPoint.Id;
            string outPointId = storyNode.outPoint.Id;



            if (Connections != null && Connections.Count > 0)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    if (selectedNode == null)
                    {
                        break;
                    }
                    //If you are picking the node then all its connections are selected
                    if (storyNode.Id == selectedNode.Id && _isSelected == true)
                    {
                        Connections[i].isSelected = false;

                        if (Connections[i].inPoint.Id == inPointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                        if (Connections[i].outPoint.Id == outPointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                    }
                    //If you are picking the node, and this one is set to be not selected (should never happen)
                    else if (storyNode.Id == selectedNode.Id && _isSelected == false)
                    {
                        Connections[i].isSelected = false;
                    }
                }
            }
        }

        public void SetConnectionsSelected(StoryCondition conditionNode, bool _isSelected)
        {

            string inPointId = conditionNode.inPoint.Id;
            string outPointId = conditionNode.outPoint.Id;
            string elsePointId = conditionNode.elseOutPoint.Id;

            if (Connections != null && Connections.Count > 0)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    if (selectedNode == null)
                    {
                        break;
                    }
                    //If you are picking the node then all its connections are selected
                    if (conditionNode.Id == selectedNode.Id && _isSelected == true)
                    {
                        Connections[i].isSelected = false;

                        if (Connections[i].inPoint.Id == inPointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                        if (Connections[i].outPoint.Id == outPointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                        if (Connections[i].outPoint.Id == elsePointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                    }
                    //If you are picking the node, and this one is set to be not selected (should never happen)
                    else if (conditionNode.Id == selectedNode.Id && _isSelected == false)
                    {
                        Connections[i].isSelected = false;
                    }
                }
            }
        }

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

        public void RemoveNode(StoryCondition node)
        {
            if (node != null && Connections != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();

                for (int i = 0; i < Connections.Count; i++)
                {
                    if (Connections[i].inPoint == node.inPoint || Connections[i].outPoint == node.outPoint || Connections[i].outPoint == node.elseOutPoint)
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
            Connection connection = (Connection)ScriptableObject.CreateInstance(typeof(Connection));

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

        public void BuildObject()
        {

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

        public void TraverseConnection(string outPointId, string loopId)
        {
            for (int i = 0; i < Connections.Count; i++) //for each connection
            {
                if (Connections[i].outPoint != null && Connections[i].outPoint.Id == outPointId) //if that connection's outPoint matches the ConnectionPoint Id
                {
                    Connections[i].IsDone = true;

                    ExecuteNode(Connections[i].inPoint.NodeId, loopId);
                }
            }
        }

        public void ExecuteNode(string NodeId, string loopId)
        {
            StoryNode storyNode = GetNodeById(NodeId);
            if (storyNode != null)
            {
                storyNode.WakeUpNode(loopId);
            }
        }

        public bool PassesOrGate(OrLogicGate logicNode)
        {
            string inPointId = logicNode.inPoint.Id;

            int sameTimesVisited = -1;

            for (int i = 0; i < Connections.Count; i++)
            {
                if (Connections[i].inPoint.Id == inPointId)
                {
                    StoryNode node = GetNodeById(Connections[i].outPoint.NodeId);

                    switch (logicNode.orState)
                    {
                        case OrLogicGate.OrState.IsDone:
                            if (node.storyNodeState == StoryNodeState.IsDone) return true;
                            break;
                        case OrLogicGate.OrState.IsDoneMoreThanOnce:
                            if (node.storyNodeState == StoryNodeState.IsDone && node.timesVisited > 1) return true;
                            break;
                        case OrLogicGate.OrState.IsDoneMoreThanOrEqualToOnce:
                            if (node.storyNodeState == StoryNodeState.IsDone && node.timesVisited >= 1) return true;
                            break;
                        case OrLogicGate.OrState.IsAwake:
                            if (node.storyNodeState == StoryNodeState.IsAwake) return true;
                            break;
                        case OrLogicGate.OrState.IsAsleep:
                            if (node.storyNodeState == StoryNodeState.IsAsleep) return true;
                            break;
                    }
                }
            }
            return false;
        }

        public bool PassesAndGate(AndLogicGate logicNode)
        {
            string inPointId = logicNode.inPoint.Id;

            bool isAnd = true;

            int sameTimesVisited = -1;
            string loopId;

            //for each connection that leads to the And gate
            for (int i = 0; i < Connections.Count; i++)
            {
                if (Connections[i].inPoint.Id == inPointId)
                {
                    StoryNode node = GetNodeById(Connections[i].outPoint.NodeId);

                    switch (logicNode.andState)
                    {
                        case AndLogicGate.AndState.IsDone:
                            if (node.storyNodeState != StoryNodeState.IsDone) isAnd = false;
                            break;
                        case AndLogicGate.AndState.IsDoneSameNumberOfTimes:
                            if (sameTimesVisited == node.timesVisited) isAnd = true;

                            if (sameTimesVisited == -1)
                            {
                                sameTimesVisited = node.timesVisited; 
                                isAnd = false; 
                                break;
                            }
                            if (node.storyNodeState != StoryNodeState.IsDone) isAnd = false;
                            break;
                        case AndLogicGate.AndState.IsDoneInSameLoop:
                            if(logicNode.LoopId != node.LoopId) isAnd = false;

                            if (node.storyNodeState != StoryNodeState.IsDone) isAnd = false;
                            break;
                        case AndLogicGate.AndState.IsAwake:
                            if (node.storyNodeState != StoryNodeState.IsAwake) isAnd = false;
                            break;
                        case AndLogicGate.AndState.IsAsleep:
                            if (node.storyNodeState != StoryNodeState.IsAsleep) isAnd = false;
                            break;
                    }
                }
            }
            return isAnd;
        }




    }
}