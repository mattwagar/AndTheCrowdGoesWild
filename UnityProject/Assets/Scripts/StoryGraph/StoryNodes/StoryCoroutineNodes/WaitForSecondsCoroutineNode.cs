using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class WaitForSecondsCoroutineNode : StoryNode {
	public float Duration;

	#if UNITY_EDITOR    
    public override string MenuName {get{return "Coroutine/Wait For Seconds";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeCoroutineStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("Duration");
    }
    #endif
	
	public override void Execute()
	{
		storyGraph.StartCoroutine(WaitForSeconds());
	}
	public IEnumerator WaitForSeconds()
    {

        yield return new WaitForSeconds(Duration);
        GoToNextNode();
    }
}




