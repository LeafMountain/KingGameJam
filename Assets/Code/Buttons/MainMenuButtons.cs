using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    private GameManager gameManager_ref;
    private CanvasManager canvasManager_ref;
    private AudioManager audioManager_ref;
    

    void Start()
    {
        gameManager_ref = GameManager.GetInstance();
        canvasManager_ref = CanvasManager.GetInstance();
        audioManager_ref = AudioManager.GetInstance();
    }

    public void StartGame()
    {
        audioManager_ref.ButtonSelect();
    }
    public void OpenOptions()
    {
        canvasManager_ref.SwitchMainMenuPanels(1);
    }
    public void OpenCredits()
    {
        canvasManager_ref.SwitchMainMenuPanels(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
