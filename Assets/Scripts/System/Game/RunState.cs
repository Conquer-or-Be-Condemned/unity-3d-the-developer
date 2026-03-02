using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState
{
    public int PlayerHp { get; internal set; }
    public int WaveIndex { get; internal set; }
    public bool IsPaused { get; internal set; }
}

public readonly struct PlayerHpChanged
{
    public readonly int Value;
    public readonly int Delta;
    //  Constructor
    public PlayerHpChanged(int value, int delta)
    {
        Value = value;
        Delta = delta;
    }
}

public readonly struct WaveIndexChanged
{
    public readonly int Value;
    public readonly int Delta;
    //  Constructor
    public WaveIndexChanged(int value, int delta)
    {
        Value = value;
        Delta = delta;
    }
}



