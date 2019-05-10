using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;


public class OnDisableListener : StoryListener
{

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnDisable()
    {
        if (StoryListenerAction != null)
        {
            StoryListenerAction.Invoke();
        }
    }

}

