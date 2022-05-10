using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scrypts.GameData;


namespace Assets.Scrypts.UI
{
    public class PanelAnim : MonoBehaviour
    {
        void Start()
        {
            transform.localScale = new Vector3(0.25f, 0.25f, 1);
            transform.DOScale(new Vector3(1, 1, 1), PanelControllData.TimePanelOutput).SetUpdate(true);
        }
    }
}