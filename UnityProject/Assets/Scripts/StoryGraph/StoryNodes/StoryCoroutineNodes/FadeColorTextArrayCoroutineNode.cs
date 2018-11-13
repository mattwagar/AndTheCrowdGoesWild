using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StoryGraph;

public class FadeColorTextArrayCoroutineNode : StoryNode
{

    public Text[] Text;
    public Color Color;
    public float Duration;
    public float Offset;
    public AnimationCurve AnimCurve;

#if UNITY_EDITOR
    public override string MenuName {get{return "Coroutine/CanvasUI/Fade Color Text Array";}}
    public override void SetStyles()
    {
        base.SetStyles();
        nodeHeaderStyle = StoryGraphStyles.NodeCoroutineStyle();
    }
    public override void SetSerializedProperties()
    {
        AddSerializedProperty("Text", StorySerializedPropertyType.Array);
        AddSerializedProperty("Color");
        AddSerializedProperty("Duration");
        AddSerializedProperty("Offset");
        AddSerializedProperty("AnimCurve");
    }
#endif

    public override void Execute()
    {

        for (int i = 0; i < Text.Length; i++)
        {
            storyGraph.StartCoroutine(FadeInTarget(Text[i], (float)i * Offset));
        }
    }
    public IEnumerator FadeInTarget(Text text, float Offset)
    {

        yield return new WaitForSeconds(Offset);

        float journey = 0f;
        while (journey <= Duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / Duration);

            float curvePercent = AnimCurve.Evaluate(percent);
            text.color = Color.Lerp(text.color, Color, curvePercent);

            yield return null;
        }
        GoToNextNode();
    }
}
