using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButtons : MonoBehaviour
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

    public void ReturnToMainMenu()
    {
        // canvasManager_ref.SwitchMainMenuPanels(0);
    }
}
