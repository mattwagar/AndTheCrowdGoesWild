using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Events;


#if UNITY_EDITOR
using UnityEditor;
#endif


namespace StoryGraph
{

    public enum StorySerializedPropertyType { PropertyField, UnityEvent, Array, NoLabelPropertyField, RadioButton, NoLabelReadOnly, IntegerField }
    public enum StoryNodeState { IsAsleep, IsAwake, IsDone }
    // [Serializable]
    // public class StoryNodeEvent : UnityEvent <StoryNode> {}


    // [System.Serializable]
    public class StoryNode : ScriptableObject, IComparable<StoryNode>
    {
        public virtual string MenuName { get { return "StoryNode"; } }

        public string Id;
        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;
        public StoryNodeState storyNodeState = StoryNodeState.IsAsleep;

        public StoryGraph storyGraph;

#if UNITY_EDITOR
        public Rect originalRect;
        public Rect rect;
        public string title;
        public float Spacing;
        public float NegativeSpace = 0;
        public bool isDragged;
        public bool isSelected;

        public GUIStyle style;
        public GUIStyle defaultNodeStyle;
        public GUIStyle selectedNodeStyle;
        public GUIStyle closeButtonStyle;
        public GUIStyle nodeHeaderStyle;
        public GUIStyle whiteTextStyle;
        public GUIStyle whiteTextHeaderStyle;
        public GUIStyle isAsleepStyle;
        public GUIStyle isAwakeStyle;
        public GUIStyle isDoneStyle;
        public GUIStyle whiteBoldTextStyle;
        public GUIStyle whiteVarNodeStyle;

        public SerializedObject serializedObject;

        public List<SerializedProperty> SerializedProperties;
        public List<string> Labels;
        // public List<bool> IsObjectField;
        public List<StorySerializedPropertyType> StorySerializedPropertyTypes;


        public StoryNode() { }
        public virtual void Initialize(string _title, Vector2 position, float width, float height, StoryGraph _storyGraph)
        {
            storyGraph = _storyGraph;

            Id = "Node_" + System.Guid.NewGuid().ToString();
            inPoint = (ConnectionPoint)ScriptableObject.CreateInstance(typeof(ConnectionPoint));
            inPoint.Initialize(Id, ConnectionPointType.In, StoryGraphStyles.InPointStyle(), storyGraph);

            outPoint = (ConnectionPoint)ScriptableObject.CreateInstance(typeof(ConnectionPoint));
            outPoint.Initialize(Id, ConnectionPointType.Out, StoryGraphStyles.OutPointStyle(), storyGraph);

            title = _title;
            rect = new Rect(position.x, position.y, width, height);
            originalRect = new Rect(position.x, position.y, width, height);
            SetStyles();

            InitializeSerializedProperties();
        }


