using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class IsGameObjectArrayEnabled : ConditionNode
{
    [StoryGraphField(StoryDrawer.Array)] public GameObject[] gameObjects;

    public override string MenuName { get { return "Condition/Is GameObject Array Enabled"; } }



    public override void Execute()
    {
        bool isEnabled = false;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if(gameObjects[i].activeSelf == true)
            {
                isEnabled = true;
            }
        }
            if (isEnabled)
            {
                GoToTrueNode();
            }
            else
            {
                GoToFalseNode();
            }
    }


}
