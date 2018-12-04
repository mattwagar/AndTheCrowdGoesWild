using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class OnRaycastHover : MonoBehaviour
{

    public bool IsHovered = false;
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

    void OnMouseEnter()
    {
        IsHovered = true;
    }
	// void OnMouseOver()
	// {
	// 	StoryListenerAction.Invoke();
	// }
    void OnMouseExit()
    {
        IsHovered = false;
    }
}
