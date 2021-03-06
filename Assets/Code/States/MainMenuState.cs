﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class MainMenuState : IStateBase
{
    private GameManager gameManagerRef;
    
    private MainMenuState() { }
    public MainMenuState(GameManager gameManager_ref)
    {
        this.gameManagerRef = gameManager_ref;
        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing MainMenuState!");
        }

        gameManagerRef.audioManagerRef.StartMenuMusic();

        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing MainMenuState DONE!!");
        }

        CanvasManager.ActivateMainMenu();
        PlayerManager.StartScanningForPlayers();
    }

    public void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(PlayerManager.GetPlayerCount() > 1)
            {
                gameManagerRef.SetNewState(new PlayState(gameManagerRef));
            }
            else
            {
                Debug.Log("Not enough players to start game");
            }
        }
    }

    public void UIState()
    {

    }
}
