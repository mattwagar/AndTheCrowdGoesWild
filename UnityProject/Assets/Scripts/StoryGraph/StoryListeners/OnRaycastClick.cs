using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class OnRaycastClick : StoryListener
{

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        if(StoryListenerAction != null)
        {
            StoryListenerAction.Invoke();
        }
    }
}
