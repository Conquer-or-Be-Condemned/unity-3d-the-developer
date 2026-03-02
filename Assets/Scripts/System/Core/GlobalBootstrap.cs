using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (Object.FindFirstObjectByType<Global>() != null) return;

        var globalInstance = new GameObject("[Global]");
        globalInstance.AddComponent<Global>();
        Object.DontDestroyOnLoad(globalInstance);
    }
}
