using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class OnRaycastHover : StoryListener
{
    // void Update()
    // {
    //     RaycastHit hit;
    //     Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

    //     if (Physics.Raycast(ray, out hit, 300f))
    //     {
	// 		Debug.Log("HOVERING");
    //         if (StoryListenerAction != null)
    //         {
    //             StoryListenerAction.Invoke();
    //         }
    //     }
    // }

	/// <summary>
	/// Called every frame while the mouse is over the GUIElement or Collider.
	/// </summary>
	void OnMouseOver()
	{
		Debug.Log("Hovering");
	}
}
