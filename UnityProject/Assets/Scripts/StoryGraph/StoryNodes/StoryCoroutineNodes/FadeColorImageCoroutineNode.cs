using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StoryGraph;

public class FadeColorImageCoroutineNode : StoryNode {

	public Image Image;
	public Color Color;
	public float Duration;
	public AnimationCurve AnimCurve;

	#if UNITY_EDITOR    
    public override string MenuName {get{return "Coroutine/CanvasUI/Fade Color Image";}}
    public override void SetStyles()
    {
        base.SetStyles();    
        nodeHeaderStyle = StoryGraphStyles.NodeCoroutineStyle();
    }
    public override void SetSerializedProperties()
    {    
        AddSerializedProperty("Image");
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
            Image.color = Color.Lerp(Image.color, Color, curvePercent);

            yield return null;
        }
        GoToNextNode();
    }
}
