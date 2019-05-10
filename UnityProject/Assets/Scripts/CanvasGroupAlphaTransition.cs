using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryGraph
{
    public class CanvasGroupAlphaTransition : CoroutineNode
    {

        [StoryGraphField] public CanvasGroup canvasGroup;
        [StoryGraphField] public float ValueStart;
        [StoryGraphField] public float ValueEnd;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public AnimationCurve AnimCurve;

        public override string MenuName { get { return "Coroutine/Canvas Group Alpha Transition"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(AlphaTransition());
        }
        private IEnumerator AlphaTransition()
        {

            float journey = 0f;

            while (journey <= Duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / Duration);

                float curvePercent = AnimCurve.Evaluate(percent);
                canvasGroup.alpha = Mathf.Lerp(ValueStart, ValueEnd, curvePercent);
                yield return null;
            }
            GoToNextNode();
        }
    }
}
