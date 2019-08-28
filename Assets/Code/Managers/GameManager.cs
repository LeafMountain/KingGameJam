using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerRef;
    [HideInInspector] public AudioManager audioManagerRef;
    [HideInInspector] public CanvasManager canvasManagerRef;

    public Camera cam;

    private IStateBase gameState;
    private LoadManager loadManagerRef;

    [HideInInspector]
    public bool gameRunning;

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

        cam = Camera.main;

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
