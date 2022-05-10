using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.UI
{
    public class WinLoseControl : MonoBehaviour
    {
        [SerializeField] PanelController winPanel;
        [SerializeField] PanelController losePanel;

        void Start()
        {
            LevelData.levelData.enemyCount.Where(x => x == 0).Subscribe(Win);
            LevelData.levelData.fortressCount.Where(x => x == 0).Subscribe(Lose);
        }

        void Win(int count)
        {
            Debug.Log("Start");
            StartCoroutine(DelayOnStart(winPanel));
        }

        void Lose(int count)
        {
            Debug.Log("Lose");
            StartCoroutine(DelayOnStart(losePanel));
        }

        IEnumerator DelayOnStart(PanelController panelPrefab)
        {
            yield return new WaitForSeconds(PanelControllData.TimePanelSpawnDelay);
            UIManager.Instance.AddPanel(panelPrefab);
        }
    }
}

