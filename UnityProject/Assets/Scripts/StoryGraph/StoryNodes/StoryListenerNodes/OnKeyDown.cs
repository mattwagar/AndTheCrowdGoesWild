﻿using System.Collections;
using UnityEngine;

namespace StoryGraph
{
    public class OnKeyDown : ListenerNode
    {
        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = false;
        [StoryGraphField] public KeyCode Key;


        public override string MenuName { get { return "Keyboard/On Key Down"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(KeyDownListener());
        }


        public IEnumerator KeyDownListener()
        {
            while (true)
            {
                if (Input.GetKeyDown(Key))
                {
                    GoToNextNode();
                    if (TurnOffOnExecute)
                    {
                        break;
                    }
                }
                yield return null;
            }
            yield return null;
        }
    }
}
