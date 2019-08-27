using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    private GameManager gameManagerRef;
    private CanvasManager canvasManagerRef;
    private AudioManager audioManagerRef;
    

    void Start()
    {
        gameManagerRef = GameManager.GetInstance();
        canvasManagerRef = CanvasManager.GetInstance();
        audioManagerRef = AudioManager.GetInstance();
        PlayerManager.StartScanningForPlayers();
    }

    public void StartGame()
    {
        audioManagerRef.ButtonSelect();
        gameManagerRef.SetNewState(new PlayState(gameManagerRef));
    }
    public void PressStart()
    {
        if(PlayerManager.GetPlayerCount() > 1)
        {
            audioManagerRef.ButtonSelect();
            gameManagerRef.canvasManagerRef.SetMainMenuStep(1);
            gameManagerRef.SetNewState(new PlayState(gameManagerRef));
        }
        else
        {
            Debug.Log("Not enough players to start game");
        }
    }

    public void GoToMainMenu()
    {
        gameManagerRef.SetNewState(new MainMenuState(gameManagerRef));
    }

    public void OpenOptions()
    {
        canvasManagerRef.SwitchMainMenuPanels(1);
    }
    public void OpenCredits()
    {
        canvasManagerRef.SwitchMainMenuPanels(2);
    }
    public void QuitGame()
    {
        audioManagerRef.ButtonSelect();
        Application.Quit();
    }
}
