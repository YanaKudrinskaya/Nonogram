using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Stats
{
    public static string PlayerName;
    public static int Level = 0;
    public static int Progress = 0;
    public static int PicturesCellSum = 0;
    public static int numberOfLifes = 3;

    public static void SetPlayer(string playerName, int level)
    {
        PlayerName = playerName;
        Level = level;
    }
    public static void NewLevel()
    {
        Progress = 0;
        PicturesCellSum = 0;
        numberOfLifes = 3;
    }
}


