using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class MainMenuState : IStateBase
{
    private GameManager gameManager_ref;
    
    private MainMenuState() { }
    public MainMenuState(GameManager gameManager_ref)
    {
        this.gameManager_ref = gameManager_ref;
        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing MainMenuState!");
        }


        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing MainMenuState DONE!!");
        }
    }

    public void StateUpdate()
    {


    }

    public void UIState()
    {

    }
}
