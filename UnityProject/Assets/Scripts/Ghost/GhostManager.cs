﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{

    [HideInInspector]
    public Ghost[] Ghosts;

    public Texture2D HappyGhostTex;
    public Texture2D WowGhostTex;
    public Texture2D PleasedGhostTex;
    public Texture2D TerriefiedGhostTex;
    public Texture2D YawnGhostTex;
    public Texture2D ExcitedGhostTex;

    // Use this for initialization
    void Start()
    {
        Ghosts = new Ghost[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            Ghosts[i] = transform.GetChild(i).GetComponent<Ghost>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private enum GhostState {Clap, Run, Gasp, Idle, Yawn, Wiggle}
    private GhostState state = GhostState.Idle;

    public void Clap()
    {

        if(state != GhostState.Clap){
            for (int i = 0; i < Ghosts.Length; i++)
            {
                int rnd = Random.Range(0, 10);

                if(rnd <= 4){
                    Ghosts[i].animator.Play("Clap_01");
                    Ghosts[i].material.mainTexture = PleasedGhostTex;
                }else if(rnd <= 8){
                    Ghosts[i].animator.Play("Clap_02");
                    Ghosts[i].material.mainTexture = ExcitedGhostTex;
                }else if(rnd == 9){
                    Ghosts[i].animator.Play("Wiggle");
                    Ghosts[i].material.mainTexture = WowGhostTex;
                }
            }
            state = GhostState.Clap;
        }
    }

    public void RunAway()
    {
        for (int i = 0; i < Ghosts.Length; i++)
        {
            Ghosts[i].animator.Play("Wiggle");
            Ghosts[i].material.mainTexture = TerriefiedGhostTex;
        }
        // for (int i = 0; i < Ghosts.Length; i++)
        // {
        //     Ghosts[i].animator.Play("Running");
        // }
    }

    public void Gasp()
    {
        if(state != GhostState.Clap){
            for (int i = 0; i < Ghosts.Length; i++)
            {
                int rnd = Random.Range(0, 10);

                if(rnd <= 4){
                    Ghosts[i].animator.Play("Gasp");
                    Ghosts[i].material.mainTexture = WowGhostTex;
                }else if(rnd <= 8){
                    Ghosts[i].animator.Play("Clap_02");
                    Ghosts[i].material.mainTexture = HappyGhostTex;
                }else if(rnd == 9){
                    Ghosts[i].animator.Play("Wiggle");
                    Ghosts[i].material.mainTexture = ExcitedGhostTex;
                }
            }
            state = GhostState.Clap;
        }
        // if(state != GhostState.Clap){
        //     for (int i = 0; i < Ghosts.Length; i++)
        //     {
        //         Ghosts[i].animator.Play("Gasp");
        //     }
        // }
    }

    public void Idle()
    {
        for (int i = 0; i < Ghosts.Length; i++)
        {
            Ghosts[i].material.mainTexture = HappyGhostTex;
            Ghosts[i].animator.Play("Idle");
        }
    }

    public void Yawn()
    {
        for (int i = 0; i < Ghosts.Length; i++)
        {
            int rnd = Random.Range(0, 10);
            if(rnd == 1){
                Ghosts[i].animator.Play("Yawn");
                Ghosts[i].material.mainTexture = YawnGhostTex;
            }
        }
    }

    public void Wiggle()
    {
        for (int i = 0; i < Ghosts.Length; i++)
        {
            Ghosts[i].animator.Play("Wiggle");
            Ghosts[i].material.mainTexture = WowGhostTex;
        }
    }
}
