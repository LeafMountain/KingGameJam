using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class SubPlayStateCutScene : IPlayStateBase
{

    private GameManager gameManager_ref;
    private PlayState playState_ref;

    private SubPlayStateCutScene() { }
    public SubPlayStateCutScene(GameManager gameManager_ref, PlayState playState_ref)
    {
        this.gameManager_ref = gameManager_ref;
        this.playState_ref = playState_ref;

        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing SubPlayStateCutScene!");
        }


        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing SubPlayStateCutScene DONE!!!");
        }

    }

    public void PlayStateUpdate()
    {

    }
    
}
