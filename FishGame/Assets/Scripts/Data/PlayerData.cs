using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float CurrentHighScore;
    public LevelData[] Levels;

    public PlayerData(int levelCount)
    {
        CurrentHighScore = 0;
        Levels = new LevelData[levelCount];
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i] = new LevelData();
        }
    }
}

[System.Serializable]
public class LevelData
{
    public float Score;
    public float TimeBonus;
    public int Coins;

    public LevelData()
    {
        Score = 0;
        Coins = 0;
        TimeBonus = 2000;
    }
}