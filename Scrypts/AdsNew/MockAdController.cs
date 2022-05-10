using Assets.Scrypts.GameData;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.AdsNew
{
    class MockAdController : MonoBehaviour
    {
        public void SkipAd()
        {
            AdManager.Instance.ExecuteReward(RewardVideoWatchType.Skipped);
            Destroy(gameObject);
        }
        public void ShowAd(float showTime)
        {
            StartCoroutine(ShowAdCorutine(showTime));
        }
        public IEnumerator ShowAdCorutine(float showTime)
        {
            yield return new WaitForSecondsRealtime(showTime);
            AdManager.Instance.ExecuteReward(RewardVideoWatchType.Finished);
            Destroy(gameObject);
        }
    }
    class MockAd : IAdsProvider
    {
        MockAdController adPanelPrefab;
        GameObject observer;

        float timeForLoad = 2;
        float timeForShowAd = 5;

        private static string gameId = "999999";
        private float timer;

        //полный просмотр рекламы
        public Action RewardedVideoFinished { get; set; }
        //пропуск рекламы
        public Action RewardedVideoSkipped { get; set; }

        public MockAd()
        {
            Init();
        }

        //деструктор - унитожаем панель
        public void Deinit()
        {
            timer = float.MaxValue;
            RewardedVideoFinished = () => { };
            RewardedVideoSkipped = () => { };
        }

        public void Init(bool isChildDirected = false)
        {
            timer = float.MaxValue;
            adPanelPrefab = Resources.Load<MockAdController>(Paths.UI_PREFABS + "Panels/AdPanel");
        }

        public bool IsReadyRewardedVideo()
        {
            return timer < Time.realtimeSinceStartup;
        }

        public void LoadRewardedVideo(GameObject gameObject)
        {
            timer = Time.realtimeSinceStartup + timeForLoad;
            observer = gameObject;
        }

        public void ShowRewardedVideo()
        {
            MockAdController ad = GameObject.Instantiate(adPanelPrefab, GameObject.Find("BattleCan").transform);
            RewardedVideoFinished += () => GameObject.Destroy(observer);
            ad.ShowAd(timeForShowAd);
        }
    }
}
