using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHandEffect : HandEffect
{

    [Header("Hand VFX")]
	public SkinnedMeshRenderer rightHandRenderer;
	public SkinnedMeshRenderer leftHandRenderer;
	public Material handEffectsMaterial;

    public override void ActivateHandEffect()
    {
        rightHandRenderer.material = handEffectsMaterial;
        leftHandRenderer.material = handEffectsMaterial;
    }

}
