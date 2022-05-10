using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class LoadResources : MonoBehaviour
    {
        [SerializeField] ValutType valut;
        void Start()
        {
            Text text = GetComponent<Text>();
            text.text = Profile.profileData.GetValut(valut).ToString();
        }
    }
}