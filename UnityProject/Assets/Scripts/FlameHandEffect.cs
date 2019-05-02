using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHandEffect : HandEffect
{

    [Header("Hand VFX")]
	public SkinnedMeshRenderer rightHandRenderer;
	public SkinnedMeshRenderer leftHandRenderer;
	public Material handEffectsMaterial;
	public float burnAmount = 0.68f;
	public float dissolveAmount = 0.82f;
	public float transitionSpeed;

	private IEnumerator HandEffectTransition()
	{
		float startTime = 0;
		while(startTime > 1)
		{
			startTime += Time.deltaTime * transitionSpeed;
			handEffectsMaterial.SetFloat("Burn", Mathf.Lerp(0f, burnAmount, startTime));
			handEffectsMaterial.SetFloat("DissolveAmount", Mathf.Lerp(0f, dissolveAmount, startTime));
			yield return null;
		}
	}

    public override void ActivateHandEffect()
    {
        rightHandRenderer.material = handEffectsMaterial;
        leftHandRenderer.material = handEffectsMaterial;
        StartCoroutine(HandEffectTransition());
    }

}
