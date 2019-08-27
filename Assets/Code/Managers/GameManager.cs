using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerRef;
    [HideInInspector] public AudioManager audioManagerRef;
    [HideInInspector] public CanvasManager canvasManagerRef;

    [Header("Setup")]
    public Kroken krokenPrefab = null;
    public InputMapping[] inputMappings = null;
    [HideInInspector] public List<int> usedMappings = new List<int>();

    public ColorPalette[] colorPalettes = null;
    [HideInInspector] public List<int> usedPalettes = new List<int>();

    public Camera cam;

    private IStateBase gameState;
    private LoadManager loadManagerRef;

    [HideInInspector]
    public bool gameRunning;

    public bool debugMode;

    public  List<Kroken> players = new List<Kroken>();

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

    public ColorPalette SwapPalette(ColorPalette oldPalette)
    {
        int currentPaletteIndex = -1;

        // Add old palette to available palettes again
        if(oldPalette != null)
        {
            for (int i = 0; i < colorPalettes.Length; i++)
            {
                if(colorPalettes[i] == oldPalette)
                {
                    usedPalettes.Remove(i);
                    currentPaletteIndex = i;
                    break;
                }
            }
        }

        int oldPaletteIndex = currentPaletteIndex;
        currentPaletteIndex = (currentPaletteIndex + 1) % (colorPalettes.Length);
        while(usedPalettes.Contains(currentPaletteIndex))
        {
            currentPaletteIndex = (currentPaletteIndex + 1) % (colorPalettes.Length);

            if(oldPaletteIndex == currentPaletteIndex)
            {
                Debug.Log("Missing available palettes.");
                return null;
            }
        }

        usedPalettes.Add(currentPaletteIndex);
        return colorPalettes[currentPaletteIndex];
    }

    private void CheckIfDone()
    {
        if(players.Count == 1)
        {
            Debug.Log($"Game is done. Player {players[0].name} won!");
            SetNewState(new ResultState(this, players[0]));
        }
        else if(players.Count < 1)
        {
            Debug.Log("No player left. Ending game in a tie");
        }
    }
}
