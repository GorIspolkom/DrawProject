using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.AdsNew
{
    enum RewardVideoWatchType
    {
        Finished,
        Skipped
    }
    class AdManager : MonoBehaviour
    {
        private static AdManager _instance;
        public static AdManager Instance => _instance;

        private List<IAdsProvider> providers;
        private int activeAd;
        private void Start()
        {
            activeAd = 0;
            _instance = this;
            providers = new List<IAdsProvider>();
            providers.Add(new MockAd());
            //#if UNITY_EDITOR
            //            providers.Add(new MockAd());
            //#else
            //            //инит провайдеров рекламы
            //#endif
        }
        public void LoadRewardedVideo(GameObject adButton)
        {
            for (int i = 0; i < providers.Count; i++)
                providers[i].LoadRewardedVideo(adButton);
            StartCoroutine(LoadAd(adButton));
        }
        private IEnumerator LoadAd(GameObject adButton)
        {
            yield return new WaitUntil(() => IsReadyRewardedVideo() && adButton != null);
            adButton.SetActive(true);
        }
        public bool IsReadyRewardedVideo()
        {
            for (int i = 0; i < providers.Count; i++)
                if (providers[i].IsReadyRewardedVideo())
                    return true;
            return false;
        }
        public void ShowAd(Action RewardedVideoFinished, Action RewardedVideoSkipped)
        {
            for(int i = 0; i < providers.Count; i++)
                if (providers[i].IsReadyRewardedVideo())
                {
                    providers[i].RewardedVideoFinished = RewardedVideoFinished;
                    providers[i].RewardedVideoSkipped = RewardedVideoSkipped;
                    providers[i].ShowRewardedVideo();
                    activeAd = i;
                    break;
                }
        }
        public void ExecuteReward(RewardVideoWatchType watchType)
        {
            if (watchType == RewardVideoWatchType.Finished)
            {
                providers[activeAd].RewardedVideoFinished?.Invoke();
                Deinit();
            }
            else if (watchType == RewardVideoWatchType.Skipped)
                providers[activeAd].RewardedVideoSkipped?.Invoke();
        }
        public void Deinit()
        {
            for (int i = 0; i < providers.Count; i++)
                providers[i].Deinit();
        }
    }
}
