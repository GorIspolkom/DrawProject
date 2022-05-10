using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSave : MonoBehaviour
{
    void Start()
    {
        foreach (ValutType valut in Enum.GetValues(typeof(ValutType)))
            Profile.AddValut(LevelData.levelData.lvlValutes[(int)valut].Value, valut);
        Profile.SaveData();
    }
}
