using System.Collections;
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


        if (gameManager_ref.debugMode)
        {
            Debug.Log("Constructing MainMenuState DONE!!");
        }
    }

    public void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(gameManagerRef.players.Count > 1)
            {
                gameManagerRef.SetNewState(new PlayState(gameManagerRef));
            }
            else
            {
                Debug.Log("Not enough players to start game");
            }
        }

        // Check if player joins
        for (int i = 0; i < gameManagerRef.inputMappings.Length; i++)
        {
            if(gameManagerRef.inputMappings[i].GetAttack())
            {
                SpawnPlayer(i);
            }
        }
    }

    public void UIState()
    {

    }

    private void SpawnPlayer(int inputMappingIndex)
    {
        if(gameManagerRef.usedMappings.Contains(inputMappingIndex))
        {
            Debug.Log("This user has already joined the game");
            return;
        }
        
        int paletteIndex = -1;
        for (int i = 0; i < gameManagerRef.colorPalettes.Length; i++)
        {
            if(!gameManagerRef.usedPalettes.Contains(i))
            {
                paletteIndex = i;
                gameManagerRef.usedPalettes.Add(i);
                break;
            }
        }

        if(paletteIndex < 0)
        {
            Debug.Log("No color palettes left");
            return;
        }

        gameManagerRef.usedMappings.Add(inputMappingIndex);
        Kroken player = GameObject.Instantiate(gameManagerRef.krokenPrefab, Vector2.zero, Quaternion.identity);
        player.Init(gameManagerRef.inputMappings[inputMappingIndex], gameManagerRef.colorPalettes[paletteIndex]);
    }
}
