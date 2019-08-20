using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class SubStatePause : IPlayStateBase
{
    private GameManager gameManager_ref;
    private PlayState playState_ref;

    private SubStatePause() { }
    public SubStatePause(GameManager gameManager_ref, PlayState playState_ref)
    {
        this.gameManager_ref = gameManager_ref;
        this.playState_ref = playState_ref;

        if(gameManager_ref.debugMode)
        {
            Debug.Log("Constructing SubStatePause!");
        }
        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing SubStatePause DONE!!");
        }
    }
   
    public void PlayStateUpdate()
    {

    }
}
