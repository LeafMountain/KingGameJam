using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager canvasManager_ref;

    //Tier 1 panels
    private GameObject pnl_SplashScreen_ref;
    private GameObject pnl_MainMenu_ref;
    private GameObject pnl_Play_ref;
    private GameObject pnl_Pause_ref;
    private GameObject pnl_ResultRef;

    private GameObject[] panels; 

    private GameObject[] tier1pnls;

    //Tier 2 panels

    //MainMenu
    private GameObject pnl_Main_ref;
    private GameObject pnl_Options_ref;
    private GameObject pnl_Credits_ref;

    private GameObject[] tier2_MainMenu_pnls;

    void Awake()
    {
        if(canvasManager_ref == null)
        {
            canvasManager_ref = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static CanvasManager GetInstance()
    {
        return canvasManager_ref;
    }

    
    void Start()
    {
        

        pnl_SplashScreen_ref = transform.GetChild(0).gameObject;
        pnl_MainMenu_ref = transform.GetChild(1).gameObject;
        pnl_Play_ref = transform.GetChild(2).gameObject;
        pnl_Pause_ref = transform.GetChild(3).gameObject;
        pnl_ResultRef = transform.GetChild(4).gameObject;

        tier1pnls = new GameObject[] { pnl_SplashScreen_ref, pnl_MainMenu_ref,
                                        pnl_Play_ref, pnl_Pause_ref };

        pnl_Main_ref = pnl_MainMenu_ref.transform.GetChild(0).gameObject;
        pnl_Options_ref = pnl_MainMenu_ref.transform.GetChild(1).gameObject;
        pnl_Credits_ref = pnl_MainMenu_ref.transform.GetChild(2).gameObject;

        tier2_MainMenu_pnls = new GameObject[] { pnl_Main_ref, pnl_Options_ref, pnl_Credits_ref };
        panels = new GameObject[] { pnl_SplashScreen_ref, pnl_MainMenu_ref,
        pnl_Play_ref, pnl_Pause_ref, pnl_ResultRef};

        ToggleMainMenu(true);
    }
    public void ToggleSplashScreen()
    {

    }

    public void ToggleMainMenu(bool b)
    {
        DisablePanels();
        pnl_MainMenu_ref.SetActive(b);
    }
    public void TogglePlayUI(bool b)
    {
        DisablePanels();

        pnl_Play_ref.SetActive(b);
    }
    public void ToggleVictoryPanel(bool b)
    {
        DisablePanels();

        pnl_ResultRef.SetActive(b);
    }

    public void TogglePause(bool b)
    {
        DisablePanels();

        pnl_Pause_ref.SetActive(b);
    }

    public void SetMainMenuStep(int step)
    {
        for (int i = 0; i < pnl_MainMenu_ref.transform.childCount; i++)
        {
            pnl_MainMenu_ref.transform.GetChild(i).gameObject.SetActive(false);
        }

        switch (step)
        {
            case 0:

                pnl_MainMenu_ref.transform.GetChild(0).gameObject.SetActive(true);

                break;

            case 1:

                pnl_MainMenu_ref.transform.GetChild(1).gameObject.SetActive(true);

                break;

            default:
                break;
        }
    }
    public void SwitchTier1Panels(int index)
    {
        for (int i = 0; i < tier1pnls.Length; i++)
        {
            tier1pnls[i].SetActive(false);
        }

        tier1pnls[index].SetActive(true);
    }

    public void SwitchMainMenuPanels(int index)
    {
        for (int i = 0; i < tier2_MainMenu_pnls.Length; i++)
        {
            tier2_MainMenu_pnls[i].SetActive(false);
        }

        tier2_MainMenu_pnls[index].SetActive(true);
    }
    private void DisablePanels()
    {
        foreach(GameObject go in panels)
        {
            go.SetActive(false);
        }
    }
    
}
