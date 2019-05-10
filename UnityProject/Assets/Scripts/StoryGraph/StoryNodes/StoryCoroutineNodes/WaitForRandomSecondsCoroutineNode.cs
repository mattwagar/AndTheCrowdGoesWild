using System.Collections;
using UnityEngine;

namespace StoryGraph
{
    public class WaitForRandomSecondsCoroutineNode : CoroutineNode
    {
        [StoryGraphField] public float LowDuration;
        [StoryGraphField] public float HighDuration;

        public override string MenuName { get { return "Wait For Random Seconds"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(WaitForSeconds());
        }
        public IEnumerator WaitForSeconds()
        {

            yield return new WaitForSeconds(Random.Range(LowDuration, HighDuration));
            GoToNextNode();
        }
    }
}




