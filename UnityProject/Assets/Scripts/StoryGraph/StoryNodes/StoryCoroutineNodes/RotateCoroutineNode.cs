using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class RotateCoroutineNode : StoryNode {

	public GameObject Origin;
	public GameObject Target;
	public float Duration;
	public AnimationCurve AnimCurve;

	#if UNITY_EDITOR    
    public override string MenuName {get{return "Coroutine/GameObject/Rotate";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeCoroutineStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("Origin");
        AddSerializedProperty("Target");
        AddSerializedProperty("Duration");
        AddSerializedProperty("AnimCurve");
    }
    #endif
	
	public override void Execute()
	{
		storyGraph.StartCoroutine(RotateTarget());
	}
	public IEnumerator RotateTarget()
    {

        float journey = 0f;
        while (journey <= Duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / Duration);

            float curvePercent = AnimCurve.Evaluate(percent);
            Origin.transform.rotation = Quaternion.Lerp(Origin.transform.rotation, Target.transform.rotation, curvePercent);

            yield return null;
        }
        GoToNextNode();
    }
}
