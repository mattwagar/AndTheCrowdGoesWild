using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class SetRandomClip : ActionNode
{

    [StoryGraphField] public AudioSource source;
    [StoryGraphField(StoryDrawer.NoLabelPropertyField)] public AudioClip[] clips;
  
    public override string MenuName {get{return "Action/SetRandomClip";}}



    public override void Execute()
    {
        int size = clips.Length;
        source.clip = clips[Random.Range(0,size)];
        source.Play();
        GoToNextNode();
    }

}
