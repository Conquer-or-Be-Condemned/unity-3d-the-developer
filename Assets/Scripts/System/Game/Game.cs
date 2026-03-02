using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Global Global { get; private set; }
    public GameServices Services { get; private set; }

    public void Initialize(Global global)
    {
        Global = global;

        Services = new GameServices(Global);
        Services.Initialize();
    }

    public void OnEnterScene(string sceneName)
    {
        Services.OnEnterScene(sceneName);
    }

    public void Dispose()
    {
        Services?.Dispose();
        Services = null;
        Global = null;
    }
}
