using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scrypts.UI
{
    public class InitLvl : MonoBehaviour
    {
        void Start()
        {
            Text text = GetComponent<Text>();
            text.text = "LEVEL " + LevelData.levelData.currentLvl;
        }
    }
}