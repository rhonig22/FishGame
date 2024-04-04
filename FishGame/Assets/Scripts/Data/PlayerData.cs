using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int CurrentHighScore;
    public float TimePassed;

    public PlayerData()
    {
        TimePassed = 0;
    }
}
