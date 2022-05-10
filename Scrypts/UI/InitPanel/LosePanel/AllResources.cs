using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class AllResources : MonoBehaviour
    {
        [SerializeField] ValutType valut;

        void Start()
        {
            Text text = GetComponent<Text>();
            long add = (Profile.profileData.GetValut(valut) - LevelData.levelData.startValuts[(int)valut]);
            if (add > 0)
                text.text = $"+{add}";
            else
                text.text = "0";
        }
    }
}