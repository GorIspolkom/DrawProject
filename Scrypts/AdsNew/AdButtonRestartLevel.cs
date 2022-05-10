using Assets.Scrypts.LevelManagerSystem;
using Assets.Scrypts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.AdsNew
{
    class AdButtonRestartLevel : MonoBehaviour
    {
        [SerializeField] PanelController panel;
        private void Start()
        {
            gameObject.SetActive(false);
            AdManager.Instance.LoadRewardedVideo(gameObject);
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Action finished = () =>
                {
                    GameObject.FindObjectOfType<LevelManager>().ReloadLevel();
                    panel.DestroySelf();
                };
                Action skipped = () => { };
                AdManager.Instance.ShowAd(finished, skipped);
            });
        }
    }
}
