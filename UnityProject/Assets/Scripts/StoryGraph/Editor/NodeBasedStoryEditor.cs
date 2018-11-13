using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace StoryGraph
{
    public class NodeBasedStoryEditor : EditorWindow
    {

        private GUIStyle LockStyle = new GUIStyle();

        private bool lockedInspector = false;



        #region MODEL
        Texture2D backgroundTexture;

        private Vector2 offset;
        private Vector2 drag;

        private Color backgroundColor;
        private Color smallLineColor;
        private Color thickLineColor;

        #endregion


        #region OPEN WINDOW
        [MenuItem("Window/Story Graph Editor")]
        private static void OpenWindow()
        {
            NodeBasedStoryEditor window = GetWindow<NodeBasedStoryEditor>();
            window.titleContent = new GUIContent("Story Graph Editor");
        }
        #endregion


        private StoryGraph SelectedStoryGraph;


        private void OnEnable()
        {
            // InitStyles();
            drawLockedStyle();

            backgroundColor = new Color(0.15f, 0.15f, 0.15f);
            smallLineColor = new Color(0.25f, 0.25f, 0.25f);
            thickLineColor = new Color(0.35f, 0.35f, 0.35f);
        }

        private void ShowButton(Rect rect)
        {

            drawLockedStyle();
            Rect lockRect = new Rect(rect.x + 2, rect.y + 2, rect.width - 4, rect.height - 4);
            if (GUI.Button(lockRect, GUIContent.none, LockStyle))
            {
                Debug.Log("Locked");
                lockedInspector = !lockedInspector;
            }
        }

        private void drawLockedStyle()
        {
            if (lockedInspector)
            {
                Texture2D icon = EditorGUIUtility.Load("builtin skins/lightskin/images/in lockbutton on.png") as Texture2D;
                LockStyle.normal.background = icon;
            }
            else
            {
                Texture2D icon = EditorGUIUtility.Load("builtin skins/lightskin/images/in lockbutton.png") as Texture2D;
                LockStyle.normal.background = icon;
            }
        }

        #region DRAW LOOP
        private void OnGUI()
        {
            // InitStyles();        
            DrawBackground();

            if (IsStoryGraphSelected() == true && lockedInspector == true)
            {
                SelectedStoryGraph = Selection.gameObjects[0].GetComponent<StoryGraph>();
                drawGraph();
            }
            else if (IsStoryGraphSelected() == true && lockedInspector == false)
            {
                SelectedStoryGraph = Selection.gameObjects[0].GetComponent<StoryGraph>();
                drawGraph();
            }
            else if (IsStoryGraphSelected() == false && lockedInspector == true && SelectedStoryGraph != null)
            {
                drawGraph();
            }
            else
            {
                GUI.Label(new Rect(0, 0, position.width, position.height), "Select a GameObject in the Hierarchy with a StoryGraph Component attached to it.", StoryGraphStyles.WhiteTextHeaderStyle());
                SelectedStoryGraph = null;
            }
            Repaint();
        }

        private void drawGraph()
        {
            DrawGrid(20, 0.2f, smallLineColor);
            DrawGrid(100, 0.4f, thickLineColor);

            DrawNodes();
            DrawConnections();

            DrawConnectionLine(Event.current);

            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);
        }
        #endregion

        #region DRAWING FUNCTIONS

        private void DrawBackground()
        {
            if (backgroundTexture == null)
            {
                backgroundTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                backgroundTexture.SetPixel(0, 0, backgroundColor);
                backgroundTexture.Apply();
            }

            GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), backgroundTexture, ScaleMode.StretchToFill);
        }

        private bool IsStoryGraphSelected()
        {
            return Selection.gameObjects.Length > 0 && Selection.gameObjects[0].GetComponent<StoryGraph>() != null;
        }


        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, 0, 0) + newOffset, new Vector3(gridSpacing * i, position.height + 100, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(0, gridSpacing * j, 0) + newOffset, new Vector3(position.width + 100, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private void DrawNodes()
        {
            int index = 0;
            if (SelectedStoryGraph.Nodes != null)
            {
                for (int i = 0; i < SelectedStoryGraph.Nodes.Count; i++)
                {

                    if (ClipNode(SelectedStoryGraph.Nodes[i].rect))
                    {
                        if (index == 0)
                        {
                            SelectedStoryGraph.Nodes[i].NegativeSpace = 0;
                        }
                        else
                        {
                            SelectedStoryGraph.Nodes[i].NegativeSpace = SelectedStoryGraph.Nodes[i - 1].GetNegativeSpace();
                        }
                        SelectedStoryGraph.Nodes[i].Draw(true);
                        index++;
                    }
                    else
                    {
                        SelectedStoryGraph.Nodes[i].Draw(false);
                    }
                }
            }
        }

        private bool ClipNode(Rect rect)
        {
            float x1 = rect.x;
            float x2 = rect.x + rect.width;
            float y1 = rect.y;
            float y2 = rect.y + rect.height;
            return ((x1 > position.x - 100 && x1 < position.width) || (x2 > position.x - 100 && x2 < position.width)) && ((y1 > position.y - 500 && y1 < position.height) || (y2 > position.y - 500 && y2 < position.height));
        }

        private void DrawConnections()
        {
            if (SelectedStoryGraph.Connections != null)
            {
                for (int i = 0; i < SelectedStoryGraph.Connections.Count; i++)
                {
                    if (ClipConnection(SelectedStoryGraph.Connections[i].inPoint, SelectedStoryGraph.Connections[i].outPoint))
                        SelectedStoryGraph.Connections[i].Draw();
                }
            }
        }

        private bool ClipConnection(ConnectionPoint inPoint, ConnectionPoint outPoint)
        {
            float x1 = inPoint.rect.center.x;
            float x2 = (outPoint.rect.center - Vector2.left * 50f).x;
            float y1 = inPoint.rect.center.y;
            float y2 = (outPoint.rect.center - Vector2.left * 50f).y;
            return (x1 > position.x && x1 < position.width) || (x2 > position.x && x2 < position.width) || (y1 > position.y && y1 < position.height) || (y2 > position.y && y2 < position.height);
        }

        private void DrawConnectionLine(Event e)
        {
            ConnectionPoint selectedOutPoint = SelectedStoryGraph.SelectedOutPoint;
            ConnectionPoint selectedInPoint = SelectedStoryGraph.SelectedInPoint;
            if (selectedInPoint != null && selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    selectedInPoint.rect.center,
                    e.mousePosition,
                    selectedInPoint.rect.center + Vector2.left * 50f,
                    e.mousePosition - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }

            if (selectedOutPoint != null && selectedInPoint == null)
            {
                Handles.DrawBezier(
                    selectedOutPoint.rect.center,
                    e.mousePosition,
                    selectedOutPoint.rect.center - Vector2.left * 50f,
                    e.mousePosition + Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }
        }

        #endregion

        #region EVENT HANDLING

        private void ProcessEvents(Event e)
        {

            if (!ProcessNodeEvents(Event.current))
            {
                ProcessWindowEvents(Event.current);
            }


        }

        private void ProcessWindowEvents(Event e)
        {
            drag = Vector2.zero;

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        SelectedStoryGraph.ClearConnectionSelection();
                    }

                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
                    }
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0)
                    {
                        OnDrag(e.delta);
                    }
                    break;
            }
        }

        private bool ProcessNodeEvents(Event e)
        {
            bool eventFired = false;
            if (SelectedStoryGraph.Nodes != null)
            {
                for (int i = SelectedStoryGraph.Nodes.Count - 1; i >= 0; i--)
                {
                    bool guiChanged;
                    guiChanged = SelectedStoryGraph.Nodes[i].ProcessEvents(e);

                    if (guiChanged)
                    {
                        GUI.changed = true;
                        eventFired = true;
                    }
                }
            }
            return eventFired;
        }


        private void OnDrag(Vector2 delta)
        {
            drag = delta;

            if (SelectedStoryGraph.Nodes != null)
            {
                for (int i = 0; i < SelectedStoryGraph.Nodes.Count; i++)
                {
                    SelectedStoryGraph.Nodes[i].Drag(delta);
                }
            }

            GUI.changed = true;
        }


        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            IEnumerable<StoryNode> StoryNodeEnumberable = ReflectiveEnumeratorUtil.GetEnumerableOfType<StoryNode>();
            List<StoryNode> StoryNodes = StoryNodeEnumberable.ToList();

            for (int i = 0; i < StoryNodes.Count; i++)
            {
                genericMenu.AddItem(new GUIContent(StoryNodes[i].MenuName), false, (storyNode) => OnClickAddStoryNode(storyNode, mousePosition), StoryNodes[i]);
            }

            genericMenu.ShowAsContext();
        }

        private void OnClickAddStoryNode(System.Object _storyObject, Vector2 mousePosition)
        {
            StoryNode _storyNode = (StoryNode)_storyObject;
            string _storyNodeName = _storyNode.MenuName.Contains('/') ? _storyNode.MenuName.Substring(_storyNode.MenuName.LastIndexOf("/") + 1) : _storyNode.MenuName;

            InitializeNodes();
            var storyNode = (StoryNode)ScriptableObject.CreateInstance(_storyNode.GetType());

            storyNode.Initialize(_storyNodeName, mousePosition, 250, 90, SelectedStoryGraph);
            SelectedStoryGraph.Nodes.Add(storyNode);
        }

        private void InitializeNodes()
        {
            if (SelectedStoryGraph.Nodes == null)
            {
                SelectedStoryGraph.Nodes = new List<StoryNode>();
            }
        }


        #endregion
    }
}