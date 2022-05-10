using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.AdsNew
{
    class AdButtonMutliplyScore : MonoBehaviour
    {
        [SerializeField] Text coin;
        [SerializeField] Text crystal;
        private void Start()
        {
            gameObject.SetActive(false);
            AdManager.Instance.LoadRewardedVideo(gameObject);
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Action finished = () => {
                    Profile.AddValut(LevelData.levelData.LvlCoins, ValutType.Coin);
                    Profile.AddValut(LevelData.levelData.LvlCrystals, ValutType.Crystal);
                    Profile.SaveData();

                    LevelData.levelData.LvlCoins *= 2;
                    LevelData.levelData.LvlCrystals *= 2;

                    coin.text = LevelData.levelData.LvlCoins.ToString();
                    crystal.text = LevelData.levelData.LvlCrystals.ToString();
                };
                Action skipped = () => { };
                AdManager.Instance.ShowAd(finished, skipped);
            });
        }
    }
}
