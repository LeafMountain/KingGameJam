using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class PlayState : IStateBase
{
    private GameManager gameManager_ref;
    private IPlayStateBase playState_ref;


    private PlayState() { }
    public PlayState(GameManager gameManager_ref)
    {
        this.gameManager_ref = gameManager_ref;

        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing PlayState!");
        }

        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing PlayState DONE!!!");
        }
    }

    public void StateUpdate()
    {

    }
    public void UIState()
    {
        
    }
}
