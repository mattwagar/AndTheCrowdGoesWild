using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class FireHandTransitionNode : CoroutineNode
    {

        [StoryGraphField] public SkinnedMeshRenderer handRenderer;
        [StoryGraphField] public float burnAmount = 0.68f;
        [StoryGraphField] public float dissolveAmount = 0.82f;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public AnimationCurve AnimCurve;

        public override string MenuName { get { return "Coroutine/Fire Hand Transition"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(HandEffectTransition());
        }
        private IEnumerator HandEffectTransition()
        {

            float journey = 0f;
            Material mat = handRenderer.material;

            while (journey <= Duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / Duration);

                float curvePercent = AnimCurve.Evaluate(percent);
                mat.SetFloat("_Burn", Mathf.Lerp(0f, burnAmount, curvePercent));
                mat.SetFloat("_DissolveAmount", Mathf.Lerp(0f, dissolveAmount, curvePercent));
                yield return null;
            }
            GoToNextNode();
        }
    }
}
