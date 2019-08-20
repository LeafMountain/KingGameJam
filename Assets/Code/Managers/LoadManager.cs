using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager 
{
    private static LoadManager loadManager_ref;

    private object[] devPortraits;

    
  
    public LoadManager()
    {
        if(loadManager_ref == null)
        {
            loadManager_ref = this;
        }
        
        devPortraits = Resources.LoadAll<Object>("Dev");
    }

    public static LoadManager GetInstance()
    {
        return loadManager_ref;
    }
   

    //properties
    public object[] DevPortraits { get { return devPortraits; } }
}
