using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager_ref;
    private AudioManager audioManager_ref;
    private CanvasManager canvasManager_ref;

    private IStateBase gameState;
    private LoadManager loadManager_ref;

    public bool debugMode;

    void Awake()
    {
        if(gameManager_ref == null)
        {
            gameManager_ref = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static GameManager GetInstance()
    {
        return gameManager_ref;
    }

    void Start()
    {
        if (gameState == null) gameState = new SplashScreenState(this);

        audioManager_ref = AudioManager.GetInstance();
        canvasManager_ref = CanvasManager.GetInstance();

        loadManager_ref = new LoadManager();
    }

    
    void Update()
    {
        gameState.StateUpdate();
    }
    public void SetNewState(IStateBase newState)
    {
        gameState = newState;
    }
}
