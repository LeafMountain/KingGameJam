﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class ResultState : IStateBase
{
    private GameManager gameManagerRef;

    private ResultState() { }

    public ResultState(GameManager gameManagerRef, Kroken player)
    {
        this.gameManagerRef = gameManagerRef;

        gameManagerRef.canvasManagerRef.ToggleVictoryPanel(true, player.GetName(), player.GetColor());
    }
    
    public void StateUpdate()
    {
      
    }

    public void UIState()
    {
       
    }   
}
