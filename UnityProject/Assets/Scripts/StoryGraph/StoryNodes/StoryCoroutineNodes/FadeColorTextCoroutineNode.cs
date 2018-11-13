using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StoryGraph;

public class FadeColorTextCoroutineNode : StoryNode {

	public Text Text;
	public Color Color;
	public float Duration;
	public AnimationCurve AnimCurve;

	#if UNITY_EDITOR    
    public override string MenuName {get{return "Coroutine/CanvasUI/Fade Color Text";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeCoroutineStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("Text");
        AddSerializedProperty("Color");
        AddSerializedProperty("Duration");
        AddSerializedProperty("AnimCurve");
    }
    #endif
	
	public override void Execute()
	{
		storyGraph.StartCoroutine(FadeInTarget());
	}
	public IEnumerator FadeInTarget()
    {

        float journey = 0f;


        while (journey <= Duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / Duration);

            float curvePercent = AnimCurve.Evaluate(percent);
            Text.color = Color.Lerp(Text.color, Color, curvePercent);

            yield return null;
        }
        GoToNextNode();
    }
}
