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

        if (!gameManagerRef.gameRunning)
        {
            gameManagerRef.gameRunning = true;
            gameManagerRef.audioManagerRef.StartPlayMusicTrack();
        }

        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing PlayState DONE!!!");
        }

        CanvasManager.ActivatePlayMenu();
        PlayerManager.StopScanningForPlayers();
        PlayerManager.SetPlayerLockstate(false);
        EnemyManager.StartSpawning();
    }

    public void StateUpdate()
    {
        if (CheckIfDone() || Input.GetKeyDown(KeyCode.Y))
        {
            gameManagerRef.SetNewState(new ResultState(gameManagerRef, PlayerManager.GetPlayers()[0]));
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            gameManagerRef.SetNewState(new PauseState(gameManagerRef));
        }
    }

    public void UIState()
    {
        
    }

    private bool CheckIfDone()
    {
        return PlayerManager.GetPlayerCount() < 2;
    }
}
