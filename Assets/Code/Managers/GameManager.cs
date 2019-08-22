using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerRef;
    public AudioManager audioManagerRef;
    public CanvasManager canvasManagerRef;

    public Camera cam;

    private IStateBase gameState;
    private LoadManager loadManagerRef;

    [HideInInspector]
    public bool gameRunning;

    public bool debugMode;

    private List<Kroken> players = new List<Kroken>();

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

    public void AddPlayer(Kroken player)
    {
        if(players.Contains(player))
        {
            Debug.Log("Player already exists");
            return;
        }
        players.Add(player);
    }

    public void RemovePlayer(Kroken player)
    {
        if(!players.Contains(player))
        {
            Debug.Log("This player is not a part of the game");
            return;
        }

        players.Remove(player);
        CheckIfDone();
    }

    private void CheckIfDone()
    {
        if(players.Count == 1)
        {
            Debug.Log($"Game is done. Player {players[0].name} won!");
            SetNewState(new ResultState(this));
        }
        else if(players.Count < 1)
        {
            Debug.Log("No player left. Ending game in a tie");
        }
    }
}
