using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    private bool textChanged;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!textChanged && PlayerManager.GetPlayerCount() > 1)
        {
            SetNewText();
            textChanged = true;
        }
    }
    
    private void SetNewText()
    {
        GetComponent<Text>().text =
            "PRESS SPACE TO START";
    }
}
