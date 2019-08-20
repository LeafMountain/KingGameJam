using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtons : MonoBehaviour
{
    private GameManager gameManagerRef;


    void Start()
    {
        gameManagerRef = GameManager.GetInstance();    
    }

    
   public void RestartMatch()
    {
        gameManagerRef.SetNewState(new PlayState(gameManagerRef));
        gameManagerRef.audioManagerRef.ButtonSelect();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
