using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class PanelController : MonoBehaviour
    {
        public void CreatePanel(PanelController panel)
        {
            UIManager.Instance.AddPanel(panel);
        }

        public void DestroySelf()
        {
            UIManager.Instance.DeletePanel(this);
            
        }

        public void DestoySelfAndLoadNew(PanelController panel)
        {
            UIManager.Instance.AddPanel(panel);
            DestroySelf();
        }
    }
}
