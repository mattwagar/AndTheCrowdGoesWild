// using UnityEngine;
// using UnityEditor;

// // ensure class initializer is called whenever scripts recompile
// [InitializeOnLoadAttribute]
// public static class EditorAppEventHandler
// {
//     // register an event handler when the class is initialized
//     static EditorAppEventHandler()
//     {
//         EditorApplication.playModeStateChanged += LogPlayModeState;
//     }

//     private static void LogPlayModeState(PlayModeStateChange state)
//     {
//         Debug.Log(state);
//         if(state == PlayModeStateChange.EnteredEditMode)
//         {
//             AssetDatabase.Refresh();
//         }
//     }
// }