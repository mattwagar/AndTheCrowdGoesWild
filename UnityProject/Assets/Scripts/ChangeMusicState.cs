using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class ChangeMusicState : ActionNode
{

    [StoryGraphField] public MarbleMusicManager marbleMusicManager;
    [StoryGraphField] public MusicState musicState;
  
    public override string MenuName {get{return "Action/ChangeMusicState";}}



    public override void Execute()
    {
        marbleMusicManager.musicState = musicState;
        GoToNextNode();
    }

}
