using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance => _instance;

        [SerializeField] Image drawArea;
        [SerializeField] GameObject blurPanel;

        List<PanelController> panels;

        public void AddPanel(PanelController panelPrefab)
        {

            if (panels.Count == 0)
            {
                SpawnPanel(panelPrefab);
                SetPause(true);
            }
            else
            {
                foreach (PanelController panel in panels)
                {
                    if (panelPrefab.gameObject.name == panel.gameObject.name)
                    {
                        return;
                    }
                }

                SpawnPanel(panelPrefab);
            }
        }

        public void DeletePanel(PanelController panel)
        {
            panels.Remove(panel);
            DestroyPanel(panel);

            if (panels.Count == 0)
            {
                SetPause(false);
            }
        }

        private void Awake()
        {
            panels = new List<PanelController>();
            _instance = this;

            PanelController[] activePanels = GameObject.FindObjectsOfType<PanelController>();
            for (int i = 0; i < activePanels.Length; i++)
                panels.Add(activePanels[i]);
            SetPause(panels.Count > 0);
        }

        private void SpawnPanel(PanelController panelPrefab)
        {
            PanelController panel = Instantiate(panelPrefab, this.transform);
            panel.name = panelPrefab.name;
            panels.Add(panel);
        }

        private void DestroyPanel(PanelController panel)
        {
            Destroy(panel.gameObject);
        }

        private void SetPause(bool key)
        {
            if (key)
            {
                Time.timeScale = 0;
                drawArea.raycastTarget = false;
                blurPanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                drawArea.raycastTarget = true;
                blurPanel.SetActive(false);
            }
        }
    }
}

