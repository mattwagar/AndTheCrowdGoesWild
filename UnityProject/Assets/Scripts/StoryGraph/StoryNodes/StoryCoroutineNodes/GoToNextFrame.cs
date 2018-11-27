using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class GoToNextFrame : StoryNode {

	#if UNITY_EDITOR    
    public override string MenuName {get{return "Coroutine/Go To Next Frame";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeCoroutineStyle();
    }
    public override void SetSerializedProperties()
    {    
    }
    #endif
	
	public override void Execute()
	{
		storyGraph.StartCoroutine(WaitForSeconds());
	}
	public IEnumerator WaitForSeconds()
    {
        yield return null;
        GoToNextNode();
    }
}




