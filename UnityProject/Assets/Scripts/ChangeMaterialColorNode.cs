using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryGraph
{
    public class ChangeMaterialColorNode : CoroutineNode
    {

        [StoryGraphField] public SkinnedMeshRenderer skinnedMeshRenderer;
        [StoryGraphField] public Color color;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public AnimationCurve AnimCurve;
        
        public override string MenuName { get { return "ChangeMaterialColorNode"; } }

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
                skinnedMeshRenderer.material.color = Color.Lerp(Color.white, color, curvePercent);

                yield return null;
            }
            GoToNextNode();
        }
    }
}