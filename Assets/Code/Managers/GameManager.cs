using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerRef;
    public AudioManager audioManagerRef;
    public CanvasManager canvasManagerRef;

    private IStateBase gameState;
    private LoadManager loadManagerRef;

    public bool debugMode;

    void Awake()
    {
        if(gameManagerRef == null)
        {
            gameManagerRef = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static GameManager GetInstance()
    {
        return gameManagerRef;
    }

    void Start()
    {
        if (gameState == null) gameState = new SplashScreenState(this);

        audioManagerRef = AudioManager.GetInstance();
        canvasManagerRef = CanvasManager.GetInstance();

        loadManagerRef = new LoadManager();
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
