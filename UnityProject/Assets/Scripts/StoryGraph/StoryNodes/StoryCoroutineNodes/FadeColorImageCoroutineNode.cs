﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryGraph
{
    public class FadeColorImageCoroutineNode : CoroutineNode
    {

        [StoryGraphField] public Image Image;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public AnimationCurve AnimCurve;
        
        public override string MenuName { get { return "UI/Fade Color Image"; } }

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
                Image.color = Color.Lerp(Image.color, Color.white, curvePercent);

                yield return null;
            }
            GoToNextNode();
        }
    }
}