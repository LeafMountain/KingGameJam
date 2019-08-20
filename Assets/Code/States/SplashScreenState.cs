using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class SplashScreenState : IStateBase
{
    private GameManager gameManager_ref;

    private SplashScreenState() { }
    public SplashScreenState(GameManager gameManager_ref)
    {
        this.gameManager_ref = gameManager_ref;

        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing SplashScreenState!");
        }

        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing SplashScreenState DONE!!!");
        }
    }

    public void StateUpdate()
    {

    }
    public void UIState()
    {

    }
}
