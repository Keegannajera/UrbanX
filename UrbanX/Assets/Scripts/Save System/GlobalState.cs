using System;
using System.Collections.Generic;
using UnityEngine;

// Global State for the Game
[System.Serializable]
public class GlobalData
{
    public int playerHealth = 100;
    public string playerName = "Player";
    public GlobalData()
    {
        //Set default val
    }
}