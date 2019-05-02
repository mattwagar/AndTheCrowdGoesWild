using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{

    public List<Ghost> Ghosts;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Clap()
    {
        for (int i = 0; i < Ghosts.Count; i++)
        {
			int rnd = Random.Range(0, 2);

			if(rnd == 0){
				Ghosts[i].animator.Play("Clap_01");
			}else if(rnd == 1){
				Ghosts[i].animator.Play("Clap_02");
			}else if(rnd == 2){
				Ghosts[i].animator.Play("Wiggle");
			}
        }
    }

    public void RunAway()
    {
        for (int i = 0; i < Ghosts.Count; i++)
        {
            Ghosts[i].animator.Play("Running");
        }
    }

    public void Gasp()
    {
        for (int i = 0; i < Ghosts.Count; i++)
        {
            Ghosts[i].animator.Play("Gasp");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < Ghosts.Count; i++)
        {
            Ghosts[i].animator.Play("Idle");
        }
    }

    public void Yawn()
    {
        for (int i = 0; i < Ghosts.Count; i++)
        {
            Ghosts[i].animator.Play("Yawn");
        }
    }

    public void Wiggle()
    {
        for (int i = 0; i < Ghosts.Count; i++)
        {
            Ghosts[i].animator.Play("Wiggle");
        }
    }
}
