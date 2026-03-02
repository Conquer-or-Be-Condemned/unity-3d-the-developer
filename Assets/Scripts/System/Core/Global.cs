using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public static Global Instance { get; private set; }
    
    public GlobalServices Services { get; private set; }
    public Game Game { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Services = new GlobalServices();
        Services.Initialize();
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        
        //  SceneManagement 
        
        Services?.Dispose();
        Services = null;
    }

    //  Scene Loaded (Delegate-Called)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }

    private void OnSceneUnloaded(Scene scene)
    {
        
    }

    private bool IsInGameScene(Scene scene)
    {
        return scene.name.StartsWith("Stage_");
    }

    private void EnsureGame()
    {
        if (Game != null) return;

        var gameInstance = new GameObject("[Game]");
        Game = gameInstance.AddComponent<Game>();
        Game.Initialize(this);   //  Global <-> Game Link
    }

    private void DestroyGame()
    {
        if (Game == null) return;

        Game.Dispose();
        Destroy(Game.gameObject);
        Game = null;
    }

}
