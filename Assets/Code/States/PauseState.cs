using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class PauseState : IStateBase
{
    private GameManager gameManagerRef;

    private PauseState() { }

    public PauseState(GameManager gameManagerRef)
    {
        this.gameManagerRef = gameManagerRef;

        // gameManagerRef.canvasManagerRef.TogglePause(true);
        CanvasManager.ActivatePauseMenu();

        gameManagerRef.audioManagerRef.BackSF();
    }

    public void StateUpdate()
    {
       if(Input.GetKeyDown(KeyCode.Escape) || 
            Input.GetKeyDown(KeyCode.P))
        {
            gameManagerRef.SetNewState(new PlayState(gameManagerRef));
            gameManagerRef.audioManagerRef.ButtonSelect();
        }

    }

    public void UIState()
    {
        
    }
}
