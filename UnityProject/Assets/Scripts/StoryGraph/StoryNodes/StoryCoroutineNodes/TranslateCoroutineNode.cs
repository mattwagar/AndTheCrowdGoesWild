using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class TranslateCoroutineNode : StoryNode {

	public GameObject Origin;
	public GameObject Target;
	public float Duration;
	public AnimationCurve AnimCurve;

	#if UNITY_EDITOR    
    public override string MenuName {get{return "Coroutine/GameObject/Translate";}}
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
		storyGraph.StartCoroutine(TranslateTarget());
	}
	public IEnumerator TranslateTarget()
    {

        float journey = 0f;


        while (journey <= Duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / Duration);

            float curvePercent = AnimCurve.Evaluate(percent);
            Origin.transform.position = Vector3.LerpUnclamped(Origin.transform.position, Target.transform.position, curvePercent);
            yield return null;
        }
        GoToNextNode();
    }
}
