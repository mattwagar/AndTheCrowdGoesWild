// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using StoryGraph;

// public class SetAllNodesAsleep : StoryNode
// {

//     #if UNITY_EDITOR    
//     public override string MenuName {get{return "Action/Set All Nodes Asleep";}}
//     public override void SetStyles()
//     {
//         base.SetStyles();    
//         nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
//     }
//     #endif


//     public override void Execute()
//     {
// 		storyGraph.setAllNodesAsleep();
//         GoToNextNode();
//     }

// }
