using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;
    public List<Kroken> players = new List<Kroken>();
    public Kroken krokenPrefab = null;

    public List<InputMapping> inputMappings = null;
    [HideInInspector] public List<int> usedMappings = new List<int>();

    public List<ColorPalette> colorPalettes = null;
    [HideInInspector] public List<int> usedPalettes = new List<int>();

    private IEnumerator scanningCoroutine = null;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static int GetPlayerCount() => Instance.players.Count;
    public static List<Kroken> GetPlayers() => Instance.players;

    public static void AddPlayer(Kroken player)
    {
        if(Instance.players.Contains(player))
        {
            Debug.Log("Player already exists");
            return;
        }
        Instance.players.Add(player);
    }

    public static void RemovePlayer(Kroken player)
    {
        if(!Instance.players.Contains(player))
        {
            Debug.Log("This player is not a part of the game");
            return;
        }

        Instance.usedMappings.Remove(Instance.inputMappings.FindIndex(i => player.inputMapping));
        Instance.usedPalettes.Remove(Instance.colorPalettes.FindIndex(i => player.colorPalette));
        Instance.players.Remove(player);
    }

    public static void SetPlayerLockstate(bool value)
    {
        foreach (var player in Instance.players)
        {
            player.SetMovementLock(value);
        }
    }

    public static void ClearPlayers()
    {
        for (int i = Instance.players.Count - 1; i >= 0; i--)
        {
            GameObject.Destroy(Instance.players[i]);
        }

        Instance.usedMappings.Clear();
        Instance.usedPalettes.Clear();
    }

    public static void SpawnPlayer(int inputMappingIndex)
    {
        if(Instance.usedMappings.Contains(inputMappingIndex))
        {
            Debug.Log("This user has already joined the game");
            return;
        }
        
        int paletteIndex = -1;
        for (int i = 0; i < Instance.colorPalettes.Count; i++)
        {
            if(!Instance.usedPalettes.Contains(i))
            {
                paletteIndex = i;
                Instance.usedPalettes.Add(i);
                break;
            }
        }

        if(paletteIndex < 0)
        {
            Debug.Log("No color palettes left");
            return;
        }

        Instance.usedMappings.Add(inputMappingIndex);
        Kroken player = GameObject.Instantiate(Instance.krokenPrefab, Vector2.zero, Quaternion.identity);
        player.Init(Instance.inputMappings[inputMappingIndex], Instance.colorPalettes[paletteIndex]);
    }

    public static ColorPalette SwapPalette(ColorPalette oldPalette)
    {
        int currentPaletteIndex = -1;

        // Add old palette to available palettes again
        if(oldPalette != null)
        {
            for (int i = 0; i < Instance.colorPalettes.Count; i++)
            {
                if(Instance.colorPalettes[i] == oldPalette)
                {
                    Instance.usedPalettes.Remove(i);
                    currentPaletteIndex = i;
                    break;
                }
            }
        }

        int oldPaletteIndex = currentPaletteIndex;
        currentPaletteIndex = (currentPaletteIndex + 1) % (Instance.colorPalettes.Count);
        while(Instance.usedPalettes.Contains(currentPaletteIndex))
        {
            currentPaletteIndex = (currentPaletteIndex + 1) % (Instance.colorPalettes.Count);

            if(oldPaletteIndex == currentPaletteIndex)
            {
                Debug.Log("Missing available palettes.");
                return null;
            }
        }

        Instance.usedPalettes.Add(currentPaletteIndex);
        return Instance.colorPalettes[currentPaletteIndex];
    }

    public static void StartScanningForPlayers()
    {
        Instance.scanningCoroutine = Instance.StartScanningForPlayers2();
        Instance.StartCoroutine(Instance.scanningCoroutine);
    }

    public static void StopScanningForPlayers()
    {
        Instance.StopCoroutine(Instance.scanningCoroutine);
    }

    private IEnumerator StartScanningForPlayers2()
    {
        while(true)
        {
            // Check if player joins
            for (int i = 0; i < inputMappings.Count; i++)
            {
                if(inputMappings[i].GetAttack())
                {
                    SpawnPlayer(i);
                }
            }
            
            yield return null;
        }
    }
}