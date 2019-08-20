using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDevPortrait : MonoBehaviour
{
    private object[] devPortraits;
    
    private Image myImage;
    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        myImage.sprite = null;

        devPortraits = Resources.LoadAll("Dev");
    }

    public void ClearImage()
    {
        print("poop");
        myImage.sprite = null;
    }
    public void Show01()
    {
        print("hey");
        myImage.sprite = devPortraits[0] as Sprite;
    }
    public void Show02()
    {
        myImage = (Image)devPortraits[1];
    }
    public void Show03()
    {
        myImage = (Image)devPortraits[2];
    }
    public void Show04()
    {
        myImage = (Image)devPortraits[3];
    }
    public void Show05()
    {
        myImage = (Image)devPortraits[4];
    }
    public void Show006()
    {
        myImage = (Image)devPortraits[5];
    }
    public void Show07()
    {
        myImage = (Image)devPortraits[6];
    }
    public void Show08()
    {
        myImage = (Image)devPortraits[7];
    }
    public void Show09()
    {
        myImage = (Image)devPortraits[8];
    }
    public void Show10()
    {
        myImage = (Image)devPortraits[9];
    }
    public void Show11()
    {
        myImage = (Image)devPortraits[10];
    }
    public void Show12()
    {
        myImage = (Image)devPortraits[11];
    }
}
