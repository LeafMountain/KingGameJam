using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class PlayState : IStateBase
{
    private GameManager gameManagerRef;
    private IPlayStateBase playStateRef;


    private PlayState() { }
    public PlayState(GameManager gameManager_ref)
    {
        this.gameManagerRef = gameManager_ref;

        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing PlayState!");
        }

       gameManagerRef.canvasManagerRef.TogglePlayUI(true);
        gameManagerRef.canvasManagerRef.ToggleMainMenu(false);

        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing PlayState DONE!!!");
        }
    }

    public void StateUpdate()
    {
        //DebugWin
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameManagerRef.SetNewState(new ResultState(gameManagerRef));
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            gameManagerRef.SetNewState(new PauseState(gameManagerRef));
        }

    }
    public void UIState()
    {
        
    }
}
