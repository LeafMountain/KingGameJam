using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour
{
    public string prefix = "";
    public IntVariable points;

    void Start()
    {
        if(prefix == "")
            prefix = GetComponent<TMP_Text>().text;
    }

    void Update()
    {
        GetComponent<TMP_Text>().text = prefix + points.GetValue().ToString();
    }
}
