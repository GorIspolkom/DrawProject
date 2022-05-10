using Assets.Scrypts.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class LoadRecord : MonoBehaviour
    {
        void Start()
        {
            Text text = GetComponentInChildren<Text>();
            int lvl = Profile.profileData.maxLvl;

            if (lvl > 0)
                text.text = $"BEST RECORD: <color=#FF007E>{lvl} lvl</color>";
            else
                gameObject.SetActive(false);
        }
    }
}