        public virtual void SetStyles()
        {
            style = StoryGraphStyles.NodeStyle();
            defaultNodeStyle = StoryGraphStyles.NodeStyle();
            selectedNodeStyle = StoryGraphStyles.SelectedNodeStyle();
            closeButtonStyle = StoryGraphStyles.CloseButtonStyle();
            nodeHeaderStyle = StoryGraphStyles.NodeHeaderStyle();
            whiteTextHeaderStyle = StoryGraphStyles.WhiteTextHeaderStyle();
            whiteTextStyle = StoryGraphStyles.WhiteTextStyle();
            isAsleepStyle = StoryGraphStyles.IsAsleepStyle();
            isAwakeStyle = StoryGraphStyles.IsAwakeStyle();
            isDoneStyle = StoryGraphStyles.IsDoneStyle();
            whiteBoldTextStyle = StoryGraphStyles.WhiteBoldTextStyle();
            whiteVarNodeStyle = StoryGraphStyles.WhiteVarNodeStyle();
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public virtual void SetSerializedProperties()
        {
        }

        public virtual void Draw(bool isVisible)
        {
            if (serializedObject == null)
            {
                serializedObject = new SerializedObject(this);
            }

            inPoint.UpdateLocation(rect.x, rect.y, rect.width, rect.height);
            inPoint.Draw();
            outPoint.UpdateLocation(rect.x, rect.y, rect.width, rect.height);
            outPoint.Draw();

            GUI.BeginGroup(rect, style);

            GUI.BeginGroup(new Rect(0, 0, rect.width, 50), nodeHeaderStyle);
            GUI.Label(new Rect(0, 0, rect.width, 50), title, whiteTextHeaderStyle);
            if (GUI.Button(new Rect(rect.width - 25, 5, 15, 15), "", closeButtonStyle))
            {
                storyGraph.RemoveNode(this);
            }
            GUI.EndGroup();


            if (isVisible)
            {
                if (storyNodeState == StoryNodeState.IsAsleep)
                {
                    GUI.BeginGroup(new Rect(0, 42, rect.width, 40));
                    GUI.Label(new Rect(15, 8, rect.width, 20), "Status:", whiteBoldTextStyle);
                    GUI.Label(new Rect(0, 8, rect.width - 20, 20), "Asleep", isAsleepStyle);
                    GUI.EndGroup();
                }
                else if (storyNodeState == StoryNodeState.IsAwake)
                {
                    GUI.BeginGroup(new Rect(0, 40, rect.width, 40));
                    GUI.Label(new Rect(15, 8, rect.width, 20), "Status:", whiteBoldTextStyle);
                    GUI.Label(new Rect(0, 8, rect.width - 20, 20), "Awake", isAwakeStyle);
                    GUI.EndGroup();
                }
                else if (storyNodeState == StoryNodeState.IsDone)
                {
                    GUI.BeginGroup(new Rect(0, 40, rect.width, 40));
                    GUI.Label(new Rect(15, 8, rect.width, 20), "Status:", whiteBoldTextStyle);
                    GUI.Label(new Rect(0, 8, rect.width - 20, 20), "Done", isDoneStyle);
                    GUI.EndGroup();
                }

                SetSpacing();
                DrawSerializedProperties();
            }
            else
            {
                if (SerializedProperties != null && SerializedProperties.Count > 0)
                {
                    SerializedProperties = null;
                    Spacing = 0;
                    SetRectSize();
                }
            }

            // if (isSelected)
            // {
            //     SetSpacing();
            //     DrawSerializedProperties();
            // } else {
            //     if(SerializedProperties != null && SerializedProperties.Count > 0)
            //     { 
            //         SerializedProperties = null;
            //         Spacing = 0;
            //         SetRectSize();
            //     }
            // }

            GUI.EndGroup();


            // if (Spacing != GetSpacing())
            // {
            //     SetSpacing();
            // }

        }

        public void SetSpacing()
        {
            Spacing = GetSpacing();
            SetRectSize();
        }

        public float GetSpacing()
        {
            float spacing = 0;
            if (SerializedProperties != null && SerializedProperties.Count > 0)
            {
                for (int i = 0; i < SerializedProperties.Count; i++)
                {
                    spacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                }
                spacing += EditorGUIUtility.singleLineHeight * (SerializedProperties.Count - 1);
                spacing += (SerializedProperties.Count) * 2;
            }
            return spacing;
        }

        public float GetNegativeSpace()
        {
            float spacing = 0;
            if (SerializedProperties != null && SerializedProperties.Count > 0)
            {
                for (int i = 0; i < SerializedProperties.Count; i++)
                {
                    spacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                }
                // spacing += EditorGUIUtility.singleLineHeight * (SerializedProperties.Count-1);
                spacing += (SerializedProperties.Count) * 2;
            }
            else
            {
                return NegativeSpace;
            }
            return -spacing;
        }

        public void SetRectSize()
        {
            rect = new Rect(rect.x, rect.y, originalRect.width, originalRect.height + Spacing);
        }

        public void InitializeSerializedProperties()
        {
            if (SerializedProperties == null || serializedObject == null)
            {
                SerializedProperties = new List<SerializedProperty>();
                Labels = new List<string>();
                // HasLabelGUI = new List<bool>();
                // IsObjectField = new List<bool>();
                StorySerializedPropertyTypes = new List<StorySerializedPropertyType>();
                serializedObject = new SerializedObject(this);

                SetSerializedProperties();
                SetSpacing();
            }
        }

        public virtual void DrawSerializedProperties()
        {
            InitializeSerializedProperties();
            if (SerializedProperties.Count > 0)
            {
                GUI.BeginGroup(new Rect(10, 75, rect.width, rect.height));
                GUILayout.Space(NegativeSpace);

                serializedObject.Update();
                float currentSpacing = 0;
                for (int i = 0; i < SerializedProperties.Count; i++)
                {
                    if (StorySerializedPropertyTypes[i] == StorySerializedPropertyType.NoLabelPropertyField)
                    {
                        GUI.BeginGroup(new Rect(0, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height));
                        EditorGUILayout.PropertyField(SerializedProperties[i], GUIContent.none, true, GUILayout.MaxWidth(rect.width - 30));
                    GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }
                    else if (StorySerializedPropertyTypes[i] == StorySerializedPropertyType.PropertyField)
                    {
                        GUI.Label(new Rect(5, (currentSpacing) + (i * 2) + EditorGUIUtility.singleLineHeight * i, rect.width, 50), Labels[i], whiteTextStyle);
                        GUI.BeginGroup(new Rect(rect.width / 2, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height), whiteTextStyle);
                        EditorGUILayout.PropertyField(SerializedProperties[i], GUIContent.none, true, GUILayout.MaxWidth(rect.width * 2 / 4 - 30));
                        GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }

                    else if (StorySerializedPropertyTypes[i] == StorySerializedPropertyType.UnityEvent || StorySerializedPropertyTypes[i] == StorySerializedPropertyType.Array)
                    {
                        GUI.BeginGroup(new Rect(0, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height));
                        EditorGUILayout.PropertyField(SerializedProperties[i], new GUIContent(Labels[i]), true, GUILayout.MaxWidth(rect.width - 30));
                        GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }
                    else if (StorySerializedPropertyTypes[i] == StorySerializedPropertyType.RadioButton)
                    {
                        GUI.Label(new Rect(5, (currentSpacing) + (i * 2) + EditorGUIUtility.singleLineHeight * i, rect.width, 50), Labels[i], whiteTextStyle);
                        GUI.BeginGroup(new Rect(rect.width - 45, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height), whiteTextStyle);
                        EditorGUILayout.PropertyField(SerializedProperties[i], GUIContent.none, true, GUILayout.MaxWidth(rect.width * 1 / 6));
                        GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }
                    else if (StorySerializedPropertyTypes[i] == StorySerializedPropertyType.NoLabelReadOnly)
                    {
                        GUI.BeginGroup(new Rect(0, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height));
                        GUI.enabled = false;
                        EditorGUILayout.PropertyField(SerializedProperties[i], GUIContent.none, true, GUILayout.MaxWidth(rect.width - 30));
                        GUI.enabled = true;
                        GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }
                   /* else if (StorySerializedPropertyTypes[i] == StorySerializedPropertyType.IntegerField)
                    {
                        GUI.Label(new Rect(5, (currentSpacing) + (i * 2) + EditorGUIUtility.singleLineHeight * i, rect.width, 50), Labels[i], whiteTextStyle);
                        GUI.BeginGroup(new Rect(rect.width - 45, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height), whiteTextStyle);
                        EditorGUILayout.PropertyField(SerializedProperties[i], GUIContent.none, true, GUILayout.MaxWidth(rect.width));
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);

                    }*/
                }
                // Debug.Log(Spacing);
                serializedObject.ApplyModifiedProperties();
                GUI.EndGroup();
            }

        }

        public virtual void AddSerializedProperty(string propertyName)
        {
            SerializedProperties.Add(serializedObject.FindProperty(propertyName));
            Labels.Add(propertyName);
            StorySerializedPropertyTypes.Add(StorySerializedPropertyType.PropertyField);
        }

        public virtual void AddSerializedProperty(string propertyName, string label)
        {
            SerializedProperties.Add(serializedObject.FindProperty(propertyName));
            Labels.Add(label);
            StorySerializedPropertyTypes.Add(StorySerializedPropertyType.PropertyField);
        }

        public virtual void AddSerializedProperty(string propertyName, StorySerializedPropertyType StorySerializedPropertyType)
        {
            SerializedProperties.Add(serializedObject.FindProperty(propertyName));
            Labels.Add(propertyName);
            StorySerializedPropertyTypes.Add(StorySerializedPropertyType);
        }

        public virtual void AddSerializedProperty(string propertyName, string label, StorySerializedPropertyType StorySerializedPropertyType)
        {
            SerializedProperties.Add(serializedObject.FindProperty(propertyName));
            Labels.Add(label);
            StorySerializedPropertyTypes.Add(StorySerializedPropertyType);
        }

        // public virtual void AddSerializedProperty(string propertyName, bool label)
        // {
        //     SerializedProperties.Add(serializedObject.FindProperty(propertyName));
        //     if (label) Labels.Add(propertyName);
        //     else Labels.Add("");
        // }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.KeyDown:

                    if (isSelected && e.Equals(Event.KeyboardEvent(KeyCode.Delete.ToString())))
                    {
                        OnClickRemoveNode();
                    }
                    return true;
                    break;
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            // GUI.changed = true;
                            isSelected = true;
                            style = selectedNodeStyle;
                            return true;
                        }
                        else
                        {
                            // GUI.changed = true;
                            isSelected = false;
                            style = defaultNodeStyle;
                            return true;
                        }
                    }

                    if (e.button == 1 && rect.Contains(e.mousePosition))
                    {
                        // GUI.changed = true;
                        isDragged = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                        // ProcessContextMenu();
                        // e.Use();

                        return true;

                    }
                    break;

                case EventType.MouseUp:
                    isDragged = false;

                    if (e.button == 1 && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                        return true;

                    }

                    break;

                case EventType.MouseDrag:
                    if (isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }

        private void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }
        private void OnClickRemoveNode()
        {
            storyGraph.RemoveNode(this);
        }
#endif
        public int CompareTo(StoryNode storyNode)
        {
            return 0;
        }



        public void WakeUpNode()
        {
            Debug.Log(Id + " is Awake");
            storyNodeState = StoryNodeState.IsAwake;
        }

        public void GoToNextNode()
        {
            Debug.Log(Id + " is Done");
            storyNodeState = StoryNodeState.IsDone;
            outPoint.GoToNextNode();
        }

        public virtual void Execute()
        {

        }

        virtual public void DisableNode(){
            storyNodeState = StoryNodeState.IsAsleep;
        }
    }
}
