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
    }

    public void StartGame()
    {
        audioManagerRef.ButtonSelect();
        gameManagerRef.SetNewState(new PlayState(gameManagerRef));
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
        Application.Quit();
    }
}
