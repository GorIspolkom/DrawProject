using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class ResourcesPerLvl : MonoBehaviour
    {
        [SerializeField] ValutType valut;

        void Start()
        {
            LevelData.levelData.lvlValutes[(int)valut].Subscribe(UpdateScore).AddTo(this);
        }

        void UpdateScore(long value)
        {
            Text text = GetComponent<Text>();
            text.text = value.ToString();
        }
    }
}