using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class ScaleCoroutineNode : StoryNode {

	public GameObject Origin;
	public GameObject Target;
	public float Duration;
	public AnimationCurve AnimCurve;

	#if UNITY_EDITOR    
    public override string MenuName {get{return "Coroutine/GameObject/Scale";}}
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
		storyGraph.StartCoroutine(ScaleTarget());
	}
	public IEnumerator ScaleTarget()
    {

        float journey = 0f;
        Vector3 beginningScale = Origin.transform.localScale;
        while (journey <= Duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / Duration);

            float curvePercent = AnimCurve.Evaluate(percent);
            Origin.transform.localScale = Vector3.Lerp(beginningScale, Target.transform.localScale, curvePercent);

            yield return null;
        }
        GoToNextNode();
    }
}
