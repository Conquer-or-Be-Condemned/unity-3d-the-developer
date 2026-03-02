using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Native C# Class
public class GameServices
{
    private readonly Global _global;
    
    //  Constructor
    public GameServices(Global global)
    {
        _global = global;
    }

    public void Initialize()
    {
        //  Create State and InGame service
    }

    public void OnEnterScene(string sceneName)
    {
        //  Stage, Wave Load
    }

    public void Dispose()
    {
        
    }
}